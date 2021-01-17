using System;
using System.Windows.Threading;
using System.Diagnostics;

namespace BreakTimer
{
    public sealed class Timer
    {
        private const float UpdateInterval = 0.1f;

        public TimerState TimerState { get; private set; }

        public event Action<double> TimerTicked;
        public event Action TimerEnded;

        private readonly DispatcherTimer dispatcherTimer;
        private readonly Stopwatch stopwatch;

        private double duration;

        public Timer()
        {
            dispatcherTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(UpdateInterval)
            };
            dispatcherTimer.Tick += OnTick;

            stopwatch = new Stopwatch();
            TimerState = TimerState.Stopped;
        }

        public void Reset()
        {
            stopwatch.Reset();
            dispatcherTimer.Stop();

            TimerState = TimerState.Stopped;
        }

        public void SetDuration(double duration)
        {
            this.duration = duration;
        }

        public void Start()
        {
            stopwatch.Start();
            dispatcherTimer.Start();

            TimerState = TimerState.Running;
        }

        public void Pause()
        {
            stopwatch.Stop();
            dispatcherTimer.Stop();

            TimerState = TimerState.Paused;
        }

        private void OnTick(object sender, EventArgs e)
        {
            double elapsedTime = stopwatch.ElapsedMilliseconds / 1000.0;

            if (elapsedTime >= duration)
            {
                dispatcherTimer.Stop();
                TimerEnded?.Invoke();
            }
            else
            {
                TimerTicked?.Invoke(duration - elapsedTime);
            }
        }
    }
}
