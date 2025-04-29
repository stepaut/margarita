using System;
using System.Collections.Generic;

namespace margarita.Data.Dto.RecipeBook;

public class RecipeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string OriginalName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IReadOnlyCollection<Guid> ComponentsId { get; set; } = [];
    public IReadOnlyCollection<Guid> StepsId { get; set; } = [];
    public Guid? FamilyId { get; set; }
}
