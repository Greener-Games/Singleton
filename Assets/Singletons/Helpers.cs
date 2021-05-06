#region

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

namespace GG.Singletons
{
    static class Helpers
    {
        static readonly List<GameObject> DontDestroyOnLoads = new List<GameObject>();
        
        /// <summary>
        ///     Workaround to help find objects marked as dont destroy on load
        /// </summary>
        /// <param name="go"></param>
        internal static void RegisterDontDestroy(GameObject go)
        {
            DontDestroyOnLoads.Add(go);
        }
        
        /// <summary>
        /// remove a game object from the global cache
        /// </summary>
        /// <param name="go"></param>
        public static void DeRegisterDontDestroy(GameObject go)
        {
            if (DontDestroyOnLoads.Contains(go))
            {
                DontDestroyOnLoads.Remove(go);
            }
        }

        /// <summary>
        ///     Finds any object of type regardless if it is enabled or not
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal static List<T> FindObjectsOfTypeAll<T>()
        {
            List<T> results = new List<T>();
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene s = SceneManager.GetSceneAt(i);
                GameObject[] allGameObjects = s.GetRootGameObjects();
                for (int j = 0; j < allGameObjects.Length; j++)
                {
                    GameObject go = allGameObjects[j];
                    results.AddRange(go.GetComponentsInChildren<T>(true));
                }
            }

            //if we are quitting then these objects may be destroyed so dont check
            foreach (GameObject savedObject in DontDestroyOnLoads)
            {
                results.AddRange(savedObject.GetComponentsInChildren<T>(true));
            }

            return results;
        }
    }
}
