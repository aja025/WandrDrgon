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
    public class TravelLog{
        public int TravelLogId { get; set; }
        public int ProfileId { get; set; }
        public string Location { get; set; }

        [JsonIgnore]
        public Profile Profile { get; set; }

    }
}