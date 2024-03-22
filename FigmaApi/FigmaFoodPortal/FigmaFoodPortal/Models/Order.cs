using System;
using System.Collections.Generic;

namespace FoodPortal.Models;

public partial class Order
{
    public int Id { get; set; }

    public string? Oid { get; set; }

    public int? DeliveryOptionId { get; set; }

    public int? PlateSizeId { get; set; }

    public string? AdditionalNote { get; set; }

    public string? Address { get; set; }

    public DateTime? Date { get; set; }

    public int? GroupSizeId { get; set; }

    public int? TimeSlotId { get; set; }

    public string? UserName { get; set; }

    public string? AdditionalAllergy { get; set; }

    public string? ContactName { get; set; }

    public string? ContactNumber { get; set; }

    public string? ContactEmail { get; set; }

    public int? DrinkId { get; set; }

    public virtual ICollection<AddOnsDetail> AddOnsDetails { get; set; } = new List<AddOnsDetail>();

    public virtual ICollection<AdditionalProductsDetail> AdditionalProductsDetails { get; set; } = new List<AdditionalProductsDetail>();

    public virtual ICollection<AllergyDetail> AllergyDetails { get; set; } = new List<AllergyDetail>();

    public virtual DeliveryOption? DeliveryOption { get; set; }

    public virtual DrinksMaster? Drink { get; set; }

    public virtual ICollection<FoodTypeCount> FoodTypeCounts { get; set; } = new List<FoodTypeCount>();

    public virtual GroupSize? GroupSize { get; set; }

    public virtual PlateSize? PlateSize { get; set; }

    public virtual ICollection<StdFoodOrderDetail> StdFoodOrderDetails { get; set; } = new List<StdFoodOrderDetail>();

    public virtual TimeSlot? TimeSlot { get; set; }

    public virtual ICollection<TrackStatus> TrackStatuses { get; set; } = new List<TrackStatus>();

    public virtual User? UserNameNavigation { get; set; }
}
