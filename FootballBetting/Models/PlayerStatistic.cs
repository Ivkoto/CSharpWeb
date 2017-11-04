﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballBetting.Models
{
    public class PlayerStatistic
    {        
        public int GameId { get; set; }
        
        public Game Game { get; set; }
        
        public int PlayerId { get; set; }
        
        public Player Player { get; set; }

        public int ScoredGoals { get; set; }

        public int PlayerAssists { get; set; }

        public int PlayedGameMinutes { get; set; }
    }
}