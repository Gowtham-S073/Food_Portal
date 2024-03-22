using FoodPortal.Models;
using FoodPortal.Models.DTO;
using FoodPortal.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace FoodPortal.Interfaces
{
    public interface ITrackStatusService
    {
        Task<RequestTrackStatus> Add_TrackStatus(RequestTrackStatus TrackStatus);
        Task<TrackDTO> Get_Order_Summary(string id);
        Task<IdDTO> check_trackid(IdDTO id,string username);
    }
}
