using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Reactive;
using System.Text;

namespace bootstarter.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region commands
        ReactiveCommand<Unit, Unit> updateCmd { get; }
        #endregion        

        public MainWindowViewModel()
        {
            #region commands
            updateCmd = ReactiveCommand.Create(() => {


                string user_path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string app_path = Path.Combine(user_path, $"Library", $"Application Support", $"XTime");
                if (!Directory.Exists(app_path))
                    Directory.CreateDirectory(app_path);
                WebClient client = new WebClient();
                string zip_paht = Path.Combine(app_path, "tmp.zip");
                client.DownloadFile("https://asemenets.com/test.zip", zip_paht);

                using (var archive = ZipFile.Open(zip_paht, ZipArchiveMode.Update))
                {
                    archive.ExtractToDirectory(app_path);
                }

                File.Delete(zip_paht);

            });
            #endregion
        }
    }
}
