using System;

namespace margarita.Data.Dto.RecipeBook;

public class RecipeStepDto
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Order { get; set; }
}

