using System.Collections;
using UnityEngine;
using Script.Core;
using Script.Core.Input;

namespace Script.Player
{
    public class WaterOrb : Summon
    {
        [SerializeField] private InputReader inputReader;
        [SerializeField] private float attackSpeed;
        [SerializeField] private bool stopAction;
        [SerializeField] private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        private void OnEnable()
        {
            inputReader.SkillActionEvent += OnPlayerSwitchElement;
        }
        private void OnDisable()
        {
            inputReader.SkillActionEvent -= OnPlayerSwitchElement;
        }
        
        public override void OnSummon(Transform transform)
        {
            this.transform.position = transform.position;
            stopAction = false;
            gameObject.SetActive(true);
        }

        public override void OnReturnSummon(Transform transform)
        {
            gameObject.SetActive(false);
        }

        private void OnPlayerSwitchElement()
        {
            print("morp");
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamageable target))
            {
                Attack(target);
            }
        }
        private void Attack(IDamageable target)
        {
            if (stopAction) return;
            animator.Play("Attack");
            DamageInfo info = new(1, transform.position);
            target.TakeDamage(info);
            stopAction = true;
            TimerSystem.Create(() => { stopAction = false; }, attackSpeed);
        }
    }
}
