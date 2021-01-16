using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BreakTimer.Extensions;

namespace BreakTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const double BreakTime = 2;

        private enum TimerState { None, Running, Paused }

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
            if (timerState == TimerState.None)
            {
                timer = new Timer();
                timer.TimerTicked += UpdateTimer;
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
            if (timer == null) return;

            timerButton.Content = defaultTimerButtonText;
            timerLabel.Content = "-";
            timer.Pause();
            timer = null;
            timerState = TimerState.None;
            resetButton.IsEnabled = false;
        }

        private void UpdateTimer(double time)
        {
            if (time >= BreakTime)
            {
                OnTimerEnded();
                return;
            }

            timerLabel.Content = time.ToTimestamp();
        }

        private void OnTimerEnded()
        {
            ResetTimer();

            Activate();
            new PopupWindow();
        }
    }
}
