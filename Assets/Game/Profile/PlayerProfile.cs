using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Profile
{
    public class PlayerProfile
    {
        private UserModel _userModel;

        public Inventory Inventory => _userModel.Inventory;
        public int VendingMachinePlayedCount => _userModel.VendingMachineTries;

        public PlayerProfile()
        {
            CreateOrFetchPlayerData();
        }

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

            _userModel = userModel;

            PrintState();
        }

        private bool IsNewUser()
        {
            return !PlayerPrefs.HasKey("user_profile_data");
        }

        public void PrintState()
        {
            Debug.Log(JsonConvert.SerializeObject(_userModel));
        }

        public void UpdateVendingMachineUsed()
        {
            _userModel.VendingMachineTries += 1;
        }
    }

    public class BaseSingleton<T> where T : class, new()
    {
        private static T _instance;
        public static T Instance => _instance ?? new T();
    }

    public class Inventory
    {
        [JsonProperty("slots")] public Dictionary<int, Slot> Slots { get; private set; }

        [JsonProperty("active")] public readonly int Available;

        public Inventory(int totalSlots)
        {
            Available = totalSlots;
            Slots = new Dictionary<int, Slot>();
            for (int i = 0; i < totalSlots; i++)
            {
                Slots.Add(i + 1, new Slot(i + 1));
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
            Id = id.ToLower();
        }
    }

    public class Slot
    {
        public int Id { get; private set; }
        public InventoryItem Item { get; private set; }
        
        [JsonIgnore] public bool IsEmpty => Item == null;

        public Slot(int id)
        {
            Id = id;
        }

        public Slot AddItem(InventoryItem item)
        {
            Item = item;
            return this;
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
        [JsonProperty("vm_game")] public int VendingMachineTries { get; set; }
    }

    public class Wallet
    {
        [JsonProperty("coins")] public int Coins { get; set; }
    }
}