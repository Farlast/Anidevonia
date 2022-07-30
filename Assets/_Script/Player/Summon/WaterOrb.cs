using System.Collections;
using UnityEngine;
using Script.Core;
using Script.Core.Input;

namespace Script.Player
{
    public class WaterOrb : MonoBehaviour,ISummon
    {
        [SerializeField] private InputReader inputReader;
        [SerializeField] private float attackSpeed;
        [SerializeField] private bool stopAction;
        [SerializeField] private float lifeTime;

        private void OnEnable()
        {
            inputReader.AttackEvent += OnPlayerAttack;
        }
        private void OnDisable()
        {
            inputReader.AttackEvent -= OnPlayerAttack;
        }
       
        public void Summon()
        {
            stopAction = false;
            gameObject.SetActive(false);
            TimerSystem.Create(RemoveSummon,lifeTime);
        }

        public void RemoveSummon()
        {
            gameObject.SetActive(false);
        }

        private void OnPlayerAttack(bool attackPress)
        {
            if (attackPress)
            {
                print("morp");
            }
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
            
            print("Attack " + target.GetType().Name);
            DamageInfo info = new(1, transform.position);
            target.TakeDamage(info);
            stopAction = true;
            TimerSystem.Create(() => { stopAction = false; }, attackSpeed);
            
        }
    }
}
