using DefaultNamespace;

public class FallingState : IState
{
    private StateManager _stateManager;
    private ScoreManager _scoreManager;

    public FallingState(StateManager sm, ScoreManager scoreManager)
    {
        _stateManager = sm;
        _scoreManager = scoreManager;
    }

    public void EnterState()
    {
        _scoreManager.UpdateByFallenPenalty();
    }

    public void FixedUpdate()
    {
    }

    public void Update()
    {
        _stateManager.ChangeState(SphereState.Stay);
    }
}