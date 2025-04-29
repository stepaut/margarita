using System;

namespace margarita.RecipeBook.Models;

public class RecipeInfo
{
    public Guid Id { get; }
    public string Name { get; }
    public string OriginalName { get; }

    public RecipeInfo(Guid id, string name, string originalName)
    {
        Id = id;
        Name = name;
        OriginalName = originalName;
    }
}
