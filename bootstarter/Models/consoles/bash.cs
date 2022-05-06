using bootstarter.Models.paths;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bootstarter.Models.consoles
{
    public class bash : IConsole
    {
        #region vars
        IPaths paths;
        #endregion
        public bash(IPaths paths)
        {
            this.paths = paths;
        }
        #region public
        public void Startup()
        {
            Process.Start(new ProcessStartInfo("open", $"-a \"{paths.AppPath}\"  -n --args --no-first-run")
            { UseShellExecute = false });
        }
        #endregion
    }
}
