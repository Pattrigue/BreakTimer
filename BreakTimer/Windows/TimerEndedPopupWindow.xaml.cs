using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
