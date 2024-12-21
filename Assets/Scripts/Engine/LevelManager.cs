using System;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Engine
{
    public class LevelManager : ILevelManager
    {
        public event Action Loading;
        public event Action<ILevel> Loaded;

        private IEnumerator LoadAsync(ILevel level)
        {
            Loading?.Invoke();
            
            if (ActiveLevel != null)
            {
                yield return SceneManager.UnloadSceneAsync(ActiveLevel.Name);
            }

            var loadOperation = SceneManager.LoadSceneAsync(level.Name, LoadSceneMode.Additive);
            loadOperation.allowSceneActivation = false;

            while (!loadOperation.isDone)
            {
                if (loadOperation.progress >= 0.9f)
                {
                    loadOperation.allowSceneActivation = true;
                }

                yield return null;
            }

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(level.Name));
            ActiveLevel = level;

            Loaded?.Invoke(level);
        }
        
        public void Load(ILevel level)
        {
            Coroutines.Run(LoadAsync(level));
        }

        public ILevel ActiveLevel { get; private set; }
    }
}