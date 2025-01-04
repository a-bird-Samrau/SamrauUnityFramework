using System;

namespace SamrauFramework.Tools
{
    public class Timer : ITimer
    {
        public event Action<float> ValueChanged;
        public event Action Finished;

        private readonly TimerType _type;
        private readonly TimerManager _manager;
    
        public Timer(TimerManager manager, TimerType type)
        {
            _manager = manager;
            _type = type;
        }

        private void Subscribe()
        {
            switch (_type)
            {
                case TimerType.UpdateTick: _manager.UpdateTimeTicked += OnUpdateTimeTicked;
                    break;
                case TimerType.UpdateUnscaledTick: _manager.UpdateTimeUnscaledTicked += OnUpdateTimeTicked;
                    break;
                case TimerType.OneSecondTick: _manager.OneSecondTicked += OnOneSecondTicked;
                    break;
                case TimerType.OneSecondUnscaledTick: _manager.OneSecondUnscaledTicked += OnOneSecondTicked;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnOneSecondTicked()
        {
            if (IsPaused)
            {
                return;
            }

            RemainingSeconds -= 1f;
        
            CheckFinish();
        }
    
        private void OnUpdateTimeTicked(float deltaTime)
        {
            if (IsPaused)
            {
                return;
            }

            RemainingSeconds -= deltaTime;
        
            CheckFinish();
        }

        private void CheckFinish()
        {
            if (RemainingSeconds <= 0f)
            {
                Stop();
            }
            else
            {
                ValueChanged?.Invoke(RemainingSeconds);
            }
        }
    
        private void Unsubscribe()
        {
            switch (_type)
            {
                case TimerType.UpdateTick: _manager.UpdateTimeTicked -= OnUpdateTimeTicked;
                    break;
                case TimerType.UpdateUnscaledTick: _manager.UpdateTimeUnscaledTicked -= OnUpdateTimeTicked;
                    break;
                case TimerType.OneSecondTick: _manager.OneSecondTicked -= OnOneSecondTicked;
                    break;
                case TimerType.OneSecondUnscaledTick: _manager.OneSecondUnscaledTicked -= OnOneSecondTicked;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Start(float seconds)
        {
            if (seconds == 0)
            {
                Finished?.Invoke();
            }
        
            SetTime(seconds);

            IsPaused = false;
            Subscribe();
        }

        public void SetTime(float value)
        {
            RemainingSeconds = value;
            ValueChanged?.Invoke(value);
        }

        public void Pause()
        {
            IsPaused = true;
            Unsubscribe();
        
            ValueChanged?.Invoke(RemainingSeconds);
        }

        public void UnPause()
        {
            IsPaused = false;
            Subscribe();

            ValueChanged?.Invoke(RemainingSeconds);
        }

        public void Stop()
        {
            RemainingSeconds = 0f;
        
            Unsubscribe();

            ValueChanged?.Invoke(RemainingSeconds);
            Finished?.Invoke();
        }

        public float RemainingSeconds { get; private set; }
        public bool IsPaused { get; private set; }
    }
}