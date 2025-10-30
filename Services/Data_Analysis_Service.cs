using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tire_Defect_Detection.Models;

namespace Tire_Defect_Detection.Services
{
    //处理厚度检测的业务逻辑和数据分析
    public class Data_Analysis_Service
    {
        //分析厚度数据，判断是否合格
        public (bool isDefective, string defectType) AnalyzeThickness(decimal thickness, Production_Line production_Line)
        {
            var range = production_Line.GetThicknessRange();

            if (thickness < range.min)
            {
                return (true, "厚度偏小");
            }
            else if (thickness > range.max)
            {
                return (true, "厚度偏大");
            }
            else
            {
                return (false,null);
            }
        }

        //用于演示的模拟轮胎检测数据
        public List<Tire_Measurement> GenerateSimulatedData(int count,Production_Line line)
        {
            var random = new System.Random();
            var measurements = new List<Tire_Measurement>();

            for (int i = 0; i < count; i++)
            {
                //生成随机厚度，部分数据故意生成缺陷
                decimal thickness;
                if (i % 10 == 0)
                {
                    //每10个数据中有1个故意生成缺陷
                    thickness = line.TargetThickness + (i % 2 == 0 ? -line.Tolerance - (decimal)(random.NextDouble() * 2) : line.Tolerance + (decimal)(random.NextDouble() * 2));
                }
                else
                {
                    thickness = line.TargetThickness + (decimal)(random.NextDouble() * (double)(line.Tolerance * 2)) - line.Tolerance;
                }
                var analysis = AnalyzeThickness(thickness, line);
                measurements.Add(new Tire_Measurement
                {
                    Id = i + 1,
                    TireId = $"TIRE-{System.DateTime.Now:yyyyMMddHHmmssfff}{i}",
                    Thickness = Math.Round(thickness, 2),
                    MeasurementTime = System.DateTime.Now.AddMinutes(-i * 5),
                    ProductionLine = line.LineId,
                    IsDefective = analysis.isDefective,
                    DefectType = analysis.defectType
                });
            }
            return measurements;
        }
    }
}
