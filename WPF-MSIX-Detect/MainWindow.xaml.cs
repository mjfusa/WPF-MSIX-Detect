using CommunityToolkit.WinUI.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_MSIX_Detect
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var helpers = new Helpers();
            var res = helpers.IsRunningInMSIXContainer();
            txtResult.Text = res ? "Running in MSIX Container" : "Not running in MSIX Container";
            btnShowToast.IsEnabled = res;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new ToastContentBuilder()
                .AddText("Hey look")
                .AddText("A toast notification from a WPF App!")
                .AddHeroImage(new Uri("https://picsum.photos/360/202"))
                .Show();
        }
    }
}
