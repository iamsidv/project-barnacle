using System;
using Game.Engine.Actions;
using Game.Profile;
using UnityEngine;

namespace Game.Engine
{
    public class Bootstrap : MonoBehaviour
    {
        private void Start()
        {
            GameEngine.Execute(new AddInventoryAction(1,  new InventoryItem("Button")));
            GameEngine.Execute(new AddInventoryAction(2,  new InventoryItem("Cap")));
            GameEngine.Execute(new AddInventoryAction(3,  new InventoryItem("MatchBox")));
            GameEngine.Execute(new AddInventoryAction(2,  new InventoryItem("Cap")));
            
            GameEngine.Execute(new LogPlayerState());
        }

        [ContextMenu("Test Player Data String")]
        private void TestPlayerData()
        {
            new PlayerProfile().CreateOrFetchPlayerData();
        }
    }
}
