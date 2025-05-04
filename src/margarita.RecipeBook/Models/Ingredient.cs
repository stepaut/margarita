using Mapster;
using margarita.Data.Dto.RecipeBook;
using System;
using System.Collections.Generic;

namespace margarita.RecipeBook.Models;

public class Ingredient
{
    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public int Alc { get; }
    public Ingredient? Parent { get; set; }

    public Ingredient(Guid id, string name, string description, int alc, Ingredient? parent)
    {
        Id = id;
        Name = name;
        Description = description;
        Alc = alc;
        Parent = parent;
    }

    public static IReadOnlyCollection<Ingredient> Create(IReadOnlyCollection<IngredientDto> dtos)
    {
        var res = new List<Ingredient>();
        var dict = new Dictionary<Guid, Ingredient>();
        var dictDtos = new Dictionary<Guid, IngredientDto>();

        foreach (var dto in dtos)
        {
            dict.Add(dto.Id, dto.Adapt<Ingredient>());
            dictDtos.Add(dto.Id, dto);
        }

        foreach (var ing in dict.Values)
        {
            var parentId = dictDtos[ing.Id].ParentId;
            var parent = !parentId.HasValue ? null : dict[parentId.Value];
            ing.Parent = parent;
            res.Add(ing);
        }

        return res;
    }
}
