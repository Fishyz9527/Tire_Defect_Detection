using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Tire_Defect_Detection.Models;

namespace Tire_Defect_Detection.Services
{
    //处理：连接、查询、插入
    public class Database_Service
    {
        private string connectionString = @"Data Source=DESKTOP-GO8JDSS\MSSQLSERVER01;Initial Catalog=TireInspection;Integrated Security=True";

        //测试链接
        public bool TestConnection()
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"数据库连接失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        //获取数据
        public List<Tire_Measurement> GetTireMeasurements()
        {
            var measurements = new List<Tire_Measurement>();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"SELECT * FROM tire_measurements ORDER BY measurement_time DESC";

                    using (var command = new SqlCommand(sql, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        int ordId = reader.GetOrdinal("id");
                        int ordTireId = reader.GetOrdinal("tire_id");
                        int ordThickness = reader.GetOrdinal("thickness");
                        int ordMeasurementTime = reader.GetOrdinal("measurement_time");
                        int ordProductionLine = reader.GetOrdinal("production_line");
                        int ordIsDefective = reader.GetOrdinal("is_defective");
                        int ordDefectType = reader.GetOrdinal("defect_type");
                        while (reader.Read())
                        {
                            measurements.Add(new Tire_Measurement
                            {
                                Id = !reader.IsDBNull(ordId) ? reader.GetInt32(ordId) : 0,
                                TireId = !reader.IsDBNull(ordTireId) ? reader.GetString(ordTireId) : string.Empty,
                                Thickness = !reader.IsDBNull(ordThickness) ? reader.GetDecimal(ordThickness) : 0m,
                                MeasurementTime = !reader.IsDBNull(ordMeasurementTime) ? reader.GetDateTime(ordMeasurementTime) : DateTime.MinValue,
                                ProductionLine = !reader.IsDBNull(ordProductionLine) ? reader.GetString(ordProductionLine) : string.Empty,
                                IsDefective = !reader.IsDBNull(ordIsDefective) ? reader.GetBoolean(ordIsDefective) : false,
                                DefectType = reader.IsDBNull(ordDefectType) ? string.Empty : reader.GetString(ordDefectType)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询数据失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return measurements;
        }

        //获取生产线配置
        public List<Production_Line> GetProductionLines()
        {
            var lines = new List<Production_Line>();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"SELECT * FROM production_lines";
                    using (var command = new SqlCommand(sql, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        int ordLineId = reader.GetOrdinal("line_id");
                        int ordLineName = reader.GetOrdinal("line_name");
                        int ordTargetThickness = reader.GetOrdinal("target_thickness");
                        int ordTolerance = reader.GetOrdinal("tolerance");
                        while (reader.Read())
                        {
                            lines.Add(new Production_Line
                            {
                                LineId = !reader.IsDBNull(ordLineId) ? reader.GetString(ordLineId) : string.Empty,
                                LineName = !reader.IsDBNull(ordLineName) ? reader.GetString(ordLineName) : string.Empty,
                                TargetThickness = !reader.IsDBNull(ordTargetThickness) ? reader.GetDecimal(ordTargetThickness) : 0m,
                                Tolerance = !reader.IsDBNull(ordTolerance) ? reader.GetDecimal(ordTolerance) : 0m
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询生产线配置失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return lines;
        }


        //插入新的数据
        public bool InsertTireMeasurement(Tire_Measurement measurement)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"INSERT INTO tire_measurements (tire_id, thickness, measurement_time, production_line, is_defective, defect_type)
                                   VALUES (@tire_id, @thickness, @measurement_time, @production_line, @is_defective, @defect_type)";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@tire_id", measurement.TireId);
                        command.Parameters.AddWithValue("@thickness", measurement.Thickness);
                        command.Parameters.AddWithValue("@measurement_time", measurement.MeasurementTime);
                        command.Parameters.AddWithValue("@production_line", measurement.ProductionLine);
                        command.Parameters.AddWithValue("@is_defective", measurement.IsDefective);
                        command.Parameters.AddWithValue("@defect_type", string.IsNullOrEmpty(measurement.DefectType) ? (object)DBNull.Value : measurement.DefectType);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"插入数据失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        //获取缺陷统计信息
        public DataTable GetDefectStatistics()
        {
            var dataTable = new DataTable();

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"
                        SELECT 
                            production_line as 生产线,
                            COUNT(*) as 检测数量,
                            SUM(CASE WHEN is_defective = 1 THEN 1 ELSE 0 END) as 缺陷数量,
                            CAST(SUM(CASE WHEN is_defective = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*) AS DECIMAL(5,2)) as 缺陷率
                        FROM tire_measurements
                        GROUP BY production_line
                        ORDER BY production_line";
                    using (var command = new SqlCommand(sql, connection))
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("获取缺陷统计信息失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dataTable;
        }
    }
}
