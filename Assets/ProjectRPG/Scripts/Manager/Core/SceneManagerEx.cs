using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectRPG
{
    public class SceneManagerEx
    {
        public BaseScene CurrentScene { get => Object.FindObjectOfType<BaseScene>(); }

	    public void LoadScene(Define.Scene type)
        {
            Managers.Clear();

            SceneManager.LoadScene(GetSceneName(type));
        }

        private string GetSceneName(Define.Scene type) => System.Enum.GetName(typeof(Define.Scene), type);

        public void Clear()
        {
            CurrentScene.Clear();
        }
    }
}