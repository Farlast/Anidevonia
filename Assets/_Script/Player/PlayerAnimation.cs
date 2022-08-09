using UnityEngine;
using Script.Core;

namespace Script.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] Animator animator;
        [SerializeField] GameObject AttackEffect;
        [SerializeField] GameObject AttackEffect_2;

        internal readonly int Idle = Animator.StringToHash("Idle");
        internal readonly int Run = Animator.StringToHash("Run");
        internal readonly int JumpUp = Animator.StringToHash("JumpUp");
        internal readonly int JumpDown = Animator.StringToHash("JumpDown");

        internal readonly int Attack = Animator.StringToHash("Attack");
        internal readonly int AttackCombo2 = Animator.StringToHash("AttackCombo2");
        internal readonly int AttackCombo3 = Animator.StringToHash("AttackCombo3");

        internal readonly int AttackUp = Animator.StringToHash("AttackUp");
        internal readonly int AttackDown = Animator.StringToHash("AttackDown");
        internal readonly int Cast = Animator.StringToHash("Casting");
        internal readonly int Dash = Animator.StringToHash("Dash");
        internal readonly int BackDash = Animator.StringToHash("BackDash");
        internal readonly int KnockBack = Animator.StringToHash("KnockBack");
        
        private int lastAnimation;
        public void KnockBackAnimation()
        {
            PlayAnimation(KnockBack, 0.15f);
        }
        public void AirAnimation(float velocityY)
        { 
            int jump = velocityY < 0 ?  JumpDown:JumpUp;
            PlayAnimation(jump,0.2f);
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
            AttackEffectOn();
        }
        public void AttackComboAnimation(int combo)
        {
            switch (combo)
            {
                case 2:
                    PlayAnimation(AttackCombo2, 0.1f);
                    AttackEffect2();
                    break;
                case 3:
                    PlayAnimation(AttackCombo3, 0.1f);
                    AttackEffectOn();
                    break;
            }
            
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
            PlayAnimation(Dash,0.2f);
        }
        public void BackDashAnimation()
        {
            PlayAnimation(BackDash, 0.2f);
        }
        //--------------------
        public void AttackEffect2()
        {
            AttackEffect_2.SetActive(true);
            TimerSystem.Create(() => { AttackEffect_2.SetActive(false); }, 0.5f);
        }

        //--------------------
        public void AttackEffectOn()
        {
            AttackEffect.SetActive(true);
            TimerSystem.Create(() => { AttackEffectOff(); }, 0.5f);
        }
        public void AttackEffectOff()
        {
            AttackEffect.SetActive(false);
        }
        //---------------------
        private void PlayAnimation(int hash,float transitionTime)
        {
            if(IsNotSameAnimation(hash))
            animator.CrossFade(hash,transitionTime, 0);
            lastAnimation = hash;
        }
        private bool IsNotSameAnimation(int hash)
        {
            return lastAnimation != hash;
        }

    }
}
