using System.Collections;

public interface IAttackable
{
    string attackType { get; }
    float attackPower { get; }
    bool Attack(IDamagable target, float power, int numberOfAttacks);
}
public interface IDamagable
{
    string armorTypeName { get; }
    float armorValue { get; }

    IEnumerator ApplyDamage(float dmg);
}