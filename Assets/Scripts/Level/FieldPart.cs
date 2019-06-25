using UnityEngine;
using Zenject;

public class FieldPart : MonoBehaviour {
    public Vector3 Position => transform.position;
    private Vector3 Size { set; get; } = Vector2.zero;
    
    public class Pool : MonoMemoryPool<Vector3, FieldPart> {
        protected override void OnCreated(FieldPart item) {
            base.OnCreated(item);
            item.Size = item.GetComponent<BoxCollider>().size;
        }
        protected override void Reinitialize(Vector3 neighbourPosition, FieldPart item)
        {
            var shiftDir = Random.Range(0, 2) == 1 ? Vector3.right : Vector3.forward;
            var position = neighbourPosition + new Vector3(item.Size.x * shiftDir.x, 0, item.Size.z * shiftDir.z);
            item.transform.position = position;
        }
    }
}