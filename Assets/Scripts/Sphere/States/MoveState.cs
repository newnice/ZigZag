using System;
using UnityEngine;

public class MoveState : IState
{
    private Vector3 _direction = Vector3.right;

    private readonly Rigidbody _rigidBody;
    private readonly float _forceScale;
    private readonly StateManager _stateManager;

    public MoveState(StateManager sm, Rigidbody sphereRb, Settings settings)
    {
        _stateManager = sm;
        _rigidBody = sphereRb;
        _forceScale = settings.ForceScale;
    }

    public void EnterState()
    {
        ChangeDirection();
    }

    public void FixedUpdate()
    {
        _rigidBody.AddForce(_direction * _forceScale);
    }

    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        StopMovement();
        if (_direction == Vector3.right)
            _direction = Vector3.forward;
        else
            _direction = Vector3.right;
    }
    
    private void StopMovement()
    {
        _rigidBody.velocity = Vector3.zero;
        _rigidBody.angularVelocity = Vector3.zero;
    }
    
    [Serializable]
    public class Settings
    {
        public float ForceScale = 20f;
    }
}
