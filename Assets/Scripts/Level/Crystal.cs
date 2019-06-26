using Common;
using UnityEngine;
using Zenject;

public class Crystal : MonoBehaviour {
    private Animator Animator { get; set; }

    public void Collect() {
        Animator.SetBool(Constants.ANIMATION_IS_DEAD, true);
    }

    public class Pool : MonoMemoryPool<Transform, Vector3, Crystal> {
        private Vector3 _relativePos = Vector3.zero;
        protected override void OnCreated(Crystal item) {
            base.OnCreated(item);
            item.Animator = item.GetComponentInChildren<Animator>();
            var collider = item.GetComponentInChildren<CapsuleCollider>();
            _relativePos = new Vector3(0, collider.height*collider.transform.localScale.y, 0);
        }

        protected override void Reinitialize(Transform parent, Vector3 localPos, Crystal item) {
            item.Animator.SetBool(Constants.ANIMATION_IS_DEAD, false);
           
            item.transform.parent = parent;
            item.transform.localPosition = localPos + _relativePos;
        }      
    }
}