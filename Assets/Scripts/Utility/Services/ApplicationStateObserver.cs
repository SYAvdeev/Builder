using System;
using UnityEngine;

namespace Builder.Utility.Services
{
    public sealed class ApplicationStateObserver : MonoBehaviour
    {
        private bool _needSave = true;
       
        public event Action<bool> ApplicationFocusAction;
        public event Action<bool> ApplicationPauseAction;
        public event Action ApplicationQuitAction;
        public event Action OnApplicationSave;
        
        private void OnApplicationFocus(bool hasFocus)
        {
            ApplicationFocusAction?.Invoke(hasFocus);
            
            if (!hasFocus && _needSave) Save();
            _needSave = hasFocus;
        }
        
        private void OnApplicationPause(bool isPaused)
        {
            ApplicationPauseAction?.Invoke(isPaused);
            
            if (isPaused && _needSave) Save();
            _needSave = !isPaused;
            
        }

        private void OnApplicationQuit()
        {
            ApplicationQuitAction?.Invoke();
            
            _needSave = false;
            Save();
        }
        
        private void Save()
        {
            OnApplicationSave?.Invoke();
        }
    }
}