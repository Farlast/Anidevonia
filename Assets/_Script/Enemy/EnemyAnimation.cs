using UnityEngine;
using Script.Core;

namespace Script.Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimation : MonoBehaviour
    {
        private Animator animator;
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        private readonly int Idle = Animator.StringToHash("Idle");
        private readonly int Run = Animator.StringToHash("Run");
        private readonly int JumpUp = Animator.StringToHash("JumpUp");
        private readonly int PreAttack = Animator.StringToHash("PreAttack");
        private readonly int Attack = Animator.StringToHash("Attack");
        private readonly int KnockBack = Animator.StringToHash("KnockBack");

        public void IdleAnimation()
        {
            PlayAnimation(Idle, 0.1f);
        }

        public void RunAnimation()
        {
            PlayAnimation(Run, 0.1f);
        }

        public void AttackAnimation()
        {
            PlayAnimation(Attack, 0.1f);
        }
        public void PreAttackAnimation()
        {
            PlayAnimation(PreAttack, 0.1f);
        }

        private void PlayAnimation(int hash, float transitionTime)
        {
            animator.CrossFade(hash, transitionTime, 0);
        }
    }
}
