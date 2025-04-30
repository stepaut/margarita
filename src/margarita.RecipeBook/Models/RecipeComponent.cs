using margarita.Data.Dto.RecipeBook;
using System;
using System.Collections.Generic;
using System.Linq;

namespace margarita.RecipeBook.Models;

public class RecipeComponent
{
    public Guid Id { get; }
    public Ingredient MainIngredient { get; }
    public int VolumeInMl { get; }
    public bool IsRequired { get; }
    public IReadOnlyCollection<Ingredient> AltIngredients { get; }

    public RecipeComponent(Guid id, Ingredient mainIngredient, int volumeInMl, bool isRequired, IReadOnlyCollection<Ingredient> altIngredients)
    {
        Id = id;
        MainIngredient = mainIngredient;
        VolumeInMl = volumeInMl;
        IsRequired = isRequired;
        AltIngredients = altIngredients;
    }

    public static RecipeComponent Create(RecipeComponentDto dto, IReadOnlyCollection<Ingredient> ingredients)
    {
        Ingredient? mainIngredient = null;
        var altIngredients = new List<Ingredient>();

        //TODO bruh
        foreach (var ingredient in ingredients)
        {
            if (ingredient.Id == dto.IngredientId)
            {
                mainIngredient = ingredient;
            }

            if (dto.AltIngredientsId.Contains(ingredient.Id))
            {
                altIngredients.Add(ingredient);
            }
        }

        return new RecipeComponent(dto.Id, mainIngredient!, dto.VolumeInMl, dto.IsRequired, altIngredients);
    }
}
