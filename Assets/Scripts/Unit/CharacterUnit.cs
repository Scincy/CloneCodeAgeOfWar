using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class CharacterUnit : MonoBehaviour
{
    public string CharacterName { get => _characterName; }
    public float MoveSpeed { get => _moveSpeed; }
    public float ATK { get => _atk; }
    public float EnemyDetectionRadious { get => _enemyDetectRadius; }
    public float NormalAttackCooldown { get => _normalAttackCooldown; }
    public float NormalAttackCastingTime { get => _noramlAttackCastingTime; }
    public float SpawnWaitingTime { get => _spawnWaitingTime; }
    public Team TeamInfo
    {
        get => _teamInfo;
        set
        {
            if (value == Team.Blue && defaultLookDirection == false || value == Team.Red && defaultLookDirection == true) 
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            _teamInfo = value;
        }
    }

    /// <summary>
    /// true : look right sie
    /// false : look left side
    /// </summary>
    [SerializeField]
    public bool defaultLookDirection;

    protected Transform AttackTargetPos
    {
        get
        {
            if (AttackTarget is null) throw new System.NullReferenceException("공격 대상의 좌표를 가져 오려 했으나 현재 이 유닛은 공격대상이 비어 있습니다.");
            return AttackTarget.transform;
        }
    }

    private string _characterName = "Unit";
    private float _moveSpeed = 1f;
    private float _atk = 1f;
    private float _enemyDetectRadius = 1f;
    private float _normalAttackCooldown = 1f;
    private float _noramlAttackCastingTime = 0.25f;
    private float _spawnWaitingTime = 1f;
    private Team _teamInfo = Team.None;
    
    


    protected Unit AttackTarget;

    public void SetTeam(Team settingValue)
    {
        if (settingValue == Team.Blue && defaultLookDirection == false || settingValue == Team.Red && defaultLookDirection == true)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        _teamInfo = settingValue;
    }
}
