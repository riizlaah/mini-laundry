using System.Configuration;
using System.Data;
using System.Globalization;
using System.Windows;

namespace testWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //CultureInfo cult = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            //cult.NumberFormat.CurrencySymbol = "Rp";
            //cult.NumberFormat.CurrencyGroupSeparator = ".";
            //Thread.CurrentThread.CurrentCulture = cult;
            //Thread.CurrentThread.CurrentUICulture = cult;


            base.OnStartup(e);
        }
    }

}
