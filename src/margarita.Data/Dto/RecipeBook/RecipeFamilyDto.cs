﻿using System;

namespace margarita.Data.Dto.RecipeBook;

public class RecipeFamilyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid? ParentId { get; set; }
}
