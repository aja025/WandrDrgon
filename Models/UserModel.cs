using System;
using System.Collections.Generic;

namespace WanderDragon.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public Dictionary<string, string> Claims { get; set; }
    }
}