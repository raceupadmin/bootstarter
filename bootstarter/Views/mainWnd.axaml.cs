using Avalonia.Controls;
using bootstarter.ViewModels;
using System;

namespace bootstarter.Views
{
    public partial class mainWnd : Window
    {
        public mainWnd()
        {
            InitializeComponent();
        }
        protected override void OnOpened(EventArgs e)
        {
            base.OnOpened(e);
            ((mainVM)DataContext).OnStarted();
        }
       
    }
}
