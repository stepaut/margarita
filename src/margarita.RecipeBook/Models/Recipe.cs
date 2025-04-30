using Mapster;
using margarita.Data.Dto.RecipeBook;
using System;
using System.Collections.Generic;
using System.Linq;

namespace margarita.RecipeBook.Models;

public class Recipe
{
    public Guid Id { get; }
    public string Name { get; }
    public string OriginalName { get; }
    public string Description { get; }
    public IReadOnlyCollection<RecipeComponent> Components { get; }
    public IReadOnlyCollection<RecipeStep> Steps { get; }
    public RecipeFamily? Family { get; }

    public Recipe(Guid id, string name, string originalName, string description, IReadOnlyCollection<RecipeComponent> components, IReadOnlyCollection<RecipeStep> steps, RecipeFamily? family)
    {
        Id = id;
        Name = name;
        OriginalName = originalName;
        Description = description;
        Components = components;
        Steps = steps;
        Family = family;
    }

    public static Recipe Create(RecipeDto dto, RecipeFamily? family, IReadOnlyCollection<Ingredient> ingredients)
    {
        var components = dto.Components.Select(x => RecipeComponent.Create(x, ingredients)).ToList();
        var steps = dto.Steps.Select(x => x.Adapt<RecipeStep>()).ToList();

        return new Recipe(dto.Id, dto.Name, dto.OriginalName, dto.Description, components, steps, family);
    }
}