using Mapster;
using margarita.Data.Dto.RecipeBook;
using System;
using System.Collections.Generic;

namespace margarita.RecipeBook.Models;

public class RecipeFamily
{
    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public RecipeFamily? Parent { get; set; }

    public RecipeFamily(Guid id, string name, string description, RecipeFamily? parent)
    {
        Id = id;
        Name = name;
        Description = description;
        Parent = parent;
    }

    public static IReadOnlyCollection<RecipeFamily> Create(IReadOnlyCollection<RecipeFamilyDto> dtos)
    {
        //TODO copy
        var res = new List<RecipeFamily>();
        var dict = new Dictionary<Guid, RecipeFamily>();
        var dictDtos = new Dictionary<Guid, RecipeFamilyDto>();

        foreach (var dto in dtos)
        {
            dict.Add(dto.Id, dto.Adapt<RecipeFamily>());
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

