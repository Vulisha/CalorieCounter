namespace CalorieCounter
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class caloriedatabaseEntities1 : DbContext
    {
        public caloriedatabaseEntities1()
            : base("name=caloriedatabaseEntities1")
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());
            //Database.SetInitializer<caloriedatabaseEntities1>(new DropCreateDatabaseIfModelChanges<caloriedatabaseEntities1>());
            //Database.SetInitializer<caloriedatabaseEntities1>(new CreateDatabaseIfNotExists<caloriedatabaseEntities1>());
        }

        public virtual DbSet<Meal> Meal { get; set; }
        public virtual DbSet<Person> Person { get; set; }

        public virtual DbSet<Food> Food { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Meal)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(true);
        }
    }
}
