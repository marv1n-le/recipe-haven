﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SWD.RecipeHaven.Data.Models;

public partial class Recipe
{
    public int RecipeId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public int? PreparationTime { get; set; }

    public int? CookingTime { get; set; }

    public int? Servings { get; set; }

    public string DifficultyLevel { get; set; }

    public string CookingMethod { get; set; }

    public int? CategoryId { get; set; }

    public int? UserId { get; set; }

    public int? OriginId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? LastUpdated { get; set; }

    public bool? ActiveStatus { get; set; }

    public virtual Category Category { get; set; }

    public virtual Origin Origin { get; set; }

    public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();

    public virtual ICollection<Step> Steps { get; set; } = new List<Step>();

    public virtual User User { get; set; }
}