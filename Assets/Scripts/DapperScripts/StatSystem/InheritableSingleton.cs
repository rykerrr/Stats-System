using System;
using UnityEngine;

namespace StatSystem.TakeOne
{
    public class InheritableGameObjectSingleton<T> : MonoBehaviour where T: MonoBehaviour
    {
        protected static T instance = default;

        protected static object lockObj = new object();
        protected static bool shuttingDown = false;

        public static T Instance
        {
            get
            {
                if (shuttingDown)
                {
                    #if UNITY_EDITOR
                        Debug.LogWarning($"Shutting down is true...{instance}");
                    #endif
                    
                    return null;
                }

                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = FindObjectOfType<T>();

                        if (instance == null)
                        {
                            instance = new GameObject(typeof(T).ToString()).AddComponent<T>();

                            #if UNITY_EDITOR
                            if (instance == null)
                            {
                                Debug.LogError("Instance is still null");
                            }
                            #endif
                        }

                        return instance;
                    }

                    return instance;
                }
            }
        }

        private void OnEnable()
        {
            shuttingDown = false;
        }

        private void OnDisable()
        {
            shuttingDown = true;
        }

        private void OnDestroy()
        {
            shuttingDown = true;
        }
    }
}