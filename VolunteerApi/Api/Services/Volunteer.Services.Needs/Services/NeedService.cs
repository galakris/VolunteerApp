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
using Volunteer.SharedObjects;

namespace Volunteer.Services.Needs.Services
{
    public class NeedService : INeedService
    {
        private readonly DalContext _dalContext;
        private readonly ApiIdentity _apiIdentity;

        public NeedService(DalContext dalContext, ApiIdentity apiIdentity)
        {
            _dalContext = dalContext;
            _apiIdentity = apiIdentity;
        }

        public async Task<ICollection<NeedDto>> GetNeeds()
        {
            var needs = await _dalContext.Needs.Include(x => x.UserAccountNeeds)
                .Where(x => x.NeedStatus == NeedStatus.NotStarted && x.UserAccountNeeds.Any(y => y.UserAccountId != _apiIdentity.UserAcountId))
                .ToListAsync();

            return needs.Select(x => new NeedDto()
            {
                Category = x.Category,
                DeadlineDate = x.DeadlineDate,
                Description = x.Description,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                NeedId = x.Id,
                NeedStatus = x.NeedStatus
            }).ToList();
        }

        public async Task<NeedDto> CreateNeed(CreateNeedRequestDto requestDto)
        {
            var need = new NeedEntity()
            {
                Description = requestDto.Description,
                DeadlineDate = requestDto.DeadlineDate,
                Category = requestDto.Category,
                Latitude = requestDto.Latitude,
                Longitude = requestDto.Longitude,
                NeedStatus = DAL.Enums.NeedStatus.NotStarted
            };

            _dalContext.Needs.Add(need);
            await _dalContext.SaveChangesAsync();

            var userAccountNeed = new UserAccountNeed()
            {
                NeedId = need.Id,
                UserAccountId = _apiIdentity.UserAcountId,
                Role = DAL.Enums.Role.Needy
            };

            _dalContext.UserAccountNeeds.Add(userAccountNeed);
            await _dalContext.SaveChangesAsync();

            return new NeedDto()
            {
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
            var need = await _dalContext.Needs.SingleOrDefaultAsync(x => x.Id == needId);
            if(need == null)
            {
                throw new Exception("Need not exist");
            }

            var userAccountNeed = new UserAccountNeed()
            {
                NeedId = need.Id,
                UserAccountId = _apiIdentity.UserAcountId,
                Role = DAL.Enums.Role.Volunteer
            };

            need.NeedStatus = DAL.Enums.NeedStatus.InProgress;
            _dalContext.UserAccountNeeds.Add(userAccountNeed);
            _dalContext.Needs.Update(need);
            await _dalContext.SaveChangesAsync();

            return new AssignVolunteerToNeedResponseDto()
            {
                NeedId = need.Id,
                UserAccountId = _apiIdentity.UserAcountId
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
