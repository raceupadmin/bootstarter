using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bootstarter.Models.paths
{
    public class Paths
    {
        #region properties        
        public string VerPath { get; set; }
        public string ZipPath => "";
        public string AppPath => "";
        public string TmpDir {get; set;}
        public string VerURL => "";
        public string ZipURL => "";        
        #endregion

        private static Paths instance;

        private Paths()
        {
            //var settings = loadSettings();
            initPaths();
        }

        public static Paths getInstance()
        {   
            if (instance == null)
                instance = new Paths();
            return instance;
        }

        #region private
        Settings loadSettings()
        {
            string json = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "settings.json"));            
            Settings settings = JsonConvert.DeserializeObject<Settings>(json);
            return settings;

        }
        void initPaths()
        {            
            Settings settings = loadSettings();
            string user_path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string product_path = Path.Combine(user_path,
                                                $"Library",
                                                $"Application Support",
                                                settings.product_folder);
            if (!Directory.Exists(product_path))
                Directory.CreateDirectory(product_path);

            string tmp_path = Path.Combine(product_path, "tmp");
            if (!Directory.Exists(tmp_path)) 
                Directory.CreateDirectory(tmp_path);


            VerPath = $"{settings.update_url}/{settings.version_file}";

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
