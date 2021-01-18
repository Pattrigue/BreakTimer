using System.Windows;

namespace BreakTimer
{
    /// <summary>
    /// Interaction logic for TimeOutWindow.xaml
    /// </summary>
    public partial class TimeOutWindow : Window
    {
        public TimeOutWindow()
        {
            InitializeComponent();
            Topmost = true;
            Show();
            Activate();
            closeButton.Focus();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
