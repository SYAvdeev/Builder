using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Builder.Utility.Animations
{
    public class SwarmAnimation
    {
        private readonly GameObject _objectPrefab;
        private Transform _objectsParent;
        
        private readonly Stack<GameObject> _objectsPool = new ();

        public SwarmAnimation(GameObject objectPrefab, Transform objectsParent)
        {
            _objectPrefab = objectPrefab;
            _objectsParent = objectsParent;
        }
        
        public SwarmAnimation(GameObject objectPrefab)
        {
            _objectPrefab = objectPrefab;
        }

        public Sequence GetSwarmAnimation(
            int objectsCount, 
            float minDelay, 
            float maxDelay,
            float flyDuration,
            float jumpPowerMin,
            float jumpPowerMax,
            Vector3 startPosition,
            Vector3 finalPosition,
            Action<GameObject> objectInitializeCallback,
            Action singleFlyStartCallback,
            Action singleFlyEndCallback)
        {
            Sequence sequence = DOTween.Sequence();
            float time = 0f;
            
            for (int i = 0; i < objectsCount; i++)
            {
                GameObject gameObject = GetObject();
                objectInitializeCallback?.Invoke(gameObject);
                gameObject.SetActive(false);
                gameObject.transform.position = startPosition;
                
                sequence.InsertCallback(time, () =>
                {
                    gameObject.SetActive(true);
                    singleFlyStartCallback?.Invoke();
                });
                sequence.Insert(time, gameObject.transform.DOJump(
                    finalPosition, 
                    Random.Range(jumpPowerMin, jumpPowerMax), 
                    1,
                    flyDuration));
                sequence.InsertCallback(time + flyDuration, () =>
                {
                    gameObject.SetActive(false);
                    _objectsPool.Push(gameObject);
                    singleFlyEndCallback?.Invoke();
                });
                
                time += Random.Range(minDelay, maxDelay);
            }
            return sequence;
        }
        
        private GameObject GetObject()
        {
            if(!_objectsPool.TryPop(out GameObject result))
            {
                result = Object.Instantiate(_objectPrefab, _objectsParent);
            }
            
            return result;
        }

        public void SetNewObjectParent(Transform objectsParent)
        {
            _objectsParent = objectsParent;
            _objectsPool.Clear();
        }
    }
}