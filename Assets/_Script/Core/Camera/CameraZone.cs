using UnityEngine;
using Cinemachine;

namespace Script.Core.Camera
{
    public class CameraZone : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        private void Start()
        {
            virtualCamera.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                virtualCamera.enabled = true;

                virtualCamera.Follow = collision.gameObject.transform;
                virtualCamera.Priority = 10;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            virtualCamera.Priority = 1;
        }
    }
}
