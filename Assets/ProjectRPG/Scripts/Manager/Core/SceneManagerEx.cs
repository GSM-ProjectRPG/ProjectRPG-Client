using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace ProjectRPG
{
    public class SceneManagerEx
    {
        public BaseScene CurrentScene { get => Object.FindObjectOfType<BaseScene>(); }

	    public void LoadScene(Define.Scene type, System.Action callback = null)
        {
            Managers.Clear();
            Managers.Instance.StartCoroutine(LoadSceneAsync(type, callback));
            SceneManager.LoadScene(GetSceneName(type));
        }

        private string GetSceneName(Define.Scene type) => System.Enum.GetName(typeof(Define.Scene), type);

        public void Clear()
        {
            CurrentScene.Clear();
        }

        private IEnumerator LoadSceneAsync(Define.Scene type, System.Action callback)
        {
            var asyncLoad = SceneManager.LoadSceneAsync(GetSceneName(type));

            while (!asyncLoad.isDone)
                yield return null;

            callback?.Invoke();
        }
    }
}