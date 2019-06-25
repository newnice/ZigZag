using UnityEngine;

public class StayState : IState
{
    private readonly StateManager _stateManager;
    private readonly Rigidbody _rigidBody;
    private readonly SphereView _sphereModel;
    
    public StayState(StateManager sm, Rigidbody sphereRb, SphereView sphere)
    {
        _stateManager = sm;
        _rigidBody = sphereRb;
        _sphereModel = sphere;
    }
    public void EnterState()
    {
        StopMovement();
        _sphereModel.MoveToLastStablePosition();
    }

    public void FixedUpdate()
    {
    }

    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _stateManager.ChangeState(SphereState.Move);
        }
    }

    private void StopMovement()
    {
        _rigidBody.velocity = Vector3.zero;
        _rigidBody.angularVelocity = Vector3.zero;
    }
}
