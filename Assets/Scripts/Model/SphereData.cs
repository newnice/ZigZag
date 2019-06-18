using UnityEngine;

public class SphereData
{
    private Vector3 _position;
    public Vector3 MoveDirection { get; set; }
    public bool IsActive { get; set; }
    public bool IsAlive { get; set; }
    public Vector3 Position { get=> _position; set {
            _position = value;
            OnPositionChanged.Invoke(_position);
        }
    }
    
    public static event System.Action<Vector3> OnPositionChanged;
}
