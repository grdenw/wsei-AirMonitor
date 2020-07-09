using System;

namespace AirMonitor.Models
{

    public class Measurement
    {
        public int CurrentDisplayValue { get; set; }
        public Current Current { get; set; }
        public History[] History { get; set; }
        public Forecast[] Forecast { get; set; }
        public NearestInstallation NearestInstallation { get; set; }
    }

    public class Current
    {
        public DateTime FromDateTime { get; set; }
        public DateTime TillDateTime { get; set; }
        public CurrentValue[] Values { get; set; }
        public CurrentIndex[] Indexes { get; set; }
        public CurrentStandard[] Standards { get; set; }
    }

    public class CurrentValue
    {
        public string Name { get; set; }
        public float Value { get; set; }
    }

    public class CurrentIndex
    {
        public string Name { get; set; }
        public float Value { get; set; }
        public string Level { get; set; }
        public string Description { get; set; }
        public string Advice { get; set; }
        public string Color { get; set; }
    }

    public class CurrentStandard
    {
        public string Name { get; set; }
        public string Pollutant { get; set; }
        public float Limit { get; set; }
        public float Percent { get; set; }
        public string Averaging { get; set; }
    }

    public class History
    {
        public DateTime FromDateTime { get; set; }
        public DateTime TillDateTime { get; set; }
        public HistoryValue[] Values { get; set; }
        public HistoryIndex[] Indexes { get; set; }
        public HistoryStandard[] Standards { get; set; }
    }

    public class HistoryValue
    {
        public string Name { get; set; }
        public float Value { get; set; }
    }

    public class HistoryIndex
    {
        public string Name { get; set; }
        public float? Value { get; set; }
        public string Level { get; set; }
        public string Description { get; set; }
        public string Advice { get; set; }
        public string Color { get; set; }
    }

    public class HistoryStandard
    {
        public string Name { get; set; }
        public string Pollutant { get; set; }
        public float Limit { get; set; }
        public float Percent { get; set; }
        public string Averaging { get; set; }
    }

    public class Forecast
    {
        public DateTime FromDateTime { get; set; }
        public DateTime TillDateTime { get; set; }
        public object[] Values { get; set; }
        public ForecastIndex[] Indexes { get; set; }
        public object[] Standards { get; set; }
    }

    public class ForecastIndex
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public string Level { get; set; }
        public string Description { get; set; }
        public object Advice { get; set; }
        public string Color { get; set; }
    }

}
