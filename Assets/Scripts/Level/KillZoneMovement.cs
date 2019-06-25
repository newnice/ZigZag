using UnityEngine;

public class KillZoneMovement : MonoBehaviour
{
    private Vector3 _prevCenterPosition = Vector3.zero;

    public void MoveKillZonePosition(Vector3 centerPosition)
    {
        transform.position += centerPosition - _prevCenterPosition;
        _prevCenterPosition = centerPosition;
    }
}
