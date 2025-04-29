using System;

namespace margarita.Data.Entities.RecipeBook;

public class RecipeEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string OriginalName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public RecipeFamilyEntity? Family { get; set; }
}
