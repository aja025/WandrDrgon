using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WanderDragon.Models;

namespace WanderDragon.Data
{
    public static class DbInitializer
    {
        public static void Initialize(WanderDragonContext context)
        {
            context.Database.EnsureCreated();
            if (!context.DragonSprites.Any())
            {
                AddDragonSprites(context);
            }
            if (!context.ItemSprites.Any())
            {
                AddItemSprites(context);
            }
            if (!context.Profiles.Any())
            {
                AddProfiles(context);
            }
            if (!context.Dragons.Any())
            {
                AddDragons(context);
            }
            if (!context.ItemLogs.Any())
            {
                AddItemLogs(context);
            }
        }

        private static void AddDragonSprites(WanderDragonContext context)
        {
            var dragonSprites = new List<DragonSprite>
            {
                new DragonSprite{ DragonSpriteId = 1, Image1 ="images/dragonA_0.png", Image2 ="images/dragonA_1.png", Image3 ="images/dragonA_2.png"},
                new DragonSprite{ DragonSpriteId = 2, Image1 ="images/dragonB_0.png", Image2 ="images/dragonB_1.png", Image3 ="images/dragonB_2.png" },
                new DragonSprite{ DragonSpriteId = 3, Image1 ="images/dragonC_0.png", Image2 ="images/dragonC_1.png", Image3 ="images/dragonC_2.png" },
                new DragonSprite{ DragonSpriteId = 4, Image1 ="images/dragonD_0.png", Image2 ="images/dragonD_1.png", Image3 ="images/dragonD_2.png" },
                new DragonSprite{ DragonSpriteId = 5, Image1 ="images/dragonE_0.png", Image2 ="images/dragonE_1.png", Image3 ="images/dragonE_2.png" },
                new DragonSprite{ DragonSpriteId = 6, Image1 ="images/dragonF_0.png", Image2 ="images/dragonF_1.png", Image3 ="images/dragonF_2.png" },
                new DragonSprite{ DragonSpriteId = 7, Image1 ="images/dragonG_0.png", Image2 ="images/dragonG_1.png", Image3 ="images/dragonG_2.png" },
                new DragonSprite{ DragonSpriteId = 8, Image1 ="images/dragonH_0.png", Image2 ="images/dragonH_1.png", Image3 ="images/dragonH_2.png" },
                new DragonSprite{ DragonSpriteId = 9, Image1 ="images/dragonI_0.png", Image2 ="images/dragonI_1.png", Image3 ="images/dragonI_2.png" },
                new DragonSprite{ DragonSpriteId = 10, Image1 ="images/dragonJ_0.png", Image2 ="images/dragonJ_1.png", Image3 ="images/dragonJ_2.png" },
                new DragonSprite{ DragonSpriteId = 11, Image1 ="images/dragonK_0.png", Image2 ="images/dragonK_1.png", Image3 ="images/dragonK_2.png" },
                new DragonSprite{ DragonSpriteId = 12, Image1 ="images/dragonL_0.png", Image2 ="images/dragonL_1.png", Image3 ="images/dragonL_2.png" }
            };
            foreach (var sprite in dragonSprites)
            {
                context.DragonSprites.Add(sprite);
            }
            context.SaveChanges();
        }

        private static void AddItemSprites(WanderDragonContext context)
        {
            var sprites = new List<ItemSprite>
            {
                new ItemSprite{ ItemSpriteId = 1, ItemName = "alexandrite", Image = "images/newSprites/alexandrite.png"},
                new ItemSprite{ ItemSpriteId = 2, ItemName = "amethyst", Image = "images/newSprites/amethyst.png"},
                new ItemSprite{ ItemSpriteId = 3, ItemName = "animalmask", Image = "images/newSprites/animalmask.png"},
                new ItemSprite{ ItemSpriteId = 4, ItemName = "aquamarine", Image = "images/newSprites/aquamarine.png"},
                new ItemSprite{ ItemSpriteId = 5, ItemName = "beadednecklace", Image = "images/newSprites/beadednecklace.png"},
                new ItemSprite{ ItemSpriteId = 6, ItemName = "bloodstone", Image = "images/newSprites/bloodstone.png"},
                new ItemSprite{ ItemSpriteId = 7, ItemName = "boomarang", Image = "images/newSprites/boomarang.png"},
                new ItemSprite{ ItemSpriteId = 8, ItemName = "cherryblossom", Image = "images/newSprites/cherryblossom.png"},
                new ItemSprite{ ItemSpriteId = 9, ItemName = "cholla", Image = "images/newSprites/cholla.png"},
                new ItemSprite{ ItemSpriteId = 10, ItemName = "croissant", Image = "images/newSprites/croissant.png"},
                new ItemSprite{ ItemSpriteId = 11, ItemName = "dragongif", Image = "images/newSprites/dragongif.png"},
                new ItemSprite{ ItemSpriteId = 12, ItemName = "garnet", Image = "images/newSprites/garnet.png"},
                new ItemSprite{ ItemSpriteId = 13, ItemName = "hamburger", Image = "images/newSprites/hamburger.png"},
                new ItemSprite{ ItemSpriteId = 14, ItemName = "hockeypuck", Image = "images/newSprites/hockeypuck.png"},
                new ItemSprite{ ItemSpriteId = 15, ItemName = "jade", Image = "images/newSprites/jade.png"},
                new ItemSprite{ ItemSpriteId = 16, ItemName = "koaladoll", Image = "images/newSprites/koaladoll.png"},
                new ItemSprite{ ItemSpriteId = 17, ItemName = "maindragonspritesheet", Image = "images/newSprites/maindragonspritesheet.png"},
                new ItemSprite{ ItemSpriteId = 18, ItemName = "mapleleaf", Image = "images/newSprites/mapleleaf.png"},
                new ItemSprite{ ItemSpriteId = 19, ItemName = "matryoshkadoll", Image = "images/newSprites/matryoshkadoll.png"},
                new ItemSprite{ ItemSpriteId = 20, ItemName = "moonstone", Image = "images/newSprites/moonstone.png"},
                new ItemSprite{ ItemSpriteId = 21, ItemName = "noodlebowl", Image = "images/newSprites/noodlebowl.png"},
                new ItemSprite{ ItemSpriteId = 22, ItemName = "opal", Image = "images/newSprites/opal.png"},
                new ItemSprite{ ItemSpriteId = 23, ItemName = "parrotfeather", Image = "images/newSprites/parrotfeather.png"},
                new ItemSprite{ ItemSpriteId = 24, ItemName = "pearl", Image = "images/newSprites/pearl.png"},
                new ItemSprite{ ItemSpriteId = 25, ItemName = "pierogi", Image = "images/newSprites/pierogi.png"},
                new ItemSprite{ ItemSpriteId = 26, ItemName = "pinacolada", Image = "images/newSprites/pinacolada.png"},
                new ItemSprite{ ItemSpriteId = 27, ItemName = "pineapple", Image = "images/newSprites/pineapple.png"},
                new ItemSprite{ ItemSpriteId = 28, ItemName = "plumeria", Image = "images/newSprites/plumeria.png"},
                new ItemSprite{ ItemSpriteId = 29, ItemName = "sapphire", Image = "images/newSprites/sapphire.png"},
                new ItemSprite{ ItemSpriteId = 30, ItemName = "teabag", Image = "images/newSprites/teabag.png"},
                new ItemSprite{ ItemSpriteId = 31, ItemName = "tigerseye", Image = "images/newSprites/tigerseye.png"},
                new ItemSprite{ ItemSpriteId = 32, ItemName = "topaz", Image = "images/newSprites/topaz.png"},
                new ItemSprite{ ItemSpriteId = 33, ItemName = "turquoise", Image = "images/newSprites/turquoise.png"},
            };
            foreach (var sprite in sprites)
            {
                context.ItemSprites.Add(sprite);
            }
            context.SaveChanges();
        }

        private static void AddProfiles(WanderDragonContext context)
        {
            var profiles = new List<Profile>
            {
                new Profile{ ProfileId = 1, AboutMe = "I am GOD!", DisplayName = "Admin", Image = "https://cdn.filestackcontent.com/XmfXbfWMQ46d04wZQ36d", JoinDate = System.DateTime.Now, UserId = "e106c376-04b3-4a2d-8ee1-bd498d57c8a6", UserName = "admin@wanderdragon.com" }
            };
            foreach (var profile in profiles)
            {
                context.Profiles.Add(profile);
            }
            context.SaveChanges();
        }

        private static void AddDragons(WanderDragonContext context)
        {
            var dragons = new List<Dragon>
            {
                new Dragon
                {
                    DragonId = 1,
                    Birthdate = System.DateTime.Now,
                    ChallengesWon = 0,
                    DragonSpriteId = 1,
                    Hometown = "The Sky",
                    KmRadius = 25.0M,
                    KmTravelled = 0.0M,
                    Latitude = 33.4941704,
                    Longitude = -111.9260519,
                    Name = "Admin",
                    ProfileId = 1,
                    TripsTaken = 0,
                    XP = 0
                }
        };
            foreach (var dragon in dragons)
            {
                context.Dragons.Add(dragon);
            }

            context.SaveChanges();
        }

        private static void AddItemLogs(WanderDragonContext context)
        {
            var itemLogs = new List<ItemLog>
            {
                new ItemLog
                {
                    ItemLogId = 1,
                    ProfileId = 1
                }
        };
            foreach (var itemLog in itemLogs)
            {
                context.ItemLogs.Add(itemLog);
            }

            context.SaveChanges();
        }
    }
}