using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleWpf.Model
{
    public class Schedule
    {
        public string Date { get; set; }
        public List<Lesson> Lessons { get; set; }
    }
}
