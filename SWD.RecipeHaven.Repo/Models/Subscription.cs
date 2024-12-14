﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using SWD.RecipeHaven.Repo.responseDTOs;
using System;
using System.Collections.Generic;

namespace SWD.RecipeHaven.Data.Models;

public partial class Subscription
{
    public int SubscriptionId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public double Price { get; set; }

    public int Duration { get; set; }

    public bool? ActiveStatus { get; set; }

    public virtual ICollection<UserSubscription> UserSubscriptions { get; set; } = new List<UserSubscription>();
}