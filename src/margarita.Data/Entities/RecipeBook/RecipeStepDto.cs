using System;

namespace margarita.Data.Entities.RecipeBook;

public class RecipeStepEntity
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Order { get; set; }

    public RecipeEntity Recipe { get; set; }
}

