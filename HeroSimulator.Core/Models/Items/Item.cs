using HeroSimulator.Core.Enums;
using System;
using System.Net.NetworkInformation;
using System.Text.Json.Serialization;
using static System.Reflection.Metadata.BlobBuilder;

namespace HeroSimulator.Core.Models.Items
{
    [JsonDerivedType(typeof(Weapon), "Weapon")]
    [JsonDerivedType(typeof(Armor), "Armor")]
    [JsonDerivedType(typeof(Pants), "Pants")]
    [JsonDerivedType(typeof(Boots), "Boots")]
    [JsonDerivedType(typeof(Amulet), "Amulet")]
    [JsonDerivedType(typeof(Ring), "Ring")]
    public abstract class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ItemRarity Rarity { get; set; }
        public int BonusStrength { get; set; }
        public int BonusDexterity { get; set; }
        public int BonusIntelligence { get; set; }
        public int BonusArmour { get; set; }
        public int Price { get; set; }

        protected Item(string name, ItemRarity rarity)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Rarity = rarity;
        }
    }
}