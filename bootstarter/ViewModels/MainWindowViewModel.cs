using ReactiveUI;
using System;
using System.Collections.Generic;
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

                WebClient client = new WebClient();
                client.DownloadFile("https://asemenets.com/test.zip", @"test.zip");

            });
            #endregion
        }
    }
}
