public enum AttackStyle { Projectile, ShortRange }
public enum AttackPowerType { Normal, Magical }

public struct Attack
{
    /// <summary>
    /// 공격력
    /// </summary>
    public float AttackPower { get => _atkPower; }
    /// <summary>
    /// 공격 방식
    /// </summary>
    public AttackStyle attackStyle { get => _atkStyle; }
    /// <summary>
    /// 공격 종류
    /// </summary>
    public AttackPowerType attackPowerType { get => _atkPowerType; }

    private float _atkPower;
    private AttackStyle _atkStyle;
    private AttackPowerType _atkPowerType;

    #region Initializer
    public Attack(float attackPower)
    {
        _atkPower = attackPower;
        _atkStyle = AttackStyle.ShortRange;
        _atkPowerType = AttackPowerType.Normal;
    }
    public Attack(float attackPower, AttackStyle attackStyle)
    {
        _atkPower = attackPower;
        _atkStyle = attackStyle;
        _atkPowerType = AttackPowerType.Normal;
    }
    public Attack(float attackPower, AttackStyle attackStyle, AttackPowerType powerType)
    {
        _atkPower = attackPower;
        _atkStyle = attackStyle;
        _atkPowerType = powerType;
    }
    #endregion
    public float GetDemageAmount()
    {
        return AttackPower;
    }
}