using bootstarter.Models.paths;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bootstarter.Models.consoles
{
    public class cmd : IConsole
    {
        #region vars
        IPaths paths;
        #endregion
        public cmd(IPaths paths)
        {
            this.paths = paths;
        }
        #region public
        public void Startup()
        {
            Process.Start(paths.AppPath);
        }
        #endregion
    }
}
