using UnityEngine;
using Script.Core;

namespace Script.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] Animator animator;
        [SerializeField] GameObject AttackEffect;

        internal readonly int Idle = Animator.StringToHash("Idle");
        internal readonly int Run = Animator.StringToHash("Run");
        internal readonly int JumpUp = Animator.StringToHash("JumpUp");
        internal readonly int JumpDown = Animator.StringToHash("JumpDown");
        internal readonly int Attack = Animator.StringToHash("Attack");
        internal readonly int AttackUp = Animator.StringToHash("AttackUp");
        internal readonly int AttackDown = Animator.StringToHash("AttackDown");
        internal readonly int Cast = Animator.StringToHash("Casting");
        internal readonly int Dash = Animator.StringToHash("Dash");
        internal readonly int KnockBack = Animator.StringToHash("KnockBack");
        public void AirAnimation(float velocityY)
        {
            int jump = velocityY < 0 ?  JumpDown:JumpUp;
            PlayAnimation(jump,0.35f);
        }
        public void RunAnimation()
        {
            PlayAnimation(Run,0.15f);
        }
        public void IdleAnimation()
        {
            PlayAnimation(Idle,0.35f);
        }
        public void AttackAnimation()
        {
            PlayAnimation(Attack,0.1f);
        }
        public void AttackUpAnimation()
        {
            PlayAnimation(AttackUp, 0.1f);
        }
        public void AttackDownAnimation()
        {
            PlayAnimation(AttackDown, 0.1f);
        }
        public void CastAnimation()
        {
            PlayAnimation(Cast, 0.25f);
        }
        public void SkillActionAnimation()
        {
            PlayAnimation(Cast, 0.25f);
        }
        public void DashAnimation()
        {
            PlayAnimation(Dash,0.25f);
        }

        //--------------------
        public void AttackEffectOn()
        {
            AttackEffect.SetActive(true);
        }
        public void AttackEffectOff()
        {
            AttackEffect.SetActive(false);
        }
        private void PlayAnimation(int hash,float transitionTime)
        {
            animator.CrossFade(hash,transitionTime, 0);
        }

    }
}
