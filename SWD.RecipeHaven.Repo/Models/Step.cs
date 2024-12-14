﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SWD.RecipeHaven.Data.Models;

public partial class Step
{
    public int StepId { get; set; }

    public int RecipeId { get; set; } 

    public int StepNumber { get; set; }

    public string Description { get; set; }

    public string? Image { get; set; } = null;

    public int? Duration { get; set; }

    public string ToolsRequired { get; set; }

    public virtual Recipe Recipe { get; set; }
}