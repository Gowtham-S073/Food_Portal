using AutoMapper;
using FoodPortal.Models;
using FoodPortal.RequestModel;
using FoodPortal.ViewModel;

namespace FoodPortal.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AdditionalCategoryMaster, ViewAdditionalCategoryMaster>();
            CreateMap<AdditionalProduct, ViewAdditionalProduct>();
            CreateMap<AddOnsMaster, ViewAddOnsMaster>();
            CreateMap<AllergyMaster, ViewAllergyMaster>();
            CreateMap<DeliveryOption, ViewDeliveryOption>();
            CreateMap<DrinksMaster, ViewDrinksMaster>();
            CreateMap<GroupSize, ViewGroupSize>();
            CreateMap<PlateSize, ViewPlateSize>();
            CreateMap<StdFoodCategoryMaster, ViewStdFoodCategoryMaster>();
            CreateMap<StdProduct, ViewStdProduct>();
            CreateMap<TimeSlot, ViewTimeSlot>();
            CreateMap<RequestAdditionalProductsDetail, AdditionalProductsDetail>().ReverseMap();
            CreateMap<RequestAddOnsDetail, AddOnsDetail>().ReverseMap();
            CreateMap<RequestAllergyDetail, AllergyDetail>().ReverseMap();
            CreateMap<RequestFoodTypeCount, FoodTypeCount>().ReverseMap();
            CreateMap<RequestOrder, Order>().ReverseMap();
            CreateMap<RequestStdFoodOrderDetail, StdFoodOrderDetail>().ReverseMap();
            CreateMap<RequestTrackStatus, TrackStatus>().ReverseMap();
        }
    }
}
