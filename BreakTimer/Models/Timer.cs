using System;
using System.Windows.Threading;

namespace BreakTimer
{
    public sealed class Timer
    {
        private const double UpdateInterval = 1.0 / 60.0;

        public event Action<double> TimerTicked;
        public event Action TimerEnded;

        private readonly DispatcherTimer timer;

        private readonly DateTime startTime;

        private readonly double duration;

        public Timer(double duration)
        {
            this.duration = duration;

            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(UpdateInterval)
            };
            timer.Tick += OnTick;
            timer.Start();

            startTime = DateTime.Now;
        }

        public void Start()
        {
            timer.Start();
        }

        public void Pause()
        {
            timer.Stop();
        }

        private void OnTick(object sender, EventArgs e)
        {
            double timeDif = (DateTime.Now - startTime).TotalSeconds;

            if (timeDif >= duration)
            {
                timer.Stop();
                TimerEnded?.Invoke();
            }
            else
            {
                TimerTicked?.Invoke(timeDif);
            }
        }
    }
}
