using System;

namespace margarita.RecipeBook.Models;

public class Ingredient
{
    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public int Alc { get; }
    public Ingredient? Parent { get; }

    public Ingredient(Guid id, string name, string description, int alc, Ingredient? parent)
    {
        Id = id;
        Name = name;
        Description = description;
        Alc = alc;
        Parent = parent;
    }
}
