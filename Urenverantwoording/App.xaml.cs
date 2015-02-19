using System.Globalization;
using System.Windows;
using System.Windows.Markup;

namespace Urenverantwoording
{
    public partial class App
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {

            ApplyCulture();
        }

        private void ApplyCulture()
        {
            var language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);

            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(language));
        }
    }
}
