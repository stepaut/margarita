﻿using System;
using System.Collections.Generic;

namespace margarita.Data.Dto.RecipeBook;

public class RecipeComponentDto
{
    public Guid Id { get; set; }
    public Guid IngredientId { get; set; }
    public int VolumeInMl { get; set; }
    public bool IsRequired { get; set; }
    public IReadOnlyCollection<Guid> AltIngredientsId { get; set; } = [];
}
