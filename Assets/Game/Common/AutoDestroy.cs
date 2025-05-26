using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Common
{
    public class AutoDestroy : MonoBehaviour
    {
        [SerializeField] private float time = 1f;

        private float _timeStep;
        private bool _isDestroying;
        private Action<GameObject> _callback;

        private void Update()
        {
            if (_isDestroying)
            {
                Destroy(this.gameObject);
                return;
            }
            
            if (_timeStep < time)
            {
                _timeStep += Time.deltaTime;
            }
            else
            {
                _callback?.Invoke(gameObject);
                _isDestroying = true;
            }
        }

        public void SetOnDestroy(Action<GameObject> callback)
        {
            _callback = callback;
        }
    }
}
