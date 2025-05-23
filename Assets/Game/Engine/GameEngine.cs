using System.Collections.Generic;
using Game.Configs;
using Game.Engine.Actions;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Engine
{
    public class GameEngine : MonoBehaviour
    {
        private readonly Queue<IPlayerAction> _actions = new Queue<IPlayerAction>();

        private PlayerContext _context;

        [SerializeField] private GameConfig config;

        private static GameEngine _instance;

        public static PlayerContext Context => _instance._context;
        
        private void Awake()
        {
            _instance = this;
            _context = new PlayerContext(config);
        }

        private void Update()
        {
            if (_actions.Count > 0)
            {
                IPlayerAction currentAction = _actions.Dequeue();
                ActionResult result = currentAction.Execute(_context);
                Debug.Log($"Executing {JsonConvert.SerializeObject(currentAction)} with result {result.ToString()}");
            }
        }

        public static void Execute(IPlayerAction action)
        {
            _instance._actions.Enqueue(action);
        }
    }
}