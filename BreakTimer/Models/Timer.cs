using System;
using System.Windows.Threading;
using System.Diagnostics;

namespace BreakTimer
{
    public sealed class Timer
    {
        private const double UpdateInterval = 1.0 / 60.0;

        public event Action<double> TimerTicked;
        public event Action TimerEnded;

        private readonly DispatcherTimer dispatcherTimer;
        private readonly Stopwatch stopwatch;

        private readonly double duration;

        public Timer(double duration)
        {
            this.duration = duration;

            dispatcherTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(UpdateInterval)
            };
            dispatcherTimer.Tick += OnTick;

            stopwatch = new Stopwatch();

            dispatcherTimer.Start();
            stopwatch.Start();
        }

        public void Start()
        {
            stopwatch.Start();
            dispatcherTimer.Start();
        }

        public void Pause()
        {
            stopwatch.Stop();
            dispatcherTimer.Stop();
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
