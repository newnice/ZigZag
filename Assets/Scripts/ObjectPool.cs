using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private List<GameObject> _objects;
    public void InitPool(int maxPoolSize, GameObject objectPrefab, Transform parent)    {
        _objects = new List<GameObject>(maxPoolSize);
        for (var i = 0; i < maxPoolSize; i++)
        {
            var obj = GameObject.Instantiate(objectPrefab);
            obj.SetActive(false);
            obj.transform.parent = parent;
            _objects.Add(obj);
        }

    }

    public bool TryGetObject(out GameObject obj)
    {
        obj = _objects.Find(go => !go.activeSelf);
        if (obj == default(GameObject))
            return false;
        obj.SetActive(true);
        return true;
    }

    public void DestroyObject(GameObject obj)
    {
        obj.SetActive(false);
    }

}
