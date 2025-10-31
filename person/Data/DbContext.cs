using CoursSupDeVinci;
using Microsoft.EntityFrameworkCore;
namespace TP3
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        // Tes tables
        public DbSet<Classe> Classes { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Details> Details { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Exemple de configuration pour les clés primaires
            modelBuilder.Entity<Classe>()
                .HasKey(c => c.id_classe); // colonne id_classe

            modelBuilder.Entity<Person>()
                .HasKey(p => p.id_person); // colonne id_person

            modelBuilder.Entity<Details>()
                .HasKey(d => d.id_details); // colonne id_detail

            modelBuilder.Entity<Person_details>()
                .HasKey(pd => new { pd.id_person, pd.id_details }); // clé composite

            // Relations
            modelBuilder.Entity<Person>()
                .HasOne<Classe>()
                .WithMany()
                .HasForeignKey(p => p.id_classe);

            modelBuilder.Entity<Person_details>()
                .HasOne<Person>()
                .WithMany()
                .HasForeignKey(pd => pd.id_person);

            modelBuilder.Entity<Person_details>()
                .HasOne<Details>()
                .WithMany()
                .HasForeignKey(pd => pd.id_details);

            // Valeurs par défaut ou contraintes NOT NULL si besoin
            //modelBuilder.Entity<Person>()
            //.Property(p => p.Details)
            //.IsRequired(false); // nullable si tu veux autoriser NULL
        }
    }
}