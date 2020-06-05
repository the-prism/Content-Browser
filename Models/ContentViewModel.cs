using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Content_Browser.Models
{
    public class ContentViewModel
    {
        public bool IsFolder { get; set; }
        public string ParentPath { get; set; }
        public List<string> ContentChildsFolders { get; set; }
        public List<string> ContentChildsFiles { get; set; }
        public string ContentPath { get; set; }
        public string ContentType { get; set; }

        public string PreviousFile { get; set; }

        public string NextFile { get; set; }
    }
}
