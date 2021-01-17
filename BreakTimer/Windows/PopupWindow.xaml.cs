using System;
using System.Windows;

namespace BreakTimer
{
    /// <summary>
    /// Interaction logic for TimerEndedPopupWindow.xaml
    /// </summary>
    public partial class PopupWindow : Window
    {
        public event Action Closed;

        public PopupWindow(string titleText, string contentText)
        {
            InitializeComponent();
            textBlock.Text = contentText;
            Title = titleText;

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
