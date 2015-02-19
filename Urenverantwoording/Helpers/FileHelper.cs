using System.IO;
using Microsoft.Win32;
using Urenverantwoording.Interfaces;
using Urenverantwoording.Properties;

namespace Urenverantwoording.Helpers
{
    public class FileHelper : IFileHelper
    {
        public string CreateFile()
        {
            var dialog = new SaveFileDialog
            {
                FileName = Resources.DefaultFileName,
                DefaultExt = ".json",
                Filter = Resources.FileDescription + " (*.json) | *.json"
            };

            var result = dialog.ShowDialog();


            // only create file if OK button is pressed
            if (result == true)
            {
                var path = dialog.FileName;

                // create file
                var stream = File.Create(path);
                stream.Close();

                return path;
            }

            return null;
        }

        public string OpenFile()
        {
            var dialog = new OpenFileDialog();
            dialog.DefaultExt = ".json";
            dialog.Filter = Resources.FileDescription + " (*.json) | *.json";

            var result = dialog.ShowDialog();

            if (result == true)
            {
                var path = dialog.FileName;

                return path;
            }

            return null;
        }
    }
}
