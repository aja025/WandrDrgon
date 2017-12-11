using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Newtonsoft.Json;
using WanderDragon.Models;

namespace WanderDragon.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class Dragon
    {
        public Dragon()
        {
            this.Birthdate = DateTime.Now;
        }
        public int DragonId { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public string Hometown {get; set;}
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int XP { get; set; }
        public decimal KmRadius { get; set; }
        public decimal KmTravelled { get; set; }
        public int ChallengesWon { get; set; }
        public int ProfileId { get; set; }
        public int DragonSpriteId { get; set; }
        public int TripsTaken { get; set; }
        // public string ItemsCollected { get; set; }

        [JsonIgnore]
        public DragonSprite DragonSprite { get; set; }

        [JsonIgnore]
        public Profile Profile { get; set; }


    }
}
