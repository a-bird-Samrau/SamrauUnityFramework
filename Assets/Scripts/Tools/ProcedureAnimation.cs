using System;
using System.Collections;
using UnityEngine;

namespace Tools
{
    public abstract class ProcedureAnimation
    {
        public event Action<float> ValueChanged;
        public event Action Completed;

        private Coroutine _routine;

        private readonly TimeType _timeType;

        protected ProcedureAnimation(float duration, TimeType timeType)
        {
            _timeType = timeType;
        
            Duration = duration;
        }

        private void SendOnValueChanged(float value)
        {
            OnValueChanged(value);
            ValueChanged?.Invoke(value);
        }

        private void SendOnCompleted()
        {
            OnCompleted();
            Completed?.Invoke();
        }

        protected abstract void OnBeginning();
        protected abstract void OnValueChanged(float value);
        protected abstract void OnCompleted();

        public bool Play(bool force)
        {
            if (IsPlaying)
            {
                if (!force)
                {
                    return false;
                }
                
                Stop();
            }
            
            OnBeginning();

            _routine = Coroutines.Run(PlayRoutine(Duration, _timeType, SendOnValueChanged, SendOnCompleted));
            
            IsPlaying = true;

            return true;
        }

        public void Stop()
        {
            if (_routine != null)
            {
                Coroutines.Stop(_routine);
            }
            
            IsPlaying = false;
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
            float progress = 0;
        
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

        public bool IsPlaying { get; private set; }
        public float Duration { get; }
    }
}