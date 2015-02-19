using System.Collections.Generic;
using Urenverantwoording.Models;

namespace Urenverantwoording.Interfaces
{
    public interface IRecentFilesHelper
    {
        List<File> GetFiles();
        void AddFile(string path);
        void RemoveFile(File file);
    }
}