using System;
using System.Collections;
using UnityEngine;

namespace SamrauFramework.Tools
{
    public abstract class CoroutineOperation
    {
        public event Action Completed;

        private Coroutine _coroutine;
        private readonly TimeType _timeType;

        protected CoroutineOperation(float duration, TimeType timeType)
        {
            _timeType = timeType;
            Duration = duration;
        }

        private void SendOnValueChanged(float value)
        {
            OnValueChanged(value);
        }

        private void SendOnCompleted()
        {
            OnCompleted();
            Completed?.Invoke();
        }

        protected virtual void OnRunning()
        {
            
        }

        protected virtual void OnValueChanged(float value)
        {
            
        }

        protected virtual void OnCompleted()
        {
            
        }

        public bool Run(bool force)
        {
            if (IsRunning)
            {
                if (!force)
                {
                    return false;
                }
                
                Stop();
            }
            
            OnRunning();

            _coroutine = Coroutines.Run(PlayRoutine(Duration, _timeType, SendOnValueChanged, SendOnCompleted));
            IsRunning = true;

            return true;
        }

        public void Stop()
        {
            if (_coroutine != null)
            {
                Coroutines.Stop(_coroutine);
            }
            
            IsRunning = false;
        }

        private static float GetDeltaTime(TimeType type)
        {
            return type switch
            {
                TimeType.Default => Time.deltaTime,
                TimeType.Unscaled => Time.unscaledDeltaTime,
                TimeType.Fixed => Time.fixedDeltaTime,
                TimeType.UnscaledFixed => Time.fixedUnscaledDeltaTime,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        private static IEnumerator PlayRoutine(float duration, TimeType timeType, Action<float> valueChanged, System.Action completed)
        {
            var elapsedTime = 0f;
            var progress = 0f;
        
            while (progress < 1f)
            {
                valueChanged?.Invoke(progress);
        
                elapsedTime += GetDeltaTime(timeType);
                progress = elapsedTime / duration;
                
                yield return null;
            }
            
            valueChanged?.Invoke(1f);
            completed?.Invoke();
        }

        public bool IsRunning { get; private set; }
        public float Duration { get; }
    }
}