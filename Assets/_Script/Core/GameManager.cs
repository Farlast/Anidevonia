using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] VoidEventChannel playerDeadEvent;

        void Start()
        {
            playerDeadEvent.onEventRaised += OnPlayerDead;
        }
        private void OnDestroy()
        {
            playerDeadEvent.onEventRaised -= OnPlayerDead;
        }

        private void OnPlayerDead()
        {
            TimerSystem.Create(() => { SceneManager.LoadSceneAsync("MainMenu"); }, 1.5f);
        }
    }
}
