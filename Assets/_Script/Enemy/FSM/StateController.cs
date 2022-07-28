using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Core;
using System;

namespace Script.Enemy
{
    public class StateController : MonoBehaviour
    {
        public string Id;
        public Vector2 MoveVector;
        [field: SerializeField] public State CurrentState { get; set; }
        [field: SerializeField] public FieldOfView View { get; private set; }
        [field: SerializeField] public Enemy Enemy { get; private set; }
        [field: SerializeField] public EnemyAnimation Animator { get; private set; }
        [field: SerializeField] public float LastDiraction { get; private set; }

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
            Id = Guid.NewGuid().ToString();
            MoveVector.Set(0, 0);
            rb = GetComponent<Rigidbody2D>();
            LastDiraction = -1;
        }
        private void Update()
        {
            CurrentState.UpdateState();
            rb.velocity = MoveVector;
            if (MoveVector.x != 0) LastDiraction = MoveVector.x;
        }
    }
}
