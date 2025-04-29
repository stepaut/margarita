using System;
using System.Collections.Generic;

namespace margarita.RecipeBook.Models;

public class RecipeComponent
{
    public Guid Id { get; }
    public Ingredient MainComponent { get; }
    public int VolumeInMl { get; }
    public bool IsRequired { get; }
    public IReadOnlyCollection<Ingredient> AltComponents { get; }

    public RecipeComponent(Guid id, Ingredient mainComponent, int volumeInMl, bool isRequired, IReadOnlyCollection<Ingredient> altComponents)
    {
        Id = id;
        MainComponent = mainComponent;
        VolumeInMl = volumeInMl;
        IsRequired = isRequired;
        AltComponents = altComponents;
    }
}
