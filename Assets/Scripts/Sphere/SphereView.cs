using UnityEngine;
using Zenject;

public class SphereView : MonoBehaviour {
    private StateManager _stateManager;
    private SignalBus _signalBus;
    private Vector3 _spherePosShift;
    private Vector3 _lastStablePosition = Vector3.zero;

    [Inject]
    public void Construct(SignalBus signalBus, StateManager stateManager) {
        _signalBus = signalBus;
        _stateManager = stateManager;
    }

    void Start() {
        var sc = GetComponent<SphereCollider>();
        _spherePosShift = new Vector3(0, sc.radius, 0);
    }


    private void OnCollisionEnter(Collision collision) {
        CheckFieldPart(collision.collider);
    }


    private void CheckFieldPart(Collider collisionCollider) {
        if (collisionCollider.GetComponent<FieldPart>() == null) return;

        var positionOnPlatform = collisionCollider.transform.position + _spherePosShift;
        _lastStablePosition = positionOnPlatform;
        _signalBus.Fire(new PositionChangedSignal {Position = positionOnPlatform});
    }

    private void OnTriggerEnter(Collider other) {
        CheckKillZone(other);
        CheckCrystal(other);
    }

    private void CheckKillZone(Collider other) {
        if (other.GetComponent<KillZoneMovement>() != null) {
            _stateManager.ChangeState(SphereState.Falling);
        }
    }

    private void CheckCrystal(Collider collisionCollider) {
        var crystal = collisionCollider.GetComponent<Crystal>();
        if (crystal == null) return;
        crystal.Collect();
    }
    
    public void MoveToLastStablePosition() {
        transform.position = _lastStablePosition;
    }
}