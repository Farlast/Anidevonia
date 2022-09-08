using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Enemy
{
    public class CliffCheck : MonoBehaviour
    {
        [SerializeField] private bool drawGismos;
        [SerializeField] protected Transform CheckPosition;
        [SerializeField] private float RayLength;
        [SerializeField] private LayerMask GroundLayer;
        [field:SerializeField] public bool IsWallAtFront { get; private set; }

        private bool WallCheck() => IsWallAtFront = Physics2D.Raycast(CheckPosition.position, new Vector2(0, -1), RayLength, GroundLayer);
        private void Update()
        {
            WallCheck();
        }
        private void OnDrawGizmos()
        {
            if (!drawGismos || CheckPosition == null) return;

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(CheckPosition.position, CheckPosition.position - new Vector3(0,RayLength, 0));
        }
    }
}
