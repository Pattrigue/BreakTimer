using System;
using System.Windows.Threading;

namespace BreakTimer
{
    public sealed class Timer
    {
        public event Action<double> TimerTicked;

        private readonly DispatcherTimer timer;

        private readonly DateTime startTime;

        public Timer()
        {
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1f / 60f)
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
            TimerTicked?.Invoke(timeDif);
        }
    }
}
