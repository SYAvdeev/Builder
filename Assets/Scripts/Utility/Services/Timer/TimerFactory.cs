namespace Builder.Utility.Services.Timer
{
    public class TimerFactory
    {
        private readonly ApplicationStateObserver _applicationStateObserver;

        public TimerFactory(ApplicationStateObserver applicationStateObserver)
        {
            _applicationStateObserver = applicationStateObserver;
        }

        public TimerCommon GetTimer()
        {
            var timer = new TimerCommon
            {
                ApplicationStateObserver = _applicationStateObserver
            };
            return timer;
        }
    }
}