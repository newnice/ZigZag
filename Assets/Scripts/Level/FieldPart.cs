using UnityEngine;
using Zenject;

public class FieldPart : MonoBehaviour {
    public Vector3 Position => transform.position;
    private Vector3 Size { set; get; } = Vector2.zero;
    private Crystal Crystal { get; set; }

    public class Pool : MonoMemoryPool<Vector3, FieldPart> {
        private Crystal.Pool _crystalPool;
        private Vector3 _crystalDefaultPos = Vector3.zero;

        public Pool(Crystal.Pool crystalPool) {
            _crystalPool = crystalPool;
        }

        protected override void OnCreated(FieldPart item) {
            base.OnCreated(item);
            item.Size = item.GetComponent<BoxCollider>().size;
            _crystalDefaultPos = new Vector3(0, item.Size.y / 2, 0);
        }

        protected override void Reinitialize(Vector3 neighbourPosition, FieldPart item) {
            var shiftDir = Random.Range(0, 2) == 1 ? Vector3.right : Vector3.forward;
            var position = neighbourPosition + new Vector3(item.Size.x * shiftDir.x, 0, item.Size.z * shiftDir.z);
            item.transform.position = position;
        }

        protected override void OnDespawned(FieldPart item) {
            base.OnDespawned(item);
            if (item.Crystal != null) {
                _crystalPool.Despawn(item.Crystal);

                item.Crystal = null;
            }
        }

        protected override void OnSpawned(FieldPart item) {
            base.OnSpawned(item);
            if (Random.Range(1, 100) > 80) {
                var crystal = _crystalPool.Spawn(item.transform, _crystalDefaultPos);
                item.Crystal = crystal;
            }
        }
    }
}