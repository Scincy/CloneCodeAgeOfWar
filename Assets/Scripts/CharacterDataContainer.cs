using System.Collections.Generic;
using UnityEngine;
public class CharacterDataContainer : MonoBehaviour
{
    public static CharacterDataContainer Instance;

    public const string CharacterPrefabPath = "Prefab/Characters";
    public Dictionary<string, GameObject> characterPrefabs;

    private void Awake()
    {
        if (Instance is null) Instance = this;
        else
        {
            Debug.LogError("Singleton 인스턴스가 이미 존재합니다. 초기화에 실패했습니다.");
        }

        characterPrefabs = new Dictionary<string, GameObject>();
        LoadCharacterPrefabs();
    }

    private void LoadCharacterPrefabs()
    {
        List<GameObject> prefabList = new List<GameObject>();
        prefabList.AddRange( Resources.LoadAll<GameObject>(CharacterPrefabPath) );

        if (prefabList is null || prefabList.Count == 0)
            throw new System.NullReferenceException($"Prefab이 {CharacterPrefabPath}에 없어요!");

        foreach (GameObject prefab in prefabList)
        {
            characterPrefabs.Add(prefab.name, prefab);
        }
    }

    public GameObject GetPrefab(string targetID)
    {
        if (characterPrefabs.Count == 0)
            throw new System.NullReferenceException("캐릭터 프리팹이 로드되지 않았거나 없습니다.");
        else if (characterPrefabs.TryGetValue(targetID, out GameObject temp))
            return temp;
        else
            throw new System.ArgumentException($"Target Prefab ID ({targetID}) 가 데이터 리스트에 존재하지 않습니다.");
    }
}
