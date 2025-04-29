using System;
using System.Collections.Generic;

namespace margarita.RecipeBook.Models;

public class Recipe
{
    public Guid Id { get; }
    public string Name { get; }
    public string OriginalName { get; }
    public string Description { get; }
    public IReadOnlyCollection<RecipeComponent> Components { get; }
    public IReadOnlyCollection<RecipeStep> Steps { get; }
    public RecipeFamily Family { get; }

    public Recipe(Guid id, string name, string originalName, string description, IReadOnlyCollection<RecipeComponent> components, IReadOnlyCollection<RecipeStep> steps, RecipeFamily family)
    {
        Id = id;
        Name = name;
        OriginalName = originalName;
        Description = description;
        Components = components;
        Steps = steps;
        Family = family;
    }
}