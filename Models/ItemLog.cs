using System;
using Newtonsoft.Json;

namespace WanderDragon.Models
{
    public class ItemLog
    {
        public int ItemLogId { get; set; }
        public int Alexandrite { get; set; }
        public int Amethyst { get; set; }
        public int AnimalMask { get; set; }
        public int Aquamarine { get; set; }
        public int BeadedNecklace { get; set; }
        public int Bloodstone { get; set; }
        public int Boomarang { get; set; }
        public int Cherryblossom { get; set; }
        public int Cholla { get; set; }
        public int Croissant { get; set; }
        public int Garnet { get; set; }
        public int Hamburger { get; set; }
        public int HockeyPuck { get; set; }
        public int Jade { get; set; }
        public int KoalaDoll { get; set; }
        public int MapleLeaf { get; set; }
        public int MatryoshkaDoll { get; set; }
        public int Moonstone { get; set; }
        public int Noodlebowl { get; set; }
        public int Opal { get; set; }
        public int ParrotFeather { get; set; }
        public int Pearl { get; set; }
        public int Pierogi { get; set; }
        public int PinaColada { get; set; }
        public int Pineapple { get; set; }
        public int Plumeria { get; set; }
        public int Sapphire { get; set; }
        public int TeaBag { get; set; }
        public int TigersEye { get; set; }
        public int Topaz { get; set; }
        public int Turquoise { get; set; }
        public int ProfileId { get; set; }

        [JsonIgnore]
        public Profile Profile { get; set; }

    }
}