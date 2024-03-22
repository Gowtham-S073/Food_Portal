using FoodPortal.Exceptions;
using FoodPortal.Interfaces;
using FoodPortal.Models.DTO;
using FoodPortal.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FoodPortal.Repos
{
    public class OrderRepo : ICrud<Order, IdDTO>
    {
        #region Field
        private readonly FoodPortal4Context _context;
        #endregion

        #region Parameterized constructor
        public OrderRepo(FoodPortal4Context context)
        {
            _context = context;
        }
        #endregion

        #region Repo method to add the order detail to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Added the order details</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="DuplicateRecordException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<Order> Add(Order item)
        {
            try
            {
                if (item == null)
                {
                    throw new NullException("Order details must not be empty");
                }
                var newOrder = _context.Orders.FirstOrDefault(h => h.Id == item.Id);
                if (newOrder == null)
                {
                    await _context.Orders.AddAsync(item);
                    await _context.SaveChangesAsync();
                    return item;
                }

                throw new DuplicateRecordException("this Order details is already exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to delete order from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>deleted the order</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<Order> Delete(IdDTO item)
        {
            try
            {
                var Orders = await _context.Orders.ToListAsync();
                var myOrder = Orders.FirstOrDefault(h => h.Id == item.IdInt);
                if (myOrder != null)
                {
                    _context.Orders.Remove(myOrder);
                    await _context.SaveChangesAsync();
                    return myOrder;
                }
                throw new NullException("this Order does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to get all the order from the database
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Get orders</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<List<Order>> GetAll()
        {
            try
            {
                var Orders = await _context.Orders.ToListAsync();
                if (Orders != null)
                    return Orders;
                throw new NullException("Order table is empty");

            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to get order from the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>get all orders</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<Order> GetValue(IdDTO item)
        {
            try
            {
                var Orders = await _context.Orders.ToListAsync();
                var Order = Orders.SingleOrDefault(h => h.Id == item.IdInt);
                if (Order != null)
                    return Order;
                throw new NullException("this Order does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion

        #region Repo method to update the order to the database
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Updated the order</returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="InvalidSqlException"></exception>
        public async Task<Order> Update(Order item)
        {
            try
            {
                var Orders = await _context.Orders.ToListAsync();
                var Order = Orders.SingleOrDefault(h => h.Id == item.Id);
                if (Order != null)
                {
                    Order.DeliveryOptionId = item.DeliveryOptionId != null ? item.DeliveryOptionId : Order.DeliveryOptionId;
                    Order.PlateSizeId = item.PlateSizeId != null ? item.PlateSizeId : Order.PlateSizeId;
                    Order.AdditionalNote = item.AdditionalNote != null ? item.AdditionalNote : Order.AdditionalNote;
                    Order.Address = item.Address != null ? item.Address : Order.Address;
                    Order.Date = item.Date != null ? item.Date : Order.Date;
                    Order.GroupSizeId = item.GroupSizeId != null ? item.GroupSizeId : Order.GroupSizeId;
                    Order.TimeSlotId = item.TimeSlotId != null ? item.TimeSlotId : Order.TimeSlotId;
                    Order.UserName = item.UserName != null ? item.UserName : Order.UserName;
                    Order.AdditionalAllergy = item.AdditionalAllergy != null ? item.AdditionalAllergy : Order.AdditionalAllergy;
                    _context.Orders.Update(Order);
                    await _context.SaveChangesAsync();
                    return Order;
                }
                throw new NullException("this Order does not exist");
            }
            catch (Exception ex)
            {
                throw new InvalidSqlException(ex.Message);
            }
        }
        #endregion
    }
}
