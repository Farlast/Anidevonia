using UnityEngine;

namespace Script.Core
{
    
    public class GroundCheck : MonoBehaviour
    {
        [Header("Ground Check")]
        [SerializeField] protected Transform GroundCheckPos;
        [SerializeField] protected Vector2 CheckRadius;
        [SerializeField] protected LayerMask GroundLayer;
        [SerializeField] private bool drawGismos;
        [field:SerializeField] public bool IsGround { get; private set; }
        [field:SerializeField] public GroundType Type { get; private set; }
        
        private bool GroundCheckUpdate() 
        {
            Collider2D ground = Physics2D.OverlapBox(GroundCheckPos.position, new Vector2(CheckRadius.x, CheckRadius.y), 0f, GroundLayer);
            
            if (ground != null && ground.TryGetComponent(out Ground type))
            {
                Type = type.Type;
            }
            else
            {
                Type = GroundType.None;
            }

            return ground;
        }
        private void Update()
        {
            IsGround = GroundCheckUpdate();
        }
        private void OnDrawGizmos()
        {
            if (!drawGismos) return;
            if (GroundCheckPos != null)
            {
                // feet
                Gizmos.color = Color.cyan;
                Vector3 radius = new(CheckRadius.x, CheckRadius.y, 0);
                Gizmos.DrawWireCube(GroundCheckPos.position, radius);
            }
        }
    }
}
