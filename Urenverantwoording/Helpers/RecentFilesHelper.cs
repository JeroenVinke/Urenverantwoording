using System.Collections.Generic;
using System.Collections.Specialized;
using Urenverantwoording.Interfaces;
using Urenverantwoording.Models;
using Urenverantwoording.Properties;

namespace Urenverantwoording.Helpers
{
    public class RecentFilesHelper : IRecentFilesHelper
    {
        public List<File> GetFiles()
        {
            var list = new List<File>();


            var recentFiles = Settings.Default.RecentFiles;

            if (recentFiles != null && recentFiles.Count > 0)
            {
                foreach (var recentFile in recentFiles)
                {
                    list.Add(new File(recentFile));
                }
            }


            return list;
        }


        public void AddFile(string path)
        {
            var recentFiles = Settings.Default.RecentFiles;

            if (recentFiles == null)
            {
                Settings.Default.RecentFiles = new StringCollection();

                recentFiles = Settings.Default.RecentFiles;
            }



            if (!recentFiles.Contains(path))
            {
                recentFiles.Add(path);
            }

            Settings.Default.Save();
        }

        public void RemoveFile(File file)
        {
            var recentFiles = Settings.Default.RecentFiles;

            if (recentFiles != null)
            {
                recentFiles.Remove(file.Path);
            }

            Settings.Default.Save();
        }
    }
}
