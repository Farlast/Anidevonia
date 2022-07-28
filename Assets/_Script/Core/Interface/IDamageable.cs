using UnityEngine;

public interface IDamageable
{
    void TakeDamage(DamageInfo info);
}
public class DamageInfo
{
    public float Damage { get; set; }
    public Vector3 AttackerPosition { get; set; }
    public float KnockBack { get; set; }

    public DamageInfo(float damage, Vector3 attackerPosition)
    {
        Damage = damage;
        AttackerPosition = attackerPosition;
    }
    public float GetDiraction(Vector3 currentPosition)
    {
        return (currentPosition - AttackerPosition).normalized.x;
    }
}
public class TargetInfo
{

}
