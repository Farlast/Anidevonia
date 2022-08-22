using Script.Core;
using UnityEngine;
using Script.Core.SaveSystem;
using UnityEngine.Events;

namespace Script.Player
{
    public class PlayerHealth : MonoBehaviour, ISaveDataPersistence,IDamageable
    {
        public event UnityAction<TakeDamageInfo> TakeDamageEvent;
        public VoidEventChannel DeadEvent;

        [SerializeField] float health;
        [SerializeField] float mana;
        [SerializeField] PlayerStats stats;
        [SerializeField] HealthEvent healthSystem;

        internal bool IsIframe { get; private set; }
        internal bool IsDead { get; private set; }

        private void Start()
        {
            SaveLoadSystem.OnLoad += Load;

            health = stats.MaxHP;
            mana = stats.MaxMp;
            IsDead = false;
        }
        private void OnDestroy()
        {
            SaveLoadSystem.OnLoad -= Load;
        }
        
        public void Load(SaveData saveData)
        {
            if(saveData.Player != null)
            {
                health = saveData.Player.HP;
                stats.MaxHP = saveData.Player.MaxHP;
                transform.position = saveData.Player.SavePosition;
            }
        }

        public void Save(SaveData saveData)
        {
            saveData.Player = new()
            {
                HP = health,
                MaxHP = stats.MaxHP
            };
        }

        public void TakeDamage(DamageInfo damage)
        {
            if (IsIframe) return;

            health = healthSystem.Damage(damage, health, stats.MaxHP);

            if (health <= 0)
            {
                IsDead = true;
                DeadEvent?.RiseEvent();
            }

            TakeDamageInfo takeDamageInfo = new()
            {
                DamageInfo = damage,
                IsDead = IsDead
            };

            TakeDamageEvent?.Invoke(takeDamageInfo);
            TakeDamageEffect();
            Iframe();
        }
        private void Iframe()
        {
            IsIframe = true;
            TimerSystem.Create(() => { IsIframe = false; },stats.IframeTime,"Iframe");
        }
        private void TakeDamageEffect()
        {
            var effect = EffectPool.GetEffect(transform);
            TimerSystem.Create(() => { EffectPool.ReturnEffect(effect); }, 0.3f, "TakeDamegeEffect");
        }
    }
}
