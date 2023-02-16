using CookBook.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace CookBook.API.Data
{
    public partial class cookbookContext : DbContext
    {
        public cookbookContext()
        {
        }

        public cookbookContext(DbContextOptions<cookbookContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Favorite> Favorites { get; set; } = null!;
        public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;
        public virtual DbSet<Material> Materials { get; set; } = null!;
        public virtual DbSet<Meal> Meals { get; set; } = null!;
        public virtual DbSet<Mealtype> Mealtypes { get; set; } = null!;
        public virtual DbSet<Measures> Meassures { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Token> Tokens { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_hungarian_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Favorite>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("favorites_ibfk_1");
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.HasOne(d => d.Materials)
                    .WithMany(p => p.Ingredients)
                    .HasForeignKey(d => d.MaterialId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ingredients_ibfk_2");

                entity.HasOne(d => d.Meals)
                    .WithMany(p => p.Ingredients)
                    .HasForeignKey(d => d.MealId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ingredients_ibfk_1");
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.HasOne(d => d.UnitOfMeasure)
                    .WithMany(p => p.Materials)
                    .HasForeignKey(d => d.UnitOfMeasureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("materials_ibfk_1");
            });

            modelBuilder.Entity<Meal>(entity =>
            {
                entity.HasOne(d => d.MealType)
                    .WithMany(p => p.Meals)
                    .HasForeignKey(d => d.MealTypeId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("meals_ibfk_2");

                modelBuilder.Entity<Meal>(entity =>
                {
                    entity.HasOne(d => d.User)
                        .WithMany(p => p.Meals)
                        .HasForeignKey(d => d.UserId)
                        .HasConstraintName("feladatok_ibfk_1");
                });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Meals)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("meals_ibfk_1");
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Token)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("tokens_ibfk_1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne(d => d.Role)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("users_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
