using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WanderDragon.Models
{
    public class ExternalLoginConfirmationModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}