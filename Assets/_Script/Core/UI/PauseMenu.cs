using UnityEngine;
using UnityEngine.SceneManagement;
using Script.Core.Input;

namespace Script.Core.UI
{
    public class PauseMenu : MonoBehaviour
    {
        public InputReader OpenMenuEvent;
        public GameObject Panel;
        private bool toggle;
        private void Start()
        {
            toggle = false;
            Panel.SetActive(false);

            OpenMenuEvent.PauseEvent += OnPauseTirgger;
        }
        private void OnDestroy()
        {
            OpenMenuEvent.PauseEvent -= OnPauseTirgger;
        }
        public void OnBackToMenu()
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }
        public void OnContinune()
        {
            Panel.SetActive(false);
        }
        public void OnPauseTirgger()
        {
            toggle = !toggle;
            Panel.SetActive(toggle);
        }
    }
}
