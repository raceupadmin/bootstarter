using Avalonia.Threading;
using bootstarter.Models.bootstarterpath;
using bootstarter.Models.consoles;
using bootstarter.Models.local;
using bootstarter.Models.paths;
using bootstarter.Models.remote;
using bootstarter.Models.version;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Reactive;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace bootstarter.ViewModels
{
    public class mainVM : ViewModelBase
    {
        #region vars
        IPaths paths;
        IRemoteManager remoteManager;
        ILocalManager localManager;
        IConsole console;
        #endregion

        #region properties
        string appname;
        public string AppName
        {
            get => appname;
            set => this.RaiseAndSetIfChanged(ref appname, value);   
        }

        string appversion;
        public string AppVersion
        {
            get => appversion;
            set => this.RaiseAndSetIfChanged(ref appversion, value);
        }

        string status;
        public string Status
        {
            get => status;
            set => this.RaiseAndSetIfChanged(ref status, value);   
        }

        bool isprogress;
        public bool IsProgress
        {
            get => isprogress;
            set => this.RaiseAndSetIfChanged(ref isprogress, value);
        }

        float progress;
        public float Progress
        {
            get => progress;
            set => this.RaiseAndSetIfChanged(ref progress, value);
        }
        #endregion

        #region commands
        ReactiveCommand<Unit, Unit> updateCmd { get; }
        #endregion        

        public mainVM()
        {

            AppName = "CRM";
            AppVersion = "";
            Status = "";
            IsProgress = false;

            #region dependencies           

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                paths = Paths.getInstance(true);
                console = new bash(paths);
            } else
            {
                paths = Paths.getInstance(false);
                console = new cmd(paths);
            }

            remoteManager = new RemoteManager(paths);
            remoteManager.ProgressChangedEvent += (p, t) =>
            {
                Progress = (float)(p * 100f / t);
            };
            localManager = new LocalManager(paths);
            #endregion

            #region commands
            updateCmd = ReactiveCommand.Create(() => {
            });
            #endregion
        }
        public async Task OnStarted()
        {

            Status = "Проверка обновлений...";
            await Task.Run(() => {
                Thread.Sleep(2000);
            });

            try
            {
                VersionFile remoteVersion = await remoteManager.GetVersion();
                string localVersion = localManager.GetVersion();
                
                if (!remoteVersion.Version.Equals(localVersion))
                {
                    Status = "Загрузка...";
                    IsProgress = true;
                    await remoteManager.GetArchive();               
                    localManager.UnZipApp();
                    localManager.UpdateVersionFile(remoteVersion);
                    IsProgress = false;
                    BootStarterFileConfig bs_config = new BootStarterFileConfig(paths.AppDir);
                    bs_config.BootStarterPath = paths.BootStarterPath;
                    bs_config.Save();
                }
                
            } catch (Exception ex)
            {
                IsProgress = false;
                Status = "Не удалось загрузить обновление";                
            }

            try
            {                
                Status = "Запуск...";
                await Task.Run(() => {
                    Thread.Sleep(2000);
                });
                console.Startup();
                Process.GetCurrentProcess().Kill();

            } catch (Exception ex)
            {
                Status = "Не удалось запустить приложение";
            }

        }
    }
}
