using UnityEngine;

namespace Script.Core
{
    public class Detector : MonoBehaviour
    {
        enum RootDirection
        {
            Up,
            Right
        }
        [Header("View Settings")]
        [SerializeField] private float Radius;
        [SerializeField] private RootDirection rootDirection;
        [Range(0,360)]
        [SerializeField] private float Angle;
        [SerializeField] public LayerMask TargrtLayer;
        [SerializeField] public LayerMask Ground;
        [Header("Output")]
        public bool DrawGizmos;
        [field: SerializeField] public bool SeeTarget { get; private set; }
        [field: SerializeField] public Vector3 TargetPosition { get; private set;}
        [field: SerializeField] public Vector3 DirectionToTarget { get; private set; }
        [field: SerializeField] public float DistanceToTarget { get; private set; }

        private void FixedUpdate()
        {
            FieldOfViewCheck();
        }
        private void FieldOfViewCheck()
        {
            var target = Physics2D.OverlapCircle(transform.position, Radius, TargrtLayer);
            
            if (target == null) 
            { 
                SeeTarget = false;
                return; 
            }

            if (!target.TryGetComponent(out IDamageable damageable))
            {
                SeeTarget = false;
                return;
            }

            TargetPosition = target.transform.position;
            DirectionToTarget = Helpers.GetDirection(transform.position, TargetPosition);
            DistanceToTarget = Vector3.Distance(transform.position, TargetPosition);

            SeeTarget = IsInAngle(DirectionToTarget) && IsNotBlockByObject(DirectionToTarget);
            
        }
        private bool IsNotBlockByObject(Vector3 Direction)
        {
            // check is some object block view.
            return !Physics2D.Raycast(transform.position, Direction, DistanceToTarget, Ground);
        }
        private bool IsInAngle(Vector3 Direction)
        {
            return Vector2.Angle(GetRootDiraction(), Direction) < Angle / 2 ;
        }
        private Vector2 GetRootDiraction()
        {
            switch (rootDirection)
            {
                case RootDirection.Up: return transform.up;
                case RootDirection.Right: return transform.right;
                default: return transform.up;
            }
        }
        private void OnDrawGizmos()
        {
            if (!DrawGizmos) return;
            
            Gizmos.color = Color.green;
            var Direction = Helpers.GetDirection(transform.position, TargetPosition);
            Gizmos.DrawLine(transform.position, transform.position + Direction * DistanceToTarget);


            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, Radius);
            
            Vector3 ang1;
            Vector3 ang2;
            
            if (rootDirection == RootDirection.Up)
            {
                ang1 = Helpers.DirectionFromAngle2D(0, -Angle / 2);
                ang2 = Helpers.DirectionFromAngle2D(0, Angle / 2);
            }
            else
            {
                ang1 = Helpers.DirectionFromAngle2D(90, -Angle / 2);
                ang2 = Helpers.DirectionFromAngle2D(90, Angle / 2);
            }

            Gizmos.DrawLine(transform.position, transform.position + ang1 * Radius);
            Gizmos.DrawLine(transform.position, transform.position + ang2 * Radius);
        }   
    }
}
