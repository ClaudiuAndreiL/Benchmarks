using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Benchmarks.DeniedContent.RegexVersion.Models
{
    public class DestinationValidationData
    {
        public Regex Regex { get; set; }
        public HashSet<string> DeniedSenders { get; set; }
    }
}
