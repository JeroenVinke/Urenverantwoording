namespace Urenverantwoording.Models
{
    public class File
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public File(string Path)
        {
            this.Path = Path;
            Name = System.IO.Path.GetFileName(Path);
        }
    }
}