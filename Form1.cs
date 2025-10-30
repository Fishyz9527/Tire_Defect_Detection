using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tire_Defect_Detection.Models;
using Tire_Defect_Detection.Services;

namespace Tire_Defect_Detection
{
    //显示轮胎检测数据的监控面板和统计信息
    public partial class Form1 : Form
    {
        private Database_Service dbService;
        private Data_Analysis_Service analysisService;

        public Form1()
        {
            InitializeComponent();
            InitializeServices();
            LoadData();
            SetupDataGridColumns();
        }

        #region 初始化数据服务
        private void InitializeServices()
        {
            dbService = new Database_Service();
            analysisService = new Data_Analysis_Service();

            // 测试数据库连接
            if (!dbService.TestConnection())
            {
                lblStatus.Text = "数据库连接失败";
                lblStatus.ForeColor = Color.Red;
            }
        }
        #endregion

        #region 设置数据表
        private void SetupDataGridColumns()
        {
            dgvMeasurements.Columns.Clear();
            dgvMeasurements.Columns.Add("ID", "ID");
            dgvMeasurements.Columns.Add("Tire_ID", "轮胎编号");
            dgvMeasurements.Columns.Add("Thickness", "厚度 (mm)");
            dgvMeasurements.Columns.Add("Production_Line", "生产线");
            dgvMeasurements.Columns.Add("Measurement_Time", "检测时间");
            dgvMeasurements.Columns.Add("Defect_Type", "状态");

        }
        #endregion

        #region 加载数据
        private void LoadData()
        {
            try
            {
                lblStatus.Text = "正在加载数据...";
                var measurements = dbService.GetTireMeasurements();
                dgvMeasurements.Rows.Clear();

                foreach (var measurement in measurements.Take(100))
                {
                    dgvMeasurements.Rows.Add(
                        measurement.Id,
                        measurement.TireId,
                        measurement.Thickness,
                        measurement.ProductionLine,
                        measurement.MeasurementTime.ToString("yyyy-MM-dd HH:mm:ss:fff"),
                        measurement.IsDefective ? $"缺陷: {measurement.DefectType}" : "合格"
                    );
                    var row = dgvMeasurements.Rows[dgvMeasurements.Rows.Count - 1];
                    row.DefaultCellStyle.ForeColor=measurement.IsDefective ? Color.Red : Color.Black;
                }

                //加载统计信息
                var stats = dbService.GetDefectStatistics();
                dgvStatistics.DataSource = stats;
                lblStatus.Text = $"数据加载完成,共{measurements.Count}条记录";
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"加载数据出错: {ex.Message}";
                lblStatus.ForeColor = Color.Red;
            }
        }
        #endregion
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnSimulateData_Click(object sender, EventArgs e)
        {
            try
            {
                var lines = dbService.GetProductionLines();
                if (lines.Count == 0)
                {
                    MessageBox.Show("没有找到生产线配置，无法模拟数据。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                foreach (var line in lines)
                {
                    //每条生产线模拟100条数据
                    for (int i = 0; i < 100; i++)
                    {
                        var simulatedData = analysisService.GenerateSimulatedData(10,line);
                        foreach (var data in simulatedData)
                        {
                            dbService.InsertTireMeasurement(data);
                        }
                    }

                    MessageBox.Show($"模拟数据插入完成。", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"模拟数据生成失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
