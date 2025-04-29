using System;

namespace margarita.Data.Dto.RecipeBook;

public class IngredientDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Alc { get; set; }
    public Guid? ParentId { get; set; }
}
