using UnityEngine;

public class FieldGenerator: MonoBehaviour
{
    [SerializeField]
    private GameObject fieldPrefab;
    [SerializeField]
    private Vector3 lastComponentPosition = new Vector3(0,-1,1);
    [SerializeField]
    private float minimumDistanceToGenerate = 2;
    [SerializeField]
    private Transform pathParent;

    private ObjectPool _fieldPool;
    private Vector3 _componentSize;

    private void Awake()
    {
        _fieldPool = new ObjectPool();
        _fieldPool.InitPool(20, fieldPrefab, pathParent);
    }

    private void Start()
    {
        _componentSize = fieldPrefab.GetComponent<BoxCollider>().size;
        while (TryGenerateNextComponent(Vector3.zero)) {
        }
    }
    private void OnEnable()
    {
        SphereData.OnPositionChanged += OnVisibleFieldChanged;
    }

    private void OnDisable()
    {
        SphereData.OnPositionChanged -= OnVisibleFieldChanged;
    }

    
    private void OnVisibleFieldChanged(Vector3 position)
    {
        TryGenerateNextComponent(position);
    }


    private bool TryGenerateNextComponent(Vector3 gamePosition)
    {
        if ((lastComponentPosition - gamePosition).magnitude < minimumDistanceToGenerate)
        {
            return GeneratePathComponent();
        }
        return false;
    }

    private bool GeneratePathComponent()
    {       
        var isGenerated =_fieldPool.TryGetObject(out var pathPart);

        if (!isGenerated)
            return false;
       
        var shiftDir = Random.Range(0, 2)==1? Vector3.right : Vector3.forward;
        var position = lastComponentPosition + new Vector3( _componentSize.x*shiftDir.x, 0, _componentSize.z*shiftDir.z);
        pathPart.transform.position = position;

        lastComponentPosition = position;

        return true;
    }
}
