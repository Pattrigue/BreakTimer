using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows;
using BreakTimer.Extensions;

namespace BreakTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Timer timer = new Timer();
        private TimerEndedPopupWindow timerEndedPopup;

        private int timerDuration;

        private readonly string defaultTimerButtonText;

        public MainWindow()
        {
            InitializeComponent();

            defaultTimerButtonText = timerButton.Content.ToString();

            timer.TimerTicked += OnTimerTicked;
            timer.TimerEnded += OnTimerEnded;
        }

        private void TimerButton_Click(object sender, RoutedEventArgs e)
        {
            if (timer.TimerState == TimerState.Running)
            {
                PauseTimer();
            }
            else
            {
                StartTimer();
            }
        }
        
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ResetTimer();
        }

        private void TimeInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void TimeInput_TextChanged(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void StartTimer()
        {
            if (timeInput.Text.Length == 0) return;

            if (int.TryParse(timeInput.Text, out int inputMinutes))
            {
                timerDuration = inputMinutes * 60;
            }

            timeInput.IsEnabled = false;
            resetButton.IsEnabled = true;

            timerButton.Content = "Pause timer";
            timer.SetDuration(timerDuration);
            timer.Start();
        }

        private void PauseTimer()
        {
            timer.Pause();
            timerButton.Content = defaultTimerButtonText;
        }

        private void ResetTimer()
        {
            timer.Reset();

            timerButton.Content = defaultTimerButtonText;
            timerLabel.Content = "-";

            resetButton.IsEnabled = false;
            timeInput.IsEnabled = true;
        }

        private void OnTimerTicked(double timeLeft)
        {
            timerLabel.Content = timeLeft.ToTimestamp();
        }

        private void OnTimerEnded()
        {
            ResetTimer();
            Activate();

            timerButton.IsEnabled = timeInput.IsEnabled = false;
            timerEndedPopup = new TimerEndedPopupWindow();
            timerEndedPopup.Closed += OnTimerPopupClosed;
        }

        private void OnTimerPopupClosed()
        {
            timerButton.IsEnabled = timeInput.IsEnabled = true;
            timerEndedPopup.Closed -= OnTimerPopupClosed;
            timerEndedPopup = null;
        }
    }
}
