using System;
using System.Collections;
using System.Collections.Generic;
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
    public float AttackableDistance { get => _attackableDistance; }
    protected Rigidbody2D RigidbodyComponent { get => _rigidbody; }
    
    /// <summary>
    /// 기본 시선 방향
    /// </summary>
    public LookDirection defaultLookDirection;
    public bool Targetable = true;

    [SerializeField]protected Unit AttackTarget;

    private string _characterName = "Unit";
    private float _moveSpeed = 0.01f;
    private float _atk = 1f;
    private float _enemyDetectRadius = 1f;
    private float _normalAttackCooldown = 1f;
    private float _noramlAttackCastingTime = 0.25f;
    private float _spawnWaitingTime = 1f;
    private float _attackableDistance = 0.3f;
    private Rigidbody2D _rigidbody;

    private Coroutine _findTagetRoutine;
    private Coroutine _moveRoutine;

    private const float DieAnimDuration = 2f;

    public override void Initialize()
    {
        base.Initialize();
        _rigidbody = GetComponent<Rigidbody2D>();
        if (_rigidbody is null)
            throw new NullReferenceException("RigidBody 찾기 실패! 설정이 되었는지 확인해 주세요.");
        AttackTarget = GameManager.GetEnemyCastle(TeamInfo);
    }

    private void Start()
    {
        _findTagetRoutine = StartCoroutine(FindTarget());
        _moveRoutine = StartCoroutine(Move());
    }

    public override void SetTeam(Team settingValue)
    {
        if (settingValue == Team.Blue && defaultLookDirection == LookDirection.Left
            || settingValue == Team.Red && defaultLookDirection == LookDirection.Right)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        base.SetTeam(settingValue);
    }

    protected virtual IEnumerator Move()
    {
        while (true)
        {
            float distance = Vector2.Distance(transform.position, AttackTarget.transform.position);
            //공격 가능 범위밖에 대상이 있다면 이동합니다.
            if (distance > _attackableDistance)
                RigidbodyComponent.MovePosition(Vector2.MoveTowards(transform.position, AttackTarget.transform.position, MoveSpeed));
            // TODO : else StartAttack()//공격 가능 거리 안에 적이 있음으로 공격 단계를 수행합니다.

            yield return new WaitForFixedUpdate();
        }
    }

    protected virtual IEnumerator FindTarget()
    {
        List<CharacterUnit> enemyList = new List<CharacterUnit>();
        while (true)
        {
            enemyList = SpawnManager.Instance.GetEnemyUnitList(TeamInfo);
            if (enemyList.Count >= 1)//소환된 적이 있는지 체크
            {
                Unit foundtarget = GetNearestEnemyCharacter();
                // 찾은 타겟이 현재 타겟팅중인 대상과 다르다면
                if (!ReferenceEquals(AttackTarget, foundtarget))
                {
                    // 현재 타켓팅중인 적의 사망에 대한 구독을 해제합니다.
                    AttackTarget.DieEvent.RemoveListener(OnEnemyDead);
                }
                AttackTarget = foundtarget; //적 타겟팅
                AttackTarget.DieEvent.AddListener(OnEnemyDead); //타겟팅 된 적의 사망에 대한 구독
            }
            else AttackTarget = GameManager.GetEnemyCastle(this.TeamInfo);
            yield return null;// update() 함수 대용
        }

        CharacterUnit GetNearestEnemyCharacter()
        {
            float min = float.MaxValue;
            int nearestItemOrder = 0;
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (!enemyList[i].Targetable) continue;
                float distance = Vector2.Distance(transform.position, enemyList[i].transform.position);
                if (distance < min)
                {
                    min = distance;
                    nearestItemOrder = i;
                }
            }
            return enemyList[nearestItemOrder];
        }
    }
    /// <summary>
    /// Callback 함수. 공격대상이던 적 캐릭터가 죽었을때 이 캐릭터가 보여 줄 반응을 이곳에 작성합니다.
    /// </summary>
    protected virtual void OnEnemyDead()
    {
        //타겟팅 중이던 적이 죽으면 새로운 대상을 찾도록 합니다.
        FindTarget();
    }
    /// <summary>
    /// 이 캐릭터가 죽임을 당했을 때 보여줄 이 캐릭터의 반응을 이곳에 작성합니다.
    /// </summary>
    protected override void OnDie()
    {
        base.OnDie();
        Targetable = false;
        StopAllCoroutines();
        SpawnManager.Instance.GetUnitList(this.TeamInfo).Remove(this);
        DieEvent.RemoveAllListeners();
        // TODO : 사망연출
        Destroy(gameObject, DieAnimDuration);
    }
}
