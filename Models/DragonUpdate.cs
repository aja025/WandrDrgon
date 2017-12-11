using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Newtonsoft.Json;
using WanderDragon.Models;

namespace WanderDragon.Data
{
    public class DragonUpdate{
        public int Xp { get; set; }
        public decimal KmRadius { get; set; }
        public decimal KmTravelled { get; set; }
        public int ChallengesWon { get; set; }
        public int TripsTaken { get; set; }

    }
}