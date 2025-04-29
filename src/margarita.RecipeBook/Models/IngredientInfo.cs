using System;

namespace margarita.RecipeBook.Models;

public class IngredientInfo
{
    public Guid Id { get; }
    public string Name { get; }

    public IngredientInfo(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
