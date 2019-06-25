public class FallingState : IState {
    private StateManager _stateManager;
    
    public FallingState( StateManager sm) {
        _stateManager = sm;
    }
    public void EnterState() {
      
    }
    
    public void FixedUpdate()
    {
        
    }

    public void Update()
    {
        _stateManager.ChangeState(SphereState.Stay);
    }
}
