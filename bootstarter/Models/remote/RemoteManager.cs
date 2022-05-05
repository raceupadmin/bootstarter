using bootstarter.Models.paths;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace bootstarter.Models.remote
{
    public class RemoteManager : IRemoteManager
    {
        #region vars
        Paths paths = Paths.getInstance();
        string url;
        WebClient webClient;
        #endregion
        public RemoteManager(string url)
        {
            this.url = url; 
            webClient = new WebClient();
            webClient.DownloadProgressChanged += (sender, arg) => {

                double total = arg.TotalBytesToReceive;
                double received = arg.BytesReceived;

                ProgressChangedEvent?.Invoke(0, 0);
            };
        }

        #region public
        public async Task<string> GetVersion()
        {            
            string tmpVerPath = Path.Combine(paths.TmpDir, "version.json");
            await webClient.DownloadFileTaskAsync(new System.Uri(paths.VerURL), tmpVerPath);
            var str = File.ReadAllText(tmpVerPath);
            JObject json = JObject.Parse(str);
            string? version = json["version"]?.ToString();
            if (version == null)
                throw new Exception("Не удалось получить версию обновления");
            return version;
        }

        public Task<bool> GetArchive()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region callbacks
        public event Action<double, double> ProgressChangedEvent;
        #endregion

    }
}
