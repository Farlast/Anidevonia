using UnityEngine;
using Script.Core;

[RequireComponent(typeof(Collider2D))]
public class HitBox : MonoBehaviour
{
    [SerializeField] Stats stats;
    [SerializeField] TragetType tragets;
    enum TragetType
    {
        All,
        PlayerOnly
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out IDamageable _traget)) return;
        Attack(_traget, collision.gameObject.CompareTag("Player"));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out IDamageable _traget)) return;
        Attack(_traget, collision.gameObject.CompareTag("Player"));
    }
    private void Attack(IDamageable _traget,bool isPlayer)
    {
        DamageInfo info = new(stats.AttackDamage, transform.position);
        info.KnockBack = stats.AttackKnockBack;

        switch (tragets)
        {
            case TragetType.PlayerOnly:

                if (isPlayer)
                    _traget.TakeDamage(info);

                break;

            case TragetType.All:
                _traget.TakeDamage(info);
                break;
        }
    }
}
