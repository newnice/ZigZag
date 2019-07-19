using UnityEngine;
using Zenject;

public class Crystal : MonoBehaviour
{
    private ParticleSystem DestructionEffect { get; set; }
    private GameObject RootObject { get; set; }

    public void Collect()
    {
        RootObject.SetActive(false);
        DestructionEffect.Play();
    }

    public class Pool : MonoMemoryPool<Transform, Vector3, Crystal>
    {
        private Vector3 _relativePos = Vector3.zero;

        protected override void OnCreated(Crystal item)
        {
            base.OnCreated(item);
            item.DestructionEffect = item.GetComponentInChildren<ParticleSystem>();
            var collider = item.GetComponentInChildren<CapsuleCollider>();
            item.RootObject = collider.gameObject;
            _relativePos = new Vector3(0, collider.height * collider.transform.localScale.y, 0);
        }

        protected override void Reinitialize(Transform parent, Vector3 localPos, Crystal item)
        {
            var t = item.transform;
            t.parent = parent;
            t.localPosition = localPos + _relativePos;

            item.RootObject.SetActive(true);
        }
    }
}