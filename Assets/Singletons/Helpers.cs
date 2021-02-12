using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GG.Singletons
{
    internal class Helpers
    {
        internal static List<GameObject> dontDestroyOnLoads = new List<GameObject>();

        /// <summary>
        /// Workaround to help find objects marked as dont destroy on load
        /// </summary>
        /// <param name="go"></param>
        internal static void RegisterDontDestroy(GameObject go)
        {
            dontDestroyOnLoads.Add(go);
        }

/// <summary>
/// Finds any object of type regardless if it is enabled or not
/// </summary>
/// <typeparam name="T"></typeparam>
/// <returns></returns>
        internal static List<T> FindObjectsOfTypeAll<T>()
        {

            List<T> results = new List<T>();
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var s = SceneManager.GetSceneAt(i);
                if (s.isLoaded)
                {
                    var allGameObjects = s.GetRootGameObjects();
                    for (int j = 0; j < allGameObjects.Length; j++)
                    {
                        var go = allGameObjects[j];
                        results.AddRange(go.GetComponentsInChildren<T>(true));
                    }
                }
            }

            foreach (GameObject savedObject in dontDestroyOnLoads)
            {
                results.AddRange(savedObject.GetComponentsInChildren<T>(true));
            }

            return results;
        }
    }
}
