using UnityEngine;
using Script.Core;

namespace Script.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] Animator animator;
        [SerializeField] GameObject attackEffect;
        [SerializeField] GameObject attackEffect_2;
        [SerializeField] GameObject deadEffect;

        readonly int Idle = Animator.StringToHash("Idle");
        readonly int Run = Animator.StringToHash("Run");
        readonly int JumpUp = Animator.StringToHash("JumpUp");
        readonly int JumpDown = Animator.StringToHash("JumpDown");

        readonly int Attack = Animator.StringToHash("Attack");
        readonly int AttackCombo2 = Animator.StringToHash("AttackCombo2");
        readonly int AttackCombo3 = Animator.StringToHash("AttackCombo3");

        readonly int AttackUp = Animator.StringToHash("AttackUp");
        readonly int AttackDown = Animator.StringToHash("AttackDown");
        readonly int Cast = Animator.StringToHash("Casting");
        readonly int Dash = Animator.StringToHash("Dash");
        readonly int BackDash = Animator.StringToHash("BackDash");
        readonly int KnockBack = Animator.StringToHash("KnockBack");
        readonly int Dead = Animator.StringToHash("Dead");

        readonly int LightStop = Animator.StringToHash("LightStoping");
        readonly int LightLanding = Animator.StringToHash("LightLanding");

        #region Animation
        private int lastAnimation;
        public void LightLandingAnimation()
        {
            PlayAnimation(LightLanding, 0.25f);
        }
        public void DeadAnimation()
        {
            PlayAnimation(Dead, 0.35f);
            DeadEffect();
        }
        public void LightStopAnimation()
        {
            PlayAnimation(LightStop, 0.15f);
        }
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
                case 1:
                    PlayAnimation(Attack, 0.1f);
                    AttackEffectOn();
                    break;
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
        #endregion
        #region Effect
        public void AttackEffect2()
        {
            attackEffect_2.SetActive(true);
            TimerSystem.Create(() => { attackEffect_2?.SetActive(false); }, 0.5f);
        }
        public void AttackEffectOn()
        {
            attackEffect.SetActive(true);
            TimerSystem.Create(() => { attackEffect?.SetActive(false); }, 0.5f);
        }
        public void DeadEffect()
        {
            deadEffect.SetActive(true);
            TimerSystem.Create(() => { deadEffect?.SetActive(false); }, 1.5f);
        }
        #endregion

        #region Main
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
        #endregion
    }
}
