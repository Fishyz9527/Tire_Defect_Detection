using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tire_Defect_Detection.Models
{
    public class Production_Line
    {
        public string LineId { get; set; }           // 生产线ID
        public string LineName { get; set; }         // 生产线名称
        public decimal TargetThickness { get; set; } // 目标厚度
        public decimal Tolerance { get; set; }       // 允许公差

        //计算厚度合格范围
        public (decimal min, decimal max) GetThicknessRange()
        {
            return (TargetThickness - Tolerance, TargetThickness + Tolerance);
        }
    }
}
