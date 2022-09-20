using UnityEngine;
using UnityEngine.Events;

public abstract class Unit : MonoBehaviour, IUnit
{
    public Team Teams => _team;
    public float HP => _hp;
    public float Armor => _armor;
    public UnityEvent DieEvent => _dieEvent;

    private Team _team;
    private float _hp = 100;
    private float _armor = 0;
    private UnityEvent _dieEvent;

    private void Awake()
    {
        Initialize();
    }

    public virtual void Initialize()
    {
        if (_dieEvent is null) _dieEvent = new UnityEvent();
        _dieEvent.AddListener(OnDie);
    }

    public float TakeDemage(Attack attack)
    {
        float actualDemage = Armor - attack.GetDemageAmount();
        if (actualDemage <= 0) return HP; //Miss Demage
        if ((HP - actualDemage) <= 0) // Dead
        {
            DieEvent.Invoke();
            return 0;
        }
        else
        {
            _hp = _hp - actualDemage;
            return HP;
        }
    }

    protected virtual void OnDie() { }
}
