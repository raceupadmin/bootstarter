﻿using bootstarter.Models.paths;
using bootstarter.Models.version;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bootstarter.Models.local
{
    public class LocalManager : ILocalManager
    {
        #region vars
        IPaths paths;
        #endregion
        public LocalManager(IPaths paths)
        {
            this.paths = paths;
        }

        #region public
        public string GetVersion()
        {
            string res = "0.0";             

            if (File.Exists(paths.VerPath)) {
                var str = File.ReadAllText(paths.VerPath);
                JObject json = JObject.Parse(str);
                string? version = json["version"]?.ToString();
                if (version != null)
                    res = version;
            }

            return res;
        }

        public void UnZipApp()
        {
            if (File.Exists(paths.ZipPath))
            {

                if (Directory.Exists(paths.AppPath))                
                    Directory.Delete(paths.AppPath, true);

                string macos_path = Path.Combine(paths.AppDir, "__MACOSX");
                if (Directory.Exists(macos_path))
                    Directory.Delete(macos_path, true);

                using (var archive = ZipFile.Open(paths.ZipPath, ZipArchiveMode.Update))
                {                    
                    archive.ExtractToDirectory(paths.AppDir, true);
                }

                File.Delete(paths.ZipPath);
                if (Directory.Exists(macos_path))
                    Directory.Delete(macos_path, true);


            } else
                throw new Exception("Не удалось обнаружить архив с приложением");
        }

        public void UpdateVersionFile(VersionFile version)
        {
            if (File.Exists(paths.VerPath))
                File.Delete(paths.VerPath);
            File.WriteAllText(paths.VerPath, version.File);
        }
        #endregion
    }
}
