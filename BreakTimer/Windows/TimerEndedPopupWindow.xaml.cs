using System.Windows;

namespace BreakTimer
{
    /// <summary>
    /// Interaction logic for TimerEndedPopupWindow.xaml
    /// </summary>
    public partial class TimerEndedPopupWindow : Window
    {
        public TimerEndedPopupWindow()
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
    }
}
