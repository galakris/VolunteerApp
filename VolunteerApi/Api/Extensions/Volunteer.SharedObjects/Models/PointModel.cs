namespace Volunteer.SharedObjects.Models
{
    public class PointModel
    {
        public PointModel(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public PointModel()
        {
            
        }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}