using System.Collections.Generic;
using UnityEngine;
public class SpawnManager : MonoBehaviour
{
    private const string UserSpawnerTag = "userSpawner";
    private const string AISpawnerTag = "aiSpawner";
    public static SpawnManager Instance;

    private List<CharacterUnit> PlayerUnits;
    private List<CharacterUnit> AIUnits;

    private ICharacterSpawner userSpawner;
    private ICharacterSpawner aiSpawner;


    private void Awake()
    {
        if (Instance is null) Instance = this;
        else
        {
            Debug.LogError("SpawnManager 가 두개 이상으로 싱글톤 패턴을 구현 할 수 없습니다.");
        }
        userSpawner = GameObject.FindGameObjectWithTag(UserSpawnerTag)?.GetComponent<ICharacterSpawner>();
        aiSpawner = GameObject.FindGameObjectWithTag(AISpawnerTag)?.GetComponent<ICharacterSpawner>();
        if (userSpawner is null || aiSpawner is null) throw new System.NullReferenceException("스포너 찾기 실패!");

        PlayerUnits = new List<CharacterUnit>();
        AIUnits = new List<CharacterUnit>();
    }
    /// <summary>
    /// 스포너에 스폰 요청을 전달합니다.
    /// </summary>
    /// <param name="request">ㅅ환할 캐릭터에 대한 요청 정보입니다.</param>
    public void SendSpawnRequest(SpawnRequest request)
    {
        if (request.Equals(default(SpawnRequest))) Debug.LogWarning("spawn request가 초기화 되지 않은 것 같아요.");
        // 소환 된 캐릭터를 관리하기 위해 소환요청에 해당하는 팀의 리스트에 추가합니다.
        GetUnitList(request.TeamInfo).Add(userSpawner.Spawn(request));
    }

    public List<CharacterUnit> GetUnitList(Team myTeamData)
    {
        switch (myTeamData)
        {
            case Team.Red:
                return AIUnits;
            case Team.Blue:
                return PlayerUnits;
            default:
                throw new System.NotSupportedException($"팀 정보 {myTeamData}에 대한 유닛리스트는 지원되지 않습니다.");
        }
    }
    public List<CharacterUnit> GetEnemyUnitList(Team myTeamData)
    {
        switch (myTeamData)
        {
            case Team.Red:
                return PlayerUnits;
            case Team.Blue:
                return AIUnits;
            default:
                throw new System.NotSupportedException($"팀 정보 {myTeamData}에 대한 유닛리스트는 지원되지 않습니다.");
        }
    }

    private void Update()
    {
        Debug.Log($"ai:{AIUnits.Count} / user: {PlayerUnits.Count}");
    }
}