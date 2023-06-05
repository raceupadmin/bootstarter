using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace bootstarter.Models.paths
{
    public class Paths : IPaths
    {
        #region properties        
        public string VerPath { get; set; }
        public string ZipPath { get; set; }
        public string AppDir { get; set; }
        public string AppPath { get; set; }
        public string TmpDir { get; set; }
        public string VerURL { get; set; }
        public string ZipURL { get; set; }
        public string BootStarterPath { get; set; }
        #endregion

        private static Paths instance;

        private Paths(bool ismacosx)
        {
            if (ismacosx)
                initPathsMac();
            else
                initPathsWin();
        }

        public static Paths getInstance(bool ismacosx)
        {   
            if (instance == null)
                instance = new Paths(ismacosx);
            return instance;        
        }

        #region private
        Settings loadSettings()
        {
            //string json = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "settings.json"));            
            //Settings settings = JsonConvert.DeserializeObject<Settings>(json);
            //return settings;
            Settings settings = new Settings();
            settings.app_name = "RaceUP CRM";
            settings.product_folder = "RaceUP";
            settings.version_file = "version.json";
            settings.update_url = "https://asemenets.com/crm";

            return settings;

        }
        void initPathsWin()
        {
            Settings settings = loadSettings();
            string bootstarter_path = Environment.CurrentDirectory.ToString();
#if DEBUG
            BootStarterPath = Path.Combine(bootstarter_path, "bootstarter.exe");
#elif RELEASE
            BootStarterPath = Path.Combine(bootstarter_path, $"{settings.app_name}.exe");
#endif
            string user_path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string product_path = Path.Combine(user_path,
                                                "Library",
                                                "Application Support",
                                                settings.product_folder);
            if (!Directory.Exists(product_path))
                Directory.CreateDirectory(product_path);

            string app_dir = Path.Combine(user_path,
                                            "Library",
                                            "Application Support",
                                            settings.product_folder,
                                            settings.app_name);
            if (!Directory.Exists(app_dir))
                Directory.CreateDirectory(app_dir);

            string tmp_path = Path.Combine(app_dir, "tmp");
            if (!Directory.Exists(tmp_path))
                Directory.CreateDirectory(tmp_path);

            TmpDir = tmp_path;

            VerURL = $"{settings.update_url}/{settings.version_file}";

            VerPath = Path.Combine(app_dir, settings.version_file);

            ZipURL = $"{settings.update_url}/{settings.app_name}.zip".Replace(" ", "%20");

            ZipPath = Path.Combine(app_dir, $"{settings.app_name}.zip");

            AppDir = app_dir;

            AppPath = Path.Combine(app_dir, $"{settings.app_name}.exe");
        }
        void initPathsMac()
        {            
            Settings settings = loadSettings();
            //string bootstarter_path = Directory.GetCurrentDirectory();
            string bootstarter_path = System.AppContext.BaseDirectory;
            string[] splt = bootstarter_path.Split("/");
            var appElement = splt.FirstOrDefault(e => e.Contains(".app"));

            if (appElement != null)
            {
                int index = bootstarter_path.IndexOf(appElement);
                int length = bootstarter_path.Length;
                bootstarter_path = bootstarter_path.Remove(index, length - index);
            }


#if DEBUG
            BootStarterPath = Path.Combine(bootstarter_path, "bootstarter.app");
#else
            BootStarterPath = Path.Combine(bootstarter_path, $"{settings.app_name}.app");
#endif

            BootStarterPath = Path.GetFullPath(BootStarterPath);

            string user_path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string product_path = Path.Combine(user_path,
                                                "Library",
                                                "Application Support",
                                                settings.product_folder);
            if (!Directory.Exists(product_path))
                Directory.CreateDirectory(product_path);

            string app_dir = Path.Combine(user_path,
                                            "Library",
                                            "Application Support",
                                            settings.product_folder,
                                            settings.app_name);
            if (!Directory.Exists(app_dir))
                Directory.CreateDirectory(app_dir);

            string tmp_path = Path.Combine(app_dir, "tmp");
            if (!Directory.Exists(tmp_path)) 
                Directory.CreateDirectory(tmp_path);

            TmpDir = tmp_path;

            VerURL = $"{settings.update_url}/{settings.version_file}";

            VerPath = Path.Combine(app_dir, settings.version_file);

            ZipURL = $"{settings.update_url}/{settings.app_name}.app.zip".Replace(" ", "%20");

            ZipPath = Path.Combine(app_dir, $"{settings.app_name}.zip");

            AppDir = app_dir;

            AppPath = Path.Combine(app_dir, $"{settings.app_name}.app");

            //File.WriteAllText(Path.Combine(AppDir, "test.txt"), bootstarter_path);


        }
        #endregion
    }

    public class Settings
    {
        public string product_folder { get; set; }
        public string app_name { get; set; }
        public string version_file { get; set; }
        public string update_url { get; set; }

    }
}
