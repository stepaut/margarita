using System;

namespace margarita.Data.Entities.RecipeBook;

public class RecipeComponentEntity
{
    public Guid Id { get; set; }
    public int VolumeInMl { get; set; }
    public bool IsRequired { get; set; }

    public RecipeEntity Recipe { get; set; }
    public IngredientEntity Ingredient { get; set; }
}

public class RecipeComponentAltIngredient
{
    public Guid Id { get; set; }

    public RecipeComponentEntity Component { get; set; }
    public IngredientEntity Ingredient { get; set; }
}
