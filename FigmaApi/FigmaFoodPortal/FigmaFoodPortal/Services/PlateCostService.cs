using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using FoodPortal.Exceptions;
#nullable disable


namespace FoodPortal.Services
{
    public class PlateCostService : IPlateCostService
    {
        private readonly ICrud<TrackStatus, IdDTO> _trackStatusRepo;
        private readonly ICrud<Order, IdDTO> _orderRepo;
        private readonly ICrud<PlateSize, IdDTO> _plateSizeRepo;
        private readonly ICrud<FoodTypeCount, IdDTO> _FoodTypeCountRepo;
        private List<TrackStatus> track = new List<TrackStatus>();
        private List<Order> order = new List<Order>();
        private List<PlateSize> plate = new List<PlateSize>();
        private List<FoodTypeCount> foodcount = new List<FoodTypeCount>();

        public PlateCostService(ICrud<TrackStatus, IdDTO> trackStatusRepo, ICrud<Order, IdDTO> orderRepo, ICrud<PlateSize, IdDTO> plateSizeRepo, ICrud<FoodTypeCount, IdDTO> foodTypeCountRepo)
        {
            _trackStatusRepo = trackStatusRepo;
            _orderRepo = orderRepo;
            _plateSizeRepo = plateSizeRepo;
            _FoodTypeCountRepo = foodTypeCountRepo;
        }

        public async Task Setvalue()
        {
            order = await _orderRepo.GetAll();
            track = await _trackStatusRepo.GetAll();
            plate = await _plateSizeRepo.GetAll();
            foodcount = await _FoodTypeCountRepo.GetAll();
        }

        #region Service method to get the Count
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns> the count of the food type</returns>
        /// <exception cref="NullException"></exception>
        public async Task<int> calculateCount(string id)
        {
            await Setvalue();
            if (order == null || track == null || plate == null || foodcount == null)
            {
                throw new NullException("No details are there for this track id");
            }
            var count = (from o in order
                         join ts in track on o.Id equals ts.OrderId
                         join p in plate on o.PlateSizeId equals p.Id
                         join f in foodcount on o.Id equals f.OrderId
                         where ts.Tid == id
                         select o).Count();
            return count;
        }
        #endregion

        #region Service method to get the food type
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true or false based on the food type</returns>
        /// <exception cref="NullException"></exception>
        public bool GetIsveg(string id)
        {

            if (order == null || track == null || plate == null || foodcount == null)
            {
                throw new NullException("No details are there for this track id");
            }
            var isveg = (from f in foodcount
                         join p in plate on f.PlateSizeId equals p.Id
                         join o in order on p.Id equals o.PlateSizeId
                         join ts in track on o.Id equals ts.OrderId
                         where ts.Tid == id
                         select f.IsVeg).FirstOrDefault();
            return (bool)isveg;
        }
        #endregion

        #region Service method to Calculate the price 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="priceSelector"></param>
        /// <returns>the price</returns>
        /// <exception cref="NullException"></exception>
        public T CalculatePrice<T>(string id, Func<PlateSize, T> priceSelector)
        {
            if (order == null || track == null || plate == null || foodcount == null)
            {
                throw new NullException("No details are there for this track id");
            }
            var price = (from f in foodcount
                         join p in plate on f.PlateSizeId equals p.Id // Corrected variable name
                         join o in order on p.Id equals o.PlateSizeId
                         join ts in track on o.Id equals ts.OrderId
                         where ts.Tid == id
                         select priceSelector(p)).FirstOrDefault();

            return price;
        }
        #endregion

        #region Service method to get the palte cost of the order
        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <param name="id"></param>
        /// <returns>the palte cost of the order</returns>
        public PlatesizeDTO GetPlatecostWithFoodType(int count, string id)
        {
            PlatesizeDTO platewithcost = new PlatesizeDTO();

            if (count == 2)
            {
                decimal? price = CalculatePrice(id, p => p.BothCost);
                platewithcost.Cost = price;
                platewithcost.foodtype = "Veg & Non-Veg";
                return platewithcost;
            }
            else if (count == 1)
            {
                var isveg = GetIsveg(id);
                if (isveg == true)
                {
                    decimal? price = CalculatePrice(id, p => p.VegPlateCost);
                    platewithcost.Cost = price;
                    platewithcost.foodtype = "Veg";
                    return platewithcost;
                }
                else
                {
                    decimal? price = CalculatePrice(id, p => p.NonvegPlateCost);
                    platewithcost.Cost = price;
                    platewithcost.foodtype = "Non-Veg";
                }
            }
            return platewithcost != null ? platewithcost : throw new NullException("No details are there for this track id");
        }
        #endregion

        #region Service method to get the plate size details of the order
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the plate size details of the order</returns>
        /// <exception cref="NullException"></exception>
        public string GetPlatesize(string id)
        {
            if (order == null || track == null || plate == null)
            {
                throw new NullException("No details are there for this track id");
            }
            var PlateSize = (from p in plate
                             join o in order on p.Id equals o.PlateSizeId
                             join t in track on o.Id equals t.OrderId
                             where t.Tid == id
                             select p.PlateType).FirstOrDefault();
            return PlateSize;
        }
        #endregion
        #region  Service method to get the guest count of the order
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the guest count</returns>
        /// <exception cref="NullException"></exception>
        public int GetGuestCount(string id)
        {
            if (order == null || track == null || foodcount == null || plate == null)
            {
                throw new NullException("No details are there for this track id");
            }
            int? Count = (from o in order
                          join ts in track on o.Id equals ts.OrderId
                          join p in plate on o.PlateSizeId equals p.Id
                          join f in foodcount on o.Id equals f.OrderId
                          where ts.Tid == id
                          select f.FoodTypeCount1).Sum();
            return (int)Count;
        }
        #endregion
    }
}
