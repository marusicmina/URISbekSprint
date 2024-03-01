using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SprintMicroService.Entites;
using System;

namespace SprintMicroService.Entities
{
    public class SprintContext : DbContext
    {
        private readonly IConfiguration configuration;

        public SprintContext(DbContextOptions<SprintContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<Phase> Phases { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Ako želite koristiti IConfiguration za konfiguraciju veze
            //if (!optionsBuilder.IsConfigured)
            //{
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("SprintDB"));
            //}
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Phase>()
                .HasOne(e => e.Sprint)
                .WithMany(e => e.Phases)
                .HasForeignKey(fk => fk.SprintId);


            builder.Entity<Sprint>()
                .HasData(new //Sprint
                {
                    SprintId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    SprintName = "Sprint 1",
                    // SprintStartDate = new DateTime(2024, 2, 1), // Set your start date
                    //SprintEndDate = new DateTime(2024, 2, 15)
                    SprintGoals = "Ciljevi prvog sprinta",
                    SprintDescription = "Opis prvog sprinta",
                    SprintStartDate = DateTimeOffset.Parse("2023-02-12 00:00:00"),
                    SprintEndDate = DateTimeOffset.Parse("2023-02-13 00:00:00"),

                    SuggestionsForNextSprint = "Sugestije za prvi sprint",
                    SprintNotes = "Napomene o prvom sprintu",
                    GoalsAchieved = true
                });

            builder.Entity<Sprint>()
                .HasData(new //Sprint
                {
                    SprintId = Guid.Parse("f47c88b9-70e4-4b1b-bb70-1f02c2f5a5e1"),
                    SprintName = "Sprint 2",  // Izmenjeno na Sprint 2
                    SprintGoals = "Ciljevi drugog sprinta",  // Izmenjeno na Ciljevi drugog sprinta
                    SprintDescription = "Opis drugog sprinta",  // Izmenjeno na Opis drugog sprinta
                    SprintStartDate = DateTimeOffset.Parse("2023-02-12 00:00:00"),
                    SprintEndDate = DateTimeOffset.Parse("2023-02-13 00:00:00"),

                    SuggestionsForNextSprint = "Sugestije za drugi sprint",  // Izmenjeno na Sugestije za drugi sprint
                    SprintNotes = "Napomene o drugom sprintu",  // Izmenjeno na Napomene o drugom sprintu
                    GoalsAchieved = true
                });

            builder.Entity<Phase>()
      .HasData(new
      {
          PhaseId = Guid.Parse("a45e2e02-38f6-4a56-94f5-74a1cf751087"),
          PhaseName = "Planning",
          PhaseDescription = "Planning phase for project X",
          PhaseStartDate = DateTimeOffset.Parse("2023-02-12 00:00:00"),
          PhaseEndDate = DateTimeOffset.Parse("2023-02-13 00:00:00"),

          PhaseNotes = "Notes for the planning phase",
          PhaseGoalsAchieved = false,
          SprintId = Guid.Parse("f47c88b9-70e4-4b1b-bb70-1f02c2f5a5e1")
      });

            builder.Entity<Phase>()
                .HasData(new
                {
                    PhaseId = Guid.Parse("4b3f8c15-2ee8-46c9-8e25-29d7a3977f92"),
                    PhaseName = "Execution",
                    PhaseDescription = "Execution phase for project XYZ",
                    PhaseStartDate = DateTimeOffset.Parse("2023-02-12 00:00:00"),
                    PhaseEndDate = DateTimeOffset.Parse("2023-02-13 00:00:00"),

                    PhaseNotes = "Notes for the execution phase",
                    PhaseGoalsAchieved = true,
                    SprintId = Guid.Parse("f47c88b9-70e4-4b1b-bb70-1f02c2f5a5e1")
                });

        }
    }

}
