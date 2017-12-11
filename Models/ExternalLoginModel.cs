using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WanderDragon.Models
{
    public class ExternalLoginModel
    {
        public string AuthenticationScheme { get; set; }

        public string DisplayName { get; set; }
    }
}