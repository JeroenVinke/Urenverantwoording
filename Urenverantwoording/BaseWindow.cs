using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Urenverantwoording
{
    public class BaseWindow : Window
    {
        public BaseWindow()
        {

            var converter = new ImageConverter();
            var bytes = (byte[])converter.ConvertTo(Properties.Resources.circularclock3, typeof(byte[]));

            var imageSource = new BitmapImage();
            imageSource.BeginInit();
            imageSource.StreamSource = new MemoryStream(bytes);
            imageSource.EndInit();


            Icon = imageSource;
        }
    }
}
