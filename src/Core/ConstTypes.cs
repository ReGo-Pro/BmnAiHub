using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// TODO: Look for a better pattern for this
namespace Core {
    public class ContentStatus {
        private static List<string> _values = new List<string> {
            "Draft",
            "PendingApproval",
            "PendingPublish",
            "Published",
            "Archived",
            "Declined",
            "Deleted",
            "ModificationRequired"
        };

        public static string Draft => _values[0];
        public static string PendingApproval => _values[1];
        public static string PendingPublish => _values[2];
        public static string Published => _values[3];
        public static string Archived => _values[4];
        public static string Declined => _values[5];
        public static string Deleted => _values[6];
        public static string ModificationRequired => _values[7];

        public static IEnumerable<string> All => _values.AsReadOnly();
    }
}
