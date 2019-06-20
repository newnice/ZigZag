using System;
using UnityEngine;

public class SpherePresenter
{
    private readonly SphereData _model;
    private readonly SphereMovement _sphereMovement;
   
    public SpherePresenter(SphereMovement movement)
    {
        _sphereMovement = movement;
        _model = new SphereData
        {
            Position = movement.Position, 
            IsAlive = true
        };
    }
    
    public void UpdateMovement()
    {
        if (!_model.IsAlive) return;

        if (!_model.IsActive || (_model.MoveDirection == Vector3.right))
        {
            _model.MoveDirection = Vector3.forward;            
        } else
        {
            _model.MoveDirection = Vector3.right;            
        }
        _model.IsActive = true;
    }

    internal bool IsAlive()
    {
        return _model.IsAlive;
    }

    internal bool TryMove(out Vector3 direction)
    {
        direction = Vector3.zero;
        if(_model.IsActive)
        {
            direction = _model.MoveDirection;            
        }
        return _model.IsActive;
    }

 

    internal void SavePosition(Vector3 position)
    {
        _model.Position = position;
    }

    public void KillSphereSlowly()
    {
        _model.IsActive = false;
        _model.IsAlive = false;       
    }

    internal void AwakeSphere()
    {
        _model.IsAlive = true;
        _sphereMovement.Position = _model.Position;
    }

    internal void DestroyPlatform(Collider collider)
    {
  
        collider.gameObject.SetActive(false);
    }
}
    
  

