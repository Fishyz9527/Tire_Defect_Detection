using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tire_Defect_Detection.Models
{
    public class Tire_Measurement
    {
        public int Id { get; set; }                     // 记录ID
        public string TireId { get; set; }              // 轮胎编号
        public decimal Thickness { get; set; }          // 厚度测量值
        public DateTime MeasurementTime { get; set; }   // 检测时间
        public string ProductionLine { get; set; }      // 生产线编号
        public bool IsDefective { get; set; }           // 是否缺陷
        public string DefectType { get; set; }          // 缺陷类型

        //获取厚度状态描述
        public string StatusDescription
        {
            get
            {
                return IsDefective ? $"缺陷: {DefectType}" : "正常";
            }
        }
    }
}
