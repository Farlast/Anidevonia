using System.Collections;
using UnityEngine;
using Script.Core;
using Script.Core.Input;

namespace Script.Player
{
    public class WaterOrb : MonoBehaviour
    {
        bool setupReady;
        [SerializeField] private InputReader inputReader;
        [SerializeField] private float attackSpeed;
        [SerializeField] private bool attackStop;
        
        private void Start()
        {
            attackStop = false;
            inputReader.AttackEvent += OnPlayerAttack;
        }
        private void OnDestroy()
        {
            inputReader.AttackEvent -= OnPlayerAttack;
        }
        public void SetUp(Vector2 diraction)
        {

        }
        

        private void OnPlayerAttack(bool attackPress)
        {
            if (setupReady && attackPress)
            {
                print("morp");
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            
            if (collision.TryGetComponent(out IDamageable target )){
                //Attack(target);
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
            if (!attackStop)
            {
                print("Attack " + target.GetType().Name);
                DamageInfo info = new DamageInfo(1, transform.position);
                target.TakeDamage(info);
                attackStop = true;
                TimerSystem.Create(() => { attackStop = false; }, attackSpeed);
            }
        }
    }
}
