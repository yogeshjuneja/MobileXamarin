using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMeditekApp.Wellogo.Models
{
    public class DashBoardHRA
    {
        public DataHRADashBoard data { get; set; }
    }
    public class Risks
    {
        public int heart { get; set; }
        public int diabetes { get; set; }
        public int stroke { get; set; }
        public int anaemia { get; set; }
        public int high_cholesterol { get; set; }
        public int satisfied_with_work { get; set; }
        public int less_time_for_family { get; set; }
        public int tobacco { get; set; }
        public int blood_pressure { get; set; }
        public int total_cholesterol { get; set; }
        public int blood_sugar { get; set; }
        public int cancer { get; set; }
    }

    public class DataHRADashBoard
    {
        public int hra_count { get; set; }
        public int last_completed_hra { get; set; }
        public string last_completed_hra_time { get; set; }
        public object blood_pressure_systolic { get; set; }
        public object blood_pressure_diastolic { get; set; }
        public object weight_kgs { get; set; }
        public object bmi { get; set; }
        public Risks risks { get; set; }
    }

    public class RootObject
    {
       
    }
}
