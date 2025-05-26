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
            GameEngine.Execute(new AddInventoryAction(4,  new InventoryItem("winecork")));
            GameEngine.Execute(new AddInventoryAction(5,  new InventoryItem("wire")));
            GameEngine.Execute(new AddInventoryAction(6,  new InventoryItem("Cap")));
            GameEngine.Execute(new AddInventoryAction(7,  new InventoryItem("Cap")));
            GameEngine.Execute(new AddInventoryAction(8,  new InventoryItem("Cap")));
            
            // GameEngine.Execute(new LogPlayerState());
        }

        [ContextMenu("Test Player Data String")]
        private void TestPlayerData()
        {
            new PlayerProfile().CreateOrFetchPlayerData();
        }
    }
}
