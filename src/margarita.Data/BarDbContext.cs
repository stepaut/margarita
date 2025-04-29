using margarita.Data.Entities.RecipeBook;
using Microsoft.EntityFrameworkCore;
using System;

namespace margarita.Data;

public class BarDbContext : DbContext
{
    public DbSet<RecipeStepEntity> RecipeSteps { get; set; } = null!;
    public DbSet<RecipeFamilyEntity> RecipeFamilies { get; set; } = null!;
    public DbSet<RecipeEntity> Recipes { get; set; } = null!;
    public DbSet<RecipeComponentEntity> RecipeComponents { get; set; } = null!;
    public DbSet<IngredientEntity> Ingredients { get; set; } = null!;
    public DbSet<RecipeComponentAltIngredient> RecipeComponentAltIngredients { get; set; } = null!;

    public BarDbContext(DbContextOptions<BarDbContext> dbOptions) : base(dbOptions)
    {
        Database.EnsureCreated();
    }

    public void Test()
    {
        var recipeFamily2 = new RecipeFamilyEntity() { Id = Guid.NewGuid(), Name = "Test family 2", Description = "Test 2" };
        var recipeFamily = new RecipeFamilyEntity() { Id = Guid.NewGuid(), Name = "Test family", Description = "Test" };

        recipeFamily.Parent = recipeFamily2;

        var recipe = new RecipeEntity() { Id = Guid.NewGuid(), Name = "Маргарита", OriginalName = "Margarita", Description = "Test" };

        recipe.Family = recipeFamily;

        var step = new RecipeStepEntity() { Id = Guid.NewGuid(), Order = 0, Description = "Make coctail" };

        step.Recipe = recipe;

        var ingr0 = new IngredientEntity() { Id = Guid.NewGuid(), Alc = 0, Name = "Common", Description = "Test" };
        var ingr1 = new IngredientEntity() { Id = Guid.NewGuid(), Alc = 0, Name = "Weter", Description = "Test" };
        var ingr2 = new IngredientEntity() { Id = Guid.NewGuid(), Alc = 0, Name = "Spring Weter", Description = "Test" };
        var ingr3 = new IngredientEntity() { Id = Guid.NewGuid(), Alc = 40, Name = "Vodka", Description = "Test" };

        ingr1.Parent = ingr0;
        ingr2.Parent = ingr0;

        var comp1 = new RecipeComponentEntity() { Id = Guid.NewGuid(), IsRequired = true, VolumeInMl = 10 };

        comp1.Ingredient = ingr3;
        comp1.Recipe = recipe;

        var comp2 = new RecipeComponentEntity() { Id = Guid.NewGuid(), IsRequired = true, VolumeInMl = 20 };

        comp2.Ingredient = ingr1;
        comp2.Recipe = recipe;

        var conn = new RecipeComponentAltIngredient();
        conn.Ingredient = ingr2;
        conn.Component = comp2;

        RecipeSteps.Add(step);
        RecipeFamilies.Add(recipeFamily2);
        RecipeFamilies.Add(recipeFamily);
        Recipes.Add(recipe);
        RecipeComponents.Add(comp1);
        RecipeComponents.Add(comp2);
        Ingredients.Add(ingr2);
        RecipeComponentAltIngredients.Add(conn);

        SaveChanges();
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlite("Data Source=C:\\margarita\\margarita.db");
    //}


    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.ApplyConfiguration(new RecipeStepConfiguration());
    //    modelBuilder.ApplyConfiguration(new RecipeFamilyConfiguration());

    //    base.OnModelCreating(modelBuilder);
    //}
}
