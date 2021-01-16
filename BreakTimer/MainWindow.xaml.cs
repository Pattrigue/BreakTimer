using System.Windows;
using BreakTimer.Extensions;

namespace BreakTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const double TimerDuration = 1200;

        private enum TimerState { Stopped, Running, Paused }

        private Timer timer;

        private TimerState timerState;

        private readonly string defaultTimerButtonText;

        public MainWindow()
        {
            InitializeComponent();
            defaultTimerButtonText = timerButton.Content.ToString();
        }

        private void TimerButton_Click(object sender, RoutedEventArgs e)
        {
            if (timerState == TimerState.Running)
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

        private void StartTimer()
        {
            if (timerState == TimerState.Stopped)
            {
                timer = new Timer(TimerDuration);
                timer.TimerTicked += OnTimerTicked;
                timer.TimerEnded += OnTimerEnded;
            }
            else if (timerState == TimerState.Paused)
            {
                timer.Start();
            }

            resetButton.IsEnabled = true;
            timerButton.Content = "Pause timer";
            timerState = TimerState.Running;
        }

        private void PauseTimer()
        {
            timer.Pause();
            timerButton.Content = defaultTimerButtonText;
            timerState = TimerState.Paused;
        }

        private void ResetTimer()
        {
            timerButton.Content = defaultTimerButtonText;
            timerLabel.Content = "-";
            timerState = TimerState.Stopped;
            resetButton.IsEnabled = false;
        }

        private void OnTimerTicked(double time)
        {
            timerLabel.Content = time.ToTimestamp();
        }

        private void OnTimerEnded()
        {
            ResetTimer();
            Activate();
            new TimerEndedPopupWindow();
        }
    }
}
