using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WanderDragon.Models
{
    public class Profile
    {
        public Profile()
        {
            this.JoinDate = DateTime.Now;
            String.Format("{0:D}", JoinDate);
        }
        public int ProfileId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Your {0} can't be more than {1} characters long.")]
        [Display(Name = "display name")]
        public string DisplayName { get; set; }
        public string AboutMe { get; set; }
        public string Image { get; set; }

        public DateTime JoinDate { get; set; }
    }
}