using System;

namespace margarita.RecipeBook.Models;

public class RecipeStep
{
    public Guid Id { get; }
    public string Description { get; }
    public int Order { get; }

    public RecipeStep(Guid id, string description, int order)
    {
        Id = id;
        Description = description;
        Order = order;
    }
}

