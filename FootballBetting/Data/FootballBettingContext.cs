namespace FootballBetting.Data
{
    using FootballBetting.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class FootballBettingContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Continent> Continents { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }

        public DbSet<Round> Rounnds { get; set; }

        public DbSet<Competition> Competitions { get; set; }

        public DbSet<Bet> Bets { get; set; }

        public DbSet<BetGame> BetsGames { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=MyTempDb;Integrated Security=True;");
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Team>()
                .HasOne(t => t.PrimaryKitColor)
                .WithMany(pc => pc.PrimaryTeams)
                .HasForeignKey(t => t.PrimaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Team>()
                .HasOne(t => t.SecondaryKitColor)
                .WithMany(sc => sc.SecondaryTeams)
                .HasForeignKey(sc => sc.SecondaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Team>()
                .HasOne(t => t.Town)
                .WithMany(town => town.Teams)
                .HasForeignKey(t => t.TownId);

            builder.Entity<Town>()
                .HasOne(t => t.Country)
                .WithMany(c => c.Towns)
                .HasForeignKey(t => t.CountryId);

            builder.Entity<Country>()
                .HasMany(cry => cry.Continents)
                .WithOne(cc => cc.Country)
                .HasForeignKey(cc => cc.CountryId);

            builder.Entity<Continent>()
                .HasMany(ct => ct.Countries)
                .WithOne(cc => cc.Continent)
                .HasForeignKey(cc => cc.ContinentId);

            builder.Entity<Player>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeanId);

            builder.Entity<Player>()
                .HasOne(p => p.Position)
                .WithMany(pn => pn.Players)
                .HasForeignKey(p => p.PositionId);

            builder.Entity<Player>()
                .HasMany(p => p.Games)
                .WithOne(ps => ps.Player)
                .HasForeignKey(ps => ps.PlayerId);

            builder.Entity<Game>()
                .HasMany(g => g.Players)
                .WithOne(ps => ps.Game)
                .HasForeignKey(ps => ps.GameId);

            builder.Entity<Game>()
                .HasOne(g => g.Round)
                .WithMany(r => r.Games)
                .HasForeignKey(g => g.RoundId);

            builder.Entity<Game>()
                .HasOne(g => g.Competition)
                .WithMany(c => c.Games)
                .HasForeignKey(g => g.CompetitionId);

            builder.Entity<Game>()
                .HasMany(g => g.Bets)
                .WithOne(bg => bg.Game)
                .HasForeignKey(bg => bg.GameId);

            builder.Entity<Game>()
                .HasOne(g => g.HomeTeam)
                .WithMany(ht => ht.HomeTeamGames)
                .HasForeignKey(g => g.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Game>()
                .HasOne(g => g.AwayTeam)
                .WithMany(at => at.AwayTeamGames)
                .HasForeignKey(g => g.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Bet>()
                .HasMany(b => b.Games)
                .WithOne(bg => bg.Bet)
                .HasForeignKey(bg => bg.BetId);

            builder.Entity<Bet>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bets)
                .HasForeignKey(b => b.UserId);

            builder.Entity<BetGame>()
                .HasOne(bg => bg.ResultPrediction)
                .WithMany(rp => rp.BetGames)
                .HasForeignKey(bg => bg.ResultPredictionId);

            builder.Entity<BetGame>()
                .HasKey(bg => new { bg.GameId, bg.BetId });

            builder.Entity<CountriesContinets>()
                .HasKey(k => new { k.CountryId, k.ContinentId });

            builder.Entity<PlayerStatistic>()
                .HasKey(k => new { k.GameId, k.PlayerId });

            builder.Entity<Competition>()
                .HasOne(c => c.CompetitionType)
                .WithMany(ct => ct.Competitions)
                .HasForeignKey(c => c.CompetitionTypeId);            
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var serviceProvider = this.GetService<IServiceProvider>();
            var ithems = new Dictionary<object, object>();
            foreach (var entry in this.ChangeTracker.Entries().Where(e => (e.State == EntityState.Added) || (e.State == EntityState.Modified)))
            {
                var entity = entry.Entity;
                var context = new ValidationContext(entity, serviceProvider, ithems);
                var results = new List<ValidationResult>();
                if (Validator.TryValidateObject(entity, context, results, true))
                {
                    foreach (var result in results)
                    {
                        if (result != ValidationResult.Success)
                        {
                            throw new ValidationException(result.ErrorMessage);
                        }
                    }
                }
            }

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
    }
}