using UnityEngine;
using Script.Core;

namespace Script.Enemy
{
    public class StateController : MonoBehaviour
    {
        public Vector2 MoveVector;
        [field: SerializeField] internal State CurrentState { get; set; }
        [field: SerializeField] internal FieldOfView View { get; private set; }
        [field: SerializeField] internal Enemy Enemy { get; private set; }
        [field: SerializeField] internal EnemyAnimation Animator { get; private set; }
        [field: SerializeField] internal float LastDiraction { get; private set; }
        [field: SerializeField] internal Vector2 Velocity { get; private set; }
        [field: SerializeField] internal bool Takehit { get; set; }

        private Rigidbody2D rb;
        private void Awake()
        {
            CurrentState = GetComponent<State>();
            View = GetComponent<FieldOfView>();
            Enemy = GetComponent<Enemy>();
            Animator = GetComponent<EnemyAnimation>();

        }
        private void Start()
        {
            MoveVector.Set(0, 0);
            rb = GetComponent<Rigidbody2D>();
            LastDiraction = -1;
        }
        private void Update()
        {
            CurrentState.UpdateState();
            rb.velocity = MoveVector;
            Velocity = rb.velocity;
            if (MoveVector.x != 0) LastDiraction = MoveVector.x;
        }
    }
}
