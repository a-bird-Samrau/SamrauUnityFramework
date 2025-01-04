﻿using System;

namespace SamrauFramework.Tools
{
    public interface ITimer
    {
        event Action<float> ValueChanged;
        event Action Finished;
    
        float RemainingSeconds { get; }
        bool IsPaused { get; }
    }
}