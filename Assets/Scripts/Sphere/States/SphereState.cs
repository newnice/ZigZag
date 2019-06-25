using System.Collections.Generic;
using Zenject;

public enum SphereState
{
    Stay, Move, Falling
}

public interface IState
{
    void EnterState();
    void Update();
    void FixedUpdate();
}
public class StateManager: ITickable, IFixedTickable
{
    private IState _currentState;
    private Dictionary<SphereState, IState> _stateMap = new Dictionary<SphereState, IState>();

    [Inject]
    public void Construct(MoveState ms, StayState ss, FallingState fs)
    {
        _stateMap.Add(SphereState.Stay, ss);
        _stateMap.Add(SphereState.Move, ms);
        _stateMap.Add(SphereState.Falling, fs);

        _currentState = ss;
    }

    public void ChangeState(SphereState newStateType)
    {
        var newState = _stateMap[newStateType];
        if (_currentState == newState) return;

        newState.EnterState();
        _currentState = newState;

    }

    public void FixedTick()
    {
        _currentState.FixedUpdate();
    }

    public void Tick()
    {
        _currentState.Update();
    }
}
