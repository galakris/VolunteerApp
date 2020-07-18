using System;
using Volunteer.SharedObjects.Enums;
using Volunteer.SharedObjects.Models;

namespace Volunteer.SharedObjects.Extensions
{
    public static class DistanceExtension
    {
        public static double GetDistance(PointModel point1, PointModel point2, DistanceUnit unit)
        {
            if ((point1.Latitude == point2.Latitude) && (point1.Longitude == point2.Longitude))
            {
                return 0;
            }
            else
            {
                double theta = point1.Longitude - point2.Longitude;
                double dist = Math.Sin(DegToRadians(point1.Latitude)) * Math.Sin(DegToRadians(point2.Latitude)) + Math.Cos(DegToRadians(point1.Latitude)) * Math.Cos(DegToRadians(point2.Latitude)) * Math.Cos(DegToRadians(theta));
                dist = Math.Acos(dist);
                dist = RadiansToDeg(dist);
                dist = dist * 60 * 1.1515;
                if (unit == DistanceUnit.Kilometers)
                {
                    dist = dist * 1.609344;
                }
                else if (unit == DistanceUnit.NauticalMiles)
                {
                    dist = dist * 0.8684;
                }
                return (dist);
            }
        }

        private static double DegToRadians(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private static double RadiansToDeg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}