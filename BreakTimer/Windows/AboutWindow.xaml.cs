using System.Windows;
using System.Diagnostics;
using System.Windows.Navigation;

namespace BreakTimer
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
            Show();
            Activate();
            closeButton.Focus();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void GithubLink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            ProcessStartInfo processStart = new ProcessStartInfo(e.Uri.ToString())
            {
                UseShellExecute = true,
                Verb = "open"
            };

            Process.Start(processStart);
        }
    }
}
