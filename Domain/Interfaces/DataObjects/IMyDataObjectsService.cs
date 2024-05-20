using Domain.DTOs.DataObjectDTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.DataObjects
{
    public interface IMyDataObjectsService
    {
        public Task<FullDataObjectDTO> GetFullByDisplayedPointId(int pointId);
    }
}
