using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Core.UI
{
    public class SceneLoader : MonoBehaviour
    {
        void Start()
        {
            LoadPlayableScene();
        }
        void LoadPlayableScene()
        {
            SceneManager.LoadSceneAsync("Map1-1", LoadSceneMode.Additive).completed += Setup;
        }
        void Setup(AsyncOperation operation)
        {
            SceneManager.LoadSceneAsync("Player", LoadSceneMode.Additive);
            SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
        }
    }
}
