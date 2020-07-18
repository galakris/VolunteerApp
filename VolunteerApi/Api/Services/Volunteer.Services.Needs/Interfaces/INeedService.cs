using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volunteer.DAL.Enums;
using Volunteer.Services.Needs.Models;

namespace Volunteer.Services.Needs.Interfaces
{
    public interface INeedService
    {
        Task<ICollection<NeedDto>> GetNeeds(bool my = false);

        Task<CreateNeedResponseDto> CreateNeed(CreateNeedRequestDto requestDto);

        Task<object> AssignVolunteerToNeed(int needId);

        Task<int> DeleteNeed(int needId);

        Task<NeedDto> ModifyNeedStatus(int needId, NeedStatus needStatus);
    }
}
