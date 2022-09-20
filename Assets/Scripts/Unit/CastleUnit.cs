using UnityEngine;


public class CastleUnit : Unit
{
    
    protected override void OnDie()
    {
        Debug.Log("END Game!");
    }
}
