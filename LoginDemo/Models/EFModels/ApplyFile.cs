﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace LoginDemo.Models.EFModels;

public partial class ApplyFile
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string FilePath { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Users User { get; set; }
}