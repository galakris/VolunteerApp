using System.Collections.Generic;
using System.Threading.Tasks;
using Volunteer.Services.Volunteers.Models;

namespace Volunteer.Services.Volunteers.Interfaces
{
    public interface IVolunteerService
    {
        Task<List<SearchVolunteersResponseDto>> SearchVolunteers(int maxDistanceKm);
    }
}