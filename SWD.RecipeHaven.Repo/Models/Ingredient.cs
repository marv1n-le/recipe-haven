﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SWD.RecipeHaven.Data.Models;

public partial class Ingredient
{
    public int IngredientId { get; set; }

    public string Name { get; set; }

    public string Type { get; set; }

    public int? NutritionInfoId { get; set; } = null;

    public bool? ActiveStatus { get; set; }

    //public virtual NutritionInfo NutritionInfo { get; set; }

    public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();

}