using Script.Core;
using UnityEngine;
using Script.Core.SaveSystem;

namespace Script.Player
{
    public class PlayerHealth : MonoBehaviour, ISaveDataPersistence,IDamageable
    {
        [SerializeField] float health;
        [SerializeField] float mana;
        [SerializeField] PlayerStats stats;
        [SerializeField] HealthEvent healthSystem;
        private DamageInfo backupInfo;

        internal bool IsIframe { get; private set; }
        internal bool IsTakeDamage { get; private set; }
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
            saveData.Player = new();
            saveData.Player.HP = health;
            saveData.Player.MaxHP = stats.MaxHP;
        }

        public void TakeDamage(DamageInfo damage)
        {
            if (IsIframe) return;
            
            health = healthSystem.Damage(damage, health,stats.MaxHP);
            backupInfo = damage;
            if (health <= 0) IsDead = true;
            
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
            IsTakeDamage = true;
            TimerSystem.Create(() => { IsTakeDamage = false; },0.1f,"TakeDamege");
            var effect = EffectPool.GetEffect(transform);
            TimerSystem.Create(() => { EffectPool.ReturnEffect(effect); }, 0.3f, "TakeDamegeEffect");
        }
        public DamageInfo GetDamageInfo()
        {
            return backupInfo;
        }
    }
}
