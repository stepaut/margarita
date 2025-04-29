using System;

namespace margarita.Data.Dto.RecipeBook;

public class RecipeInfoDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string OriginalName { get; set; } = string.Empty;
}