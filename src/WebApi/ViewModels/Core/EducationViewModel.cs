using Domain.Core;

namespace WebApi.ViewModels.Core {
    public class EducationViewModel {
        public string Level { get; set; }
        public string Major { get; set; }
        public string SubMajor { get; set; }
        public string Status { get; set; }
        public string Institute { get; set; }
        public double? GPA { get; set; }

        public EducationViewModel(Education edu) {
            Level = edu.Level;
            Major = edu.Major;
            SubMajor = edu.SubMajor;
            Status = edu.Status;
            Institute = edu.Institute;
            GPA = edu. GPA;
        }
    }
}
