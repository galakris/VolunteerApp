using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.DAL;
using Volunteer.DAL.Entities;
using Volunteer.DAL.Enums;
using Volunteer.DAL.Relations;
using Volunteer.Services.Needs.Interfaces;
using Volunteer.Services.Needs.Models;
using Volunteer.Services.Volunteers.Interfaces;
using Volunteer.SharedObjects;
using Volunteer.SharedObjects.Enums;
using Volunteer.SharedObjects.Extensions;
using Volunteer.SharedObjects.Models;

namespace Volunteer.Services.Needs.Services
{
    public class NeedService : INeedService
    {
        private readonly DalContext _dalContext;
        private readonly ApiIdentity _apiIdentity;

        public NeedService(DalContext dalContext, ApiIdentity apiIdentity, IVolunteerService volunteerService)
        {
            _dalContext = dalContext;
            _apiIdentity = apiIdentity;
        }

        public async Task<ICollection<NeedDto>> GetNeeds(bool my = false)
        {
            List<NeedEntity> needs;

            if(my)
            {
                needs = await _dalContext.Needs.Include(x => x.UserAccountNeeds).ThenInclude(x => x.UserAccount)
                    .Where(x => x.UserAccountNeeds.Any(y => y.UserAccountId == _apiIdentity.UserAcountId)).ToListAsync();
            }
            else
            {
                needs = await _dalContext.Needs.Include(x => x.UserAccountNeeds).ThenInclude(x => x.UserAccount)
                    .Where(x => x.NeedStatus == NeedStatus.NotStarted && x.UserAccountNeeds.Any(y => y.UserAccountId != _apiIdentity.UserAcountId))
                    .ToListAsync();
            }

            return needs.Select(x => new NeedDto()
            {
                Name = x.Name,
                Category = x.Category,
                DeadlineDate = x.DeadlineDate,
                Description = x.Description,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                Distance = Math.Round(DistanceExtension.GetDistance(new PointModel(_apiIdentity.Latitude, _apiIdentity.Longitude), new PointModel(x.Latitude, x.Longitude), DistanceUnit.Kilometers), 2),
                Id = x.Id,
                NeedStatus = x.NeedStatus,
                Needy =  new NeedUser()
                {
                    Telephone = x.UserAccountNeeds.SingleOrDefault(y => y.Role == Role.Needy)?.UserAccount.Telephone,
                    FirstName = x.UserAccountNeeds.SingleOrDefault(y => y.Role == Role.Needy)?.UserAccount.FirstName
                },
                AssignedVolunteer = x.UserAccountNeeds.Any(y => y.Role == Role.Volunteer) ? new NeedUser()
                {
                    Telephone = x.UserAccountNeeds.SingleOrDefault(y => y.Role == Role.Volunteer)?.UserAccount.Telephone,
                    FirstName = x.UserAccountNeeds.SingleOrDefault(y => y.Role == Role.Volunteer)?.UserAccount.FirstName
                } : null
            }).ToList();
        }

        public async Task<int> DeleteNeed(int needId)
        {
            var need = await _dalContext.Needs.Include(x => x.UserAccountNeeds)
                    .SingleOrDefaultAsync(x => x.UserAccountNeeds.Any(y => y.UserAccountId == _apiIdentity.UserAcountId && y.Role == Role.Needy) && x.Id == needId);

            if (need == null)
            {
                throw new Exception("Need not exist, or you do not have permissions");
            }

            _dalContext.Needs.Remove(need);
            await _dalContext.SaveChangesAsync();

            return needId;
        }

        public async Task<NeedDto> ModifyNeedStatus(int needId, NeedStatus needStatus)
        {
            var need = await _dalContext.Needs.Include(x => x.UserAccountNeeds)
                .SingleOrDefaultAsync(x => x.UserAccountNeeds.Any(y => y.UserAccountId == _apiIdentity.UserAcountId) && x.Id == needId);

            need.NeedStatus = needStatus;
            _dalContext.Needs.Update(need);
            await _dalContext.SaveChangesAsync();

            return new NeedDto()
            {
                Name = need.Name,
                Category = need.Category,
                DeadlineDate = need.DeadlineDate,
                Description = need.Description,
                Latitude = need.Latitude,
                Longitude = need.Longitude,
                Distance = Math.Round(DistanceExtension.GetDistance(new PointModel(_apiIdentity.Latitude, _apiIdentity.Longitude), new PointModel(need.Latitude, need.Longitude), DistanceUnit.Kilometers), 2),
                Id = need.Id,
                NeedStatus = need.NeedStatus
            };
        }

        public async Task<CreateNeedResponseDto> CreateNeed(CreateNeedRequestDto requestDto)
        {
            var need = new NeedEntity()
            {
                Name = requestDto.Name,
                Description = requestDto.Description,
                DeadlineDate = requestDto.DeadlineDate,
                Category = requestDto.Category,
                Latitude = requestDto.Latitude,
                Longitude = requestDto.Longitude,
                NeedStatus = NeedStatus.NotStarted
            };

            await _dalContext.Needs.AddAsync(need);
            await _dalContext.SaveChangesAsync();

            var userAccountNeed = new UserAccountNeed()
            {
                NeedId = need.Id,
                UserAccountId = _apiIdentity.UserAcountId,
                Role = Role.Needy
            };

            _dalContext.UserAccountNeeds.Add(userAccountNeed);
            await _dalContext.SaveChangesAsync();

            return new CreateNeedResponseDto()
            {
                Name = need.Name,
                Category = need.Category,
                DeadlineDate = need.DeadlineDate,
                Description = need.Description,
                Latitude = need.Latitude,
                Longitude = need.Longitude,
                NeedId = need.Id
            };
        }

        public async Task<object> AssignVolunteerToNeed(int needId)
        {
            if (_apiIdentity.Role == Role.Needy)
            {
                throw new Exception("You are needy, you can not help");
            }

            var need = await _dalContext.Needs.Include(x => x.UserAccountNeeds).ThenInclude(x => x.UserAccount)
                .SingleOrDefaultAsync(x => x.Id == needId);
            if(need == null)
            {
                throw new Exception("Need not exist, or you do not assign to you");
            }

            if (need.UserAccountNeeds.Any(y => y.Role == Role.Volunteer))
            {
                throw new Exception("This need is already assigned to another volunteer");
            }

            var needyUser = need.UserAccountNeeds.Single(x => x.Role == Role.Needy).UserAccount;

            var userAccountNeed = new UserAccountNeed()
            {
                NeedId = need.Id,
                UserAccountId = _apiIdentity.UserAcountId,
                Role = Role.Volunteer
            };

            need.NeedStatus = NeedStatus.InProgress;
            _dalContext.UserAccountNeeds.Add(userAccountNeed);
            _dalContext.Needs.Update(need);
            await _dalContext.SaveChangesAsync();

            return new AssignVolunteerToNeedResponseDto()
            {
                NeedId = need.Id,
                VolunteerUserAccountId = _apiIdentity.UserAcountId,
                NeedyTelephone = needyUser.Telephone,
                NeedyFirstName = needyUser.FirstName
            };
        }

        //public async Task<object> UnassignVolunteerFromNeed(int needId)
        //{
        //    var need = await _dalContext.UserAccountNeeds.SingleOrDefaultAsync(x => x.NeedId == needId && x.UserAccountId == _apiIdentity.UserAcountId);
        //    if (need == null)
        //    {
        //        throw new Exception("Need not exist");
        //    }


        //    _dalContext.UserAccountNeeds.Remove(need);
        //    await _dalContext.SaveChangesAsync();

        //    return new
        //    {
        //        NeedId = need.NeedId,
        //        UserAccountId = _apiIdentity.UserAcountId
        //    };
        //}
    }
}
