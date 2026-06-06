using HeroSimulator.Core.Enums;
using System.Text.Json.Serialization;

namespace HeroSimulator.Core.Models.Items
{
    [JsonDerivedType(typeof(Weapon), "Weapon")]
    [JsonDerivedType(typeof(Armor), "Armor")]
    [JsonDerivedType(typeof(Pants), "Pants")]
    [JsonDerivedType(typeof(Boots), "Boots")]
    [JsonDerivedType(typeof(Amulet), "Amulet")]
    [JsonDerivedType(typeof(Ring), "Ring")]
    [JsonDerivedType(typeof(Gem), "Gem")]
    public abstract class Item
    {
        public string Id
        {
            get; set;
        }
        public string Name
        {
            get; set;
        }
        public ItemRarity Rarity
        {
            get; set;
        }
        public int BonusStrength
        {
            get; set;
        }
        public int BonusDexterity
        {
            get; set;
        }
        public int BonusIntelligence
        {
            get; set;
        }
        public int BonusArmour
        {
            get; set;
        }
        public int BonusLuck
        {
            get; set;
        }
        public int Price
        {
            get; set;
        }
        public ItemSlot Slot
        {
            get; set;
        }
        public int MaxSockets
        {
            get; set;
        }
        public List<Gem> SocketedGems
        {
            get; set;
        }

        protected Item(string name, ItemRarity rarity, ItemSlot slot)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Rarity = rarity;
            Slot = slot;
            SocketedGems = new List<Gem>();

            if (slot != ItemSlot.None)
            {
                MaxSockets = rarity == ItemRarity.Rare ? 2 : rarity == ItemRarity.Magic ? 1 : 0;
            }
            else
            {
                MaxSockets = 0;
            }
        }
    }
}