using System;
using Game.Profile;
using UnityEngine;

namespace Game.Engine
{
    public class Bootstrap : MonoBehaviour
    {
        private void Start()
        {
            PlayerProfile playerProfile = new PlayerProfile();
            playerProfile.CreateOrFetchPlayerData();
        }
    }
}
