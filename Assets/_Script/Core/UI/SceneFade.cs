using UnityEngine;

namespace Script.Core.UI
{
    public class SceneFade : MonoBehaviour
    {
        [SerializeField] GameObject fadeCanvas;
        Animator animator;
        private void Awake()
        {
          animator = GetComponent<Animator>();
        }
        void Start()
        {
            fadeCanvas.SetActive(false);
        }

        public void FadeToGame()
        {
            animator.Play("FadeIn");
        }
        public void FadeToBlack()
        {
            animator.Play("FadeOut");
        }
    }
}
