using System;

namespace margarita.Data.Entities.RecipeBook;

public class RecipeFamilyEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public RecipeFamilyEntity? Parent { get; set; }
}
