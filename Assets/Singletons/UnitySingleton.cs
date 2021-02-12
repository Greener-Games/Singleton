using System.Collections.Generic;
using UnityEngine;

namespace GG.Singletons
{
    public class UnitySingleton<T> : MonoBehaviour where T : Component
    {
        static T _instance = null;
        public static T Instance
        {
            get
            {                
                ValidateInstance(true);
                return _instance;
            }
            //advised to avoid using this
            set => _instance = value;
        }
        
        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this);
                return;
            }
        }
        
        public static void ValidateInstance(bool createIfNeeded = false)
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                //attempt to find an inactive version of this object
                if(_instance == null)
                {
                    List<T> objs = Helpers.FindObjectsOfTypeAll<T>();
                    if(objs.Count > 0)
                    {
                        _instance = objs[0];
                    }
                }

                //create this object in the scene
                if (createIfNeeded && _instance == null)
                {
                    Debug.Log($"Having to Create single object for {typeof(T)}");
                    GameObject obj = new GameObject{name = typeof(T).Name  + "_RuntimeGenerated",hideFlags = HideFlags.DontSave};
                    _instance = obj.AddComponent<T>();
                }
            }
        }

        /// <summary>
        /// Check if the instance exists within the opened scenes
        /// </summary>
        /// <returns></returns>
        public static bool Exists
        {
            get
            {
                if (_instance == null)
                {
                    ValidateInstance();
                }

                return _instance != null;
            }
        }
        
        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }
}
