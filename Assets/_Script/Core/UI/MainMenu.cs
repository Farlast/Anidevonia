using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Core.UI
{
    public class MainMenu : MonoBehaviour
    {
        void Start()
        {
        
        }

        public void OnExit()
        {
            Application.Quit();
        }

        public void OnNewGame()
        {
            LoadScene();
        }
        public void OnContinue()
        {

        }
        public void OnSettings()
        {

        }

        private void LoadScene()
        {
            Debug.Log("Loading");
            SceneManager.LoadSceneAsync("System");
        }
    }
}
