using System;
using System.Collections.Generic;
using System.Text;
using Volunteer.DAL;
using Volunteer.Services.Needs.Models;

namespace Volunteer.Services.Needs.Services
{
    public class NeedService
    {
        private readonly DalContext _dalContext;

        public ICollection<NeedDto> GetNeeds()
        {
            return new List<NeedDto>();
        }
    }
}
