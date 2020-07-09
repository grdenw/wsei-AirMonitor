namespace AirMonitor.Models
{
    public class NearestInstallation
    {
        public int Id { get; set; }
        public Location Location { get; set; }
        public Address Address { get; set; }
        public float Elevation { get; set; }
        public bool Airly { get; set; }
        public Sponsor Sponsor { get; set; }
    }
}
