using UnityEngine;
using UnityEngine.EventSystems;
public class SpawnUnitButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    public string CharacterID;
    public bool aiSpawnTest = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        SpawnRequest spawnRequest = new SpawnRequest(CharacterID, aiSpawnTest ? Team.Red : Team.Blue);

        SpawnManager.Instance.SendSpawnRequest(spawnRequest);
    }
}