using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Services.Needs.Models;

namespace Volunteer.Services.Needs.Interfaces
{
    public interface INeedService
    {
        Task<ICollection<NeedDto>> GetNeeds();

        Task<NeedDto> CreateNeed(CreateNeedRequestDto requestDto);

        Task<object> AssignVolunteerToNeed(int needId);
    }
}
