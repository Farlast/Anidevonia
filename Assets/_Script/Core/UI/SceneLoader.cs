using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Core.UI
{
    public class SceneLoader : MonoBehaviour
    {
        void Start()
        {
            SceneManager.LoadSceneAsync("Map1-1", LoadSceneMode.Additive).completed += (async) =>
            {
                SceneManager.LoadSceneAsync("Player",LoadSceneMode.Additive);
                SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
            };
        }
    }
}
