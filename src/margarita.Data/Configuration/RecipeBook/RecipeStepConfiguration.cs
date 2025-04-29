//using margarita.Data.Entities.RecipeBook;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace margarita.Data.Configuration.RecipeBook;

//internal class RecipeStepConfiguration : IEntityTypeConfiguration<RecipeStepEntity>
//{
//    public void Configure(EntityTypeBuilder<RecipeStepEntity> builder)
//    {
//        builder.HasKey(x => x.Id);

//        builder.HasOne(step => step.Recipe)
//            .WithMany(recipe => recipe.Steps)
//    }
//}

//internal class RecipeFamilyConfiguration : IEntityTypeConfiguration<RecipeFamilyEntity>
//{
//    public void Configure(EntityTypeBuilder<RecipeFamilyEntity> builder)
//    {
//        builder.HasKey(x => x.Id);

//        builder.HasOne(x => x.Parent)
//            .WithMany(x => x.Childs)
//            .HasForeignKey(x => x.ParentId);
//    }
//}
