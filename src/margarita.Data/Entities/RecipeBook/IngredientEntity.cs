using System;

namespace margarita.Data.Entities.RecipeBook;

public class IngredientEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Alc { get; set; }

    public IngredientEntity? Parent { get; set; }
}
