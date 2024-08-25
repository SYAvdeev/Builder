using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Builder.Utility.Services.Timer
{
    public sealed class TimerCommon
    {
        public event Action OnEnd;

        public bool IsActive => _totalSeconds > 0;

        private readonly List<Action<TimeSpan>> _callbacks = new();
        private Action _callback;

        private ApplicationStateObserver _applicationStateObserver;

        private float _totalSeconds;
        private DateTime _unfocusedTimeStart;

        public ApplicationStateObserver ApplicationStateObserver
        {
            set
            {
                _applicationStateObserver = value;
#if !UNITY_EDITOR
                _applicationStateObserver.ApplicationFocusAction += OnApplicationFocus;
#endif
            }
        }

        public float TotalSeconds
        {
            get => _totalSeconds;
            set => _totalSeconds = value;
        }

        public void Start(float durationSeconds, Action callback)
        {
            _totalSeconds = durationSeconds;
            _callback = callback;

            UniTask.WaitUntil(() =>
            {
                _totalSeconds -= Time.deltaTime;
                var result = _totalSeconds <= 0;
                if (result)
                {
                    InvokeCallbacks(TimeSpan.Zero);
                    _callbacks.Clear();
                    OnEnd?.Invoke();
                    _callback?.Invoke();
                }
                else
                {
                    var remainingTimeSpan = TimeSpan.FromSeconds(_totalSeconds);
                    InvokeCallbacks(remainingTimeSpan);
                }

                return result;
            }, PlayerLoopTiming.TimeUpdate).Forget();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
            {
                _unfocusedTimeStart = DateTime.UtcNow;
            }

            if (hasFocus && _unfocusedTimeStart != DateTime.MinValue)
            {
                var unfocusedDuration = DateTime.UtcNow - _unfocusedTimeStart;
                _totalSeconds -= (float)unfocusedDuration.TotalSeconds;
                _unfocusedTimeStart = DateTime.MinValue;
            }
        }

        public void Stop(bool invokeCallbacks = true, bool invokeEnd = true)
        {
            if (invokeCallbacks)
                InvokeCallbacks(TimeSpan.Zero);

            if (invokeEnd)
                OnEnd?.Invoke();

            ClearCallbacks();

            _totalSeconds = 0;

            _applicationStateObserver.ApplicationFocusAction -= OnApplicationFocus;
        }

        public void SubscribeCallback(Action<TimeSpan> callback)
        {
            _callbacks.Add(callback);
        }

        public void UnsubscribeCallback(Action<TimeSpan> callback)
        {
            _callbacks.Remove(callback);
        }

        private void InvokeCallbacks(TimeSpan remainingTimeSpan)
        {
            foreach (var callback in _callbacks)
            {
                callback.Invoke(remainingTimeSpan);
            }
        }

        public void ClearCallbacks()
        {
            OnEnd = null;
            _callback = null;
            _callbacks.Clear();
        }
        
        public void Clear()
        {
            OnEnd = null;
        }
    }
}