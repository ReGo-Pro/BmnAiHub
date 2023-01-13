using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core {
    public class Education {
        public int ID { get; set; }
        public string Level { get; set; }
        public string Major { get; set; }
        public string? SubMajor { get; set; }
        public string Status { get; set; }
        public string Institute { get; set; }
        public double? GPA { get; set; }

        public virtual User User { get; set; }
        public string UserId { get; set; }
    }
}
