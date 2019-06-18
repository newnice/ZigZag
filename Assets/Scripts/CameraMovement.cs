using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform _cameraTransform;
    private Vector3 _prevCenterPosition = Vector3.zero;
    private Vector3 _positionToMove;
    private Vector3 _startMovePosition;
    [SerializeField]
    [Range(0, 1)]
    private float moveAccuracyKoeff = 0.1f;
    private float _moveProgress  = -1f;

    private void Awake()
    {
         var camera = GetComponent<Camera>();
        _cameraTransform = camera.transform;
        _prevCenterPosition = Vector3.zero;
        _positionToMove = _cameraTransform.position;
    }
    private void OnEnable()
    {
        SphereData.OnPositionChanged += StartCameraMovement;       
    }

    private void OnDisable()
    {
        SphereData.OnPositionChanged -= StartCameraMovement;
    }

    public void FixedUpdate()
    {
        if (_moveProgress !=-1)
        {
            _moveProgress += moveAccuracyKoeff;
            _cameraTransform.position = Vector3.Lerp(_startMovePosition, _positionToMove, _moveProgress);
            if (_moveProgress >= 1)
                _moveProgress = -1;
        }

    }
    private void StartCameraMovement(Vector3 centerPosition)
    {
        _moveProgress = 0;
        _startMovePosition = _cameraTransform.position;
        _positionToMove += (centerPosition - _prevCenterPosition);
        _prevCenterPosition = centerPosition;

    }

}
