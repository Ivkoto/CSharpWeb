﻿namespace FootballBetting.Models
{
    using System;
    using System.Collections.Generic;

    public class Game
    {
        public int Id { get; set; }

        public int HomeTeamId { get; set; }

        public Team HomeTeam { get; set; }

        public int AwayTeamId { get; set; }

        public Team AwayTeam { get; set; }

        public int HomeGoals { get; set; }

        public int AwayGoals { get; set; }

        public DateTime GameDate { get; set; }

        public double HomeWinBetRate { get; set; }

        public double AwayWinBetRate { get; set; }

        public double DrawGameBetRate { get; set; }

        public int RoundId { get; set; }

        public Round Round { get; set; }

        public int CompetitionId { get; set; }

        public Competition Competition { get; set; }

        public ICollection<PlayerStatistic> Players { get; set; } = new List<PlayerStatistic>();

        public ICollection<BetGame> Bets { get; set; } = new List<BetGame>();
    }
}