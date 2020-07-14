using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volunteer.DAL;
using Volunteer.DAL.Entities;
using Volunteer.DAL.Enums;
using Volunteer.Services.Volunteers.Interfaces;
using Volunteer.Services.Volunteers.Models;
using Volunteer.SharedObjects;
using Volunteer.SharedObjects.Enums;
using Volunteer.SharedObjects.Extensions;
using Volunteer.SharedObjects.Models;

namespace Volunteer.Services.Volunteers.Services
{
    public class VolunteerService : IVolunteerService
    {
        private readonly DalContext _dalContext;
        private readonly ApiIdentity _identity;

        public VolunteerService(DalContext dalContext, ApiIdentity identity)
        {
            _dalContext = dalContext;
            _identity = identity;
        }

        public async Task<List<SearchVolunteersResponseDto>> SearchVolunteers(int maxDistanceKm)
        {
            var user = (await _dalContext.Users.SingleOrDefaultAsync(x => x.Id == _identity.UserAcountId));

            var closestLocations = _dalContext.Users.Where(x => x.Role == Role.Volunteer)
                .ToList()
                .GroupBy(x => Math.Pow((user.Latitude - x.Latitude), 2) + Math.Pow((user.Longitude - x.Longitude), 2))
                .OrderBy(x => x.Key);

            var response = new List<SearchVolunteersResponseDto>();
            foreach (var closestLocation in closestLocations)
            {
                foreach (var location in closestLocation)
                {
                    var distance = DistanceExtension.GetDistance(new PointModel()
                    {
                        Longitude = user.Longitude,
                        Latitude = user.Latitude
                    }, new PointModel()
                    {
                        Longitude = location.Longitude,
                        Latitude = location.Latitude
                    }, DistanceUnit.Kilometers);

                    if (distance < maxDistanceKm)
                    {
                        response.Add(new SearchVolunteersResponseDto()
                        {
                            Distance = distance,
                            Name = location.FirstName,
                            Location = new PointModel()
                            {
                                Longitude = location.Longitude,
                                Latitude = location.Latitude
                            }
                        });
                    }
                }
            }

            return response;
        }
    }
}