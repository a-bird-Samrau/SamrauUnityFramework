using System;
using UnityEngine;
using Behaviour = Core.Behaviour;

namespace Tools
{
    public sealed class TimerManager : Behaviour
    {
        public event Action<float> UpdateTimeTicked;
        public event Action<float> UpdateTimeUnscaledTicked;

        public event Action OneSecondTicked;
        public event Action OneSecondUnscaledTicked;

        private float _oneSecondTimer;
        private float _oneSecondUnscaledTimer;

        private static TimerManager _instance;
    
        public static void Initialize()
        {
            if (_instance != null)
            {
                throw new Exception("Менеджер таймеров уже был проинициализирован");
            }

            _instance = Utilities.CreateGameObjectWithBehaviour<TimerManager>("Timer Manager");
        }

        protected override void Update()
        {
            base.Update();

            var deltaTime = Time.deltaTime;
            UpdateTimeTicked?.Invoke(deltaTime);

            var unscaledDeltaTime = Time.unscaledDeltaTime;
            UpdateTimeUnscaledTicked?.Invoke(unscaledDeltaTime);

            _oneSecondTimer += deltaTime;

            if (_oneSecondTimer >= 1f)
            {
                _oneSecondTimer -= 1f;
                OneSecondTicked?.Invoke();
            }

            _oneSecondUnscaledTimer += unscaledDeltaTime;

            if (_oneSecondUnscaledTimer >= 1f)
            {
                _oneSecondUnscaledTimer -= 1f;
                OneSecondUnscaledTicked?.Invoke();
            }
        }

        public static Timer Create(TimerType type)
        {
            return new Timer(_instance, type);
        }
    }
}