using System;

namespace margarita.RecipeBook.Models;

public class RecipeFamily
{
    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public RecipeFamily? Parent { get; }

    public RecipeFamily(Guid id, string name, string description, RecipeFamily? parent)
    {
        Id = id;
        Name = name;
        Description = description;
        Parent = parent;
    }
}

