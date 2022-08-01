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
            SceneManager.LoadSceneAsync("ForestBiome", LoadSceneMode.Additive);
            SceneManager.LoadSceneAsync("Map1-1", LoadSceneMode.Additive).completed += SetupPlayer;
        }
        void SetupPlayer(AsyncOperation operation)
        {
            SceneManager.LoadSceneAsync("Player", LoadSceneMode.Additive);
            SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
        }
    }
}
