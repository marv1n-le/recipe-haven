﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SWD.RecipeHaven.Data.Models;

public partial class RecipeIngredient
{
    public int RecipeId { get; set; }

    public int IngredientId { get; set; }

    public double? Quantity { get; set; }

    public string Unit { get; set; }

    public virtual Ingredient Ingredient { get; set; }

    public virtual Recipe Recipe { get; set; }
}