﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SWD.RecipeHaven.Data.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int UserId { get; set; }

    public int UserSubscriptionId { get; set; }

    public double Amount { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string PaymentMethod { get; set; }

    public string Status { get; set; }

    public bool? ActiveStatus { get; set; }

    public virtual User User { get; set; }

    public virtual UserSubscription UserSubscription { get; set; }
}