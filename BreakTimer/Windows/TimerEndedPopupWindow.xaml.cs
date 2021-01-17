using System;
using System.Windows;

namespace BreakTimer
{
    /// <summary>
    /// Interaction logic for TimerEndedPopupWindow.xaml
    /// </summary>
    public partial class TimerEndedPopupWindow : Window
    {
        public event Action Closed;

        public TimerEndedPopupWindow()
        {
            InitializeComponent();
            Show();
            Activate();
            closeButton.Focus();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Closed?.Invoke();
            Close();
        }
    }
}
