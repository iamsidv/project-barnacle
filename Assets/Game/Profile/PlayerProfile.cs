using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Profile
{
    public class PlayerProfile
    {
        private UserModel _userModel;

        public void CreateOrFetchPlayerData()
        {
            if (IsNewUser())
            {
                CreateUserProfile();
            }
        }

        private void CreateUserProfile()
        {
            int defaultSlots = 8;
            UserModel userModel = new UserModel
            {
                Name = "DefaultUser",
                Inventory = new Inventory(defaultSlots),
                Wallet = new Wallet
                {
                    Coins = 100
                }
            };

            userModel.Inventory.AddItem(1, new InventoryItem("Button"));
            userModel.Inventory.AddItem(2, new InventoryItem("Cap"));
            userModel.Inventory.AddItem(3, new InventoryItem("MatchBox"));
            
            _userModel = userModel;
            
            Debug.Log(JsonConvert.SerializeObject(userModel));
        }

        private bool IsNewUser()
        {
            return !PlayerPrefs.HasKey("user_profile_data");
        }
    }

    public class BaseSingleton<T> where T : class, new()
    {
        private static T instance;
        public static T Instance => instance ?? new T();
    }

    public class Inventory
    {
        [JsonProperty("slots")] public List<Slot> Slots { get; private set; }

        [JsonProperty("active")] public readonly int Available;

        public Inventory(int totalSlots)
        {
            Available = totalSlots;
            Slots = new List<Slot>();
            for (int i = 0; i < totalSlots; i++)
            {
                Slots.Add(new Slot(i + 1));
            }
        }

        public void AddItem(int slotId, InventoryItem item)
        {
            foreach (Slot slot in Slots)
            {
                if (slot.Id == slotId && slot.IsAvailable)
                {
                    slot.Item = item;
                    break;
                }
            }
        }
    }

    public class InventoryItem
    {
        public string Id;

        [JsonProperty("loc_id")] public string LocalisationId;
        [JsonProperty("loc_name")] public string LocalisedName;

        public InventoryItem(string id)
        {
            Id = id;
        }
    }

    public class Slot
    {
        public int Id;
        public InventoryItem Item;
        [JsonIgnore] public bool IsAvailable => Item == null;

        public Slot(int id)
        {
            Id = id;
        }

        public void AddItem(InventoryItem item)
        {
        }

        public void RemoveItem()
        {
            Item = null;
        }
    }

    public class UserModel
    {
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("inventory")] public Inventory Inventory { get; set; }
        [JsonProperty("wallet")] public Wallet Wallet { get; set; }
    }

    public class Wallet
    {
        [JsonProperty("coins")] public int Coins { get; set; }
    }
}