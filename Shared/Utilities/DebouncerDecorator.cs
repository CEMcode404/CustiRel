namespace crm.Shared.Utilities
{
    public class DebouncerDecorator
    {
        //This timer is recreated and dispose each debounce since razor which 
        //this class is use don't have a life cycle hook for component unmount  to clean up this timer
        private Timer timer;
        private readonly int debounceInterval;
        private Func<Task> debounceFunc = () => Task.CompletedTask;

        public DebouncerDecorator(int interval)
        {
            debounceInterval = interval;
            timer = new Timer(TimerElapsed, null, Timeout.Infinite, Timeout.Infinite);
        }

        public void CancelDebounce()
        {
            timer.Dispose();
        }
        public Func<Task> Debounce(Func<Task> function)
        {
            debounceFunc = function;
            return async () =>
            {
                timer.Dispose();
                timer = new Timer(TimerElapsed, null, Timeout.Infinite, Timeout.Infinite);
                timer.Change(debounceInterval, Timeout.Infinite);
                await Task.CompletedTask;
            };
        }


        private async void TimerElapsed(object? state)
        {
            await debounceFunc.Invoke();
            timer.Dispose();
        }
    }

}