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
    public void SendSpawnRequest(SpawnRequest request)
    {
        if (request.Equals(default(SpawnRequest))) Debug.LogWarning("spawn request가 초기화 되지 않은 것 같아요.");

        switch (request.TeamInfo)
        {
            case Team.Blue:
                PlayerUnits.Add(userSpawner.Spawn(request));
                break;
            case Team.Red:
                AIUnits.Add(aiSpawner.Spawn(request));
                break;
            default:
                throw new System.NotSupportedException("스폰 요청에서 Team 정보는 반드시 정의 되어야 합니다.");
        }
    }

    private void Update()
    {
        Debug.Log($"ai:{AIUnits.Count} / user: {PlayerUnits.Count}");
    }
}