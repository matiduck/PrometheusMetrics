using System.Diagnostics.Metrics;

namespace PrometheusMetrics
{
    public class ServiceMetrics
    {
        private Meter meter;

        private Counter<long> requestCounter;
        private long totalRequests = 0;

        public ServiceMetrics()
        {
            meter = new Meter("WeatherForecast");

            meter.CreateObservableCounter<long>("weatherForecast-request-observable-count", () =>
            {
                return totalRequests;
            });

            requestCounter = meter.CreateCounter<long>("weatherForecast-request-count");
        }

        public string Name => meter.Name;

        public void IncrementObservableCounter()
        {
            totalRequests++;
        }

        public void IncrementCounter(long value)
        {
            this.requestCounter.Add(value);
        }
    }
}
