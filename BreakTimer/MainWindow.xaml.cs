﻿using System.Text.RegularExpressions;
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
        private enum TimerState { Stopped, Running, Paused }

        private Timer timer;
        private TimerEndedPopupWindow timerEndedPopup;

        private TimerState timerState;

        private int timerDuration;

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

            if (int.TryParse(e.Text, out int inputMinutes))
            {
                timerDuration = inputMinutes * 1;
            }
        }

        private void StartTimer()
        {
            if (timerState == TimerState.Stopped)
            {
                timer = new Timer(timerDuration);
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
            timer.Pause();

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

            timerButton.IsEnabled = false;
            timerEndedPopup = new TimerEndedPopupWindow();
            timerEndedPopup.Closed += OnTimerPopupClosed;
        }

        private void OnTimerPopupClosed()
        {
            timerButton.IsEnabled = true;
            timerEndedPopup = null;
            timerEndedPopup.Closed -= OnTimerPopupClosed;
        }
    }
}
