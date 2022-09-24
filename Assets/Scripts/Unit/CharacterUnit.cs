using UnityEngine;
public enum LookDirection { Left, Right }
[RequireComponent(typeof(SpriteRenderer))]
public class CharacterUnit : Unit
{
    public string CharacterName { get => _characterName; }
    public float MoveSpeed { get => _moveSpeed; }
    public float ATK { get => _atk; }
    public float EnemyDetectionRadious { get => _enemyDetectRadius; }
    public float NormalAttackCooldown { get => _normalAttackCooldown; }
    public float NormalAttackCastingTime { get => _noramlAttackCastingTime; }
    public float SpawnWaitingTime { get => _spawnWaitingTime; }


    /// <summary>
    /// 기본 시선 방향
    /// </summary>
    [SerializeField]
    public LookDirection defaultLookDirection;

    protected Transform AttackTargetPos
    {
        get
        {
            if (AttackTarget is null) throw new System.NullReferenceException("공격 대상의 좌표를 가져 오려 했으나 현재 이 유닛은 공격대상이 비어 있습니다.");
            return AttackTarget.transform;
        }
    }
    protected Unit AttackTarget;

    private string _characterName = "Unit";
    private float _moveSpeed = 1f;
    private float _atk = 1f;
    private float _enemyDetectRadius = 1f;
    private float _normalAttackCooldown = 1f;
    private float _noramlAttackCastingTime = 0.25f;
    private float _spawnWaitingTime = 1f;


    public override void SetTeam(Team settingValue)
    {
        if (settingValue == Team.Blue && defaultLookDirection == LookDirection.Left
            || settingValue == Team.Red && defaultLookDirection == LookDirection.Right)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        base.SetTeam(settingValue);
    }
}
