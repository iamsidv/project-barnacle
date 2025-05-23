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
            };

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
        public readonly List<Slot> Slots;
        public readonly int SlotsAvailable;

        public Inventory(int defaultSlots)
        {
            SlotsAvailable = defaultSlots;
            Slots = new List<Slot>();
        }
    }

    public class InventoryItem
    {
        public int Id;
        public string LocalisationId;
        public string LocalisedName;
    }

    public class Slot
    {
        public int SlotIndex;
        public InventoryItem Item;
        public bool IsAvailable => Item != null;
    }

    public class UserModel
    {
        public string Name;
        public Inventory Inventory;
    }
}