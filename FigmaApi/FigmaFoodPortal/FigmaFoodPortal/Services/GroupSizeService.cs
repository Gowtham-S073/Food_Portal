using AutoMapper;
using FoodPortal.Interfaces;
using FoodPortal.Models;
using FoodPortal.Models.DTO;
using FoodPortal.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FoodPortal.Services
{
    public class GroupSizeService : IGroupSizeService
    {
        #region Field
        private readonly ICrud<GroupSize, IdDTO> _groupSizeRepo;
        private readonly IMapper _mapper;
        #endregion

        #region Parameterized Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupSizeRepo"></param>
        public GroupSizeService(ICrud<GroupSize, IdDTO> groupSizeRepo, IMapper mapper)
        {
            _groupSizeRepo = groupSizeRepo;
            _mapper = mapper;
        }
        #endregion

        #region Service method to get all the group size details
        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of Group size</returns>
        public async Task<List<ViewGroupSize>> View_All_GroupSizes()
        {
            var GroupSizes = await _groupSizeRepo.GetAll();
            List<ViewGroupSize> viewResult = _mapper.Map<List<ViewGroupSize>>(GroupSizes);
            return viewResult;
        }
        #endregion
    }
}
