using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bootstarter.Models.paths
{
    public interface IPaths
    {
        public string VerPath { get; set; }
        public string ZipPath { get; set; }
        public string AppDir { get; set; }
        public string AppPath { get; set; }
        public string TmpDir { get; set; }
        public string VerURL { get; set; }
        public string ZipURL { get; set; }
    }
}
