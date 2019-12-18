using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace csvWeb_v3_sqlite.Models
{
    public class TemperatureRecord
    {
        [Column("ID")]
        public string ID { get; set; }
        [Column("時間")]
        public DateTime 時間 { get; set; }
        [Column("第1段溫度顯示值")]
        public double 第1段溫度顯示值 { get; set; }
        [Column("第2段溫度顯示值")]
        public double 第2段溫度顯示值 { get; set; }
        [Column("第3段溫度顯示值")]
        public double 第3段溫度顯示值 { get; set; }
        [Column("第4段溫度顯示值")]
        public double 第4段溫度顯示值 { get; set; }
        [Column("第5段溫度顯示值")]
        public double 第5段溫度顯示值 { get; set; }
        [Column("第6段溫度顯示值")]
        public double 第6段溫度顯示值 { get; set; }
    }
}