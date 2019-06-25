using System;
using System.Collections.Generic;
using UnityEngine;

public class FieldGenerator {
    private Vector3 _lastComponentPosition;
    private readonly float _minGenDistance;
    private readonly float _maxDestroyDistance;
    private readonly FieldPart.Pool _pool;
    private Queue<FieldPart> _activeFieldParts;

    public FieldGenerator(Settings settings, FieldPart.Pool pool) {
        _lastComponentPosition = settings.StartPosition;
        _minGenDistance = settings.MinimumDistanceToGenerate;
        _maxDestroyDistance = settings.MaxDistanceToDestroy;
        _pool = pool;

        _activeFieldParts = new Queue<FieldPart>();
        //  _activeFieldParts.Enqueue(settings.StartField);
        var counter = 10;
        while (TryGenerateNextComponent(Vector3.zero) && counter-- > 0) { }
    }

    public void OnVisibleFieldChanged(Vector3 position) {
        TryGenerateNextComponent(position);
        TryDestroyUnusedComponents(position);
    }

    private void TryDestroyUnusedComponents(Vector3 position) {
        while (_activeFieldParts.Count > 0) {
            var part = _activeFieldParts.Peek();
            var posShift = position - part.transform.position;
            if (posShift.z > _maxDestroyDistance) {
                _pool.Despawn(part);
                _activeFieldParts.Dequeue();
            }
            else {
                break;
            }
        }
    }


    private bool TryGenerateNextComponent(Vector3 gamePosition) {
        if ((_lastComponentPosition - gamePosition).magnitude < _minGenDistance) {
            return GeneratePathComponent();
        }

        return false;
    }

    private bool GeneratePathComponent() {
        var part = _pool.Spawn(_lastComponentPosition);
        _lastComponentPosition = part.Position;
        _activeFieldParts.Enqueue(part);
        return true;
    }

    [Serializable]
    public class Settings {
        public Vector3 StartPosition = new Vector3(0, -1, 1);
        public float MinimumDistanceToGenerate = 2;
        public float MaxDistanceToDestroy = 1;
        public FieldPart StartField;
    }
}