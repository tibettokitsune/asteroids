using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Infrastructure
{
    public static class SceneLoader
    {
        public static AsyncOperation LoadSceneByName(string sceneName) 
            => SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        public static AsyncOperation UnloadSceneByName(string sceneName)
            => SceneManager.UnloadSceneAsync(sceneName);
    }
}