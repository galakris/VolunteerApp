using Volunteer.SharedObjects.Models;

namespace Volunteer.Services.Volunteers.Models
{
    public class SearchVolunteersResponseDto
    {
        public string Name { get; set; }

        public double Distance { get; set; }

        public PointModel Location { get; set; }
    }
}