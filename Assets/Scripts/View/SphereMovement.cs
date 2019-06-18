using System.Collections;
using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    private SpherePresenter _presenter;
    private Rigidbody _rigidbody;
    private Vector3 _spherePosShift;
    [SerializeField]
    private float forceScale = 1f;

    public Vector3 Position { get => transform.position; internal set => transform.position = value; }

    void Start()
    {
        _presenter = new SpherePresenter(this);
        _rigidbody = GetComponent<Rigidbody>();
        var collider = GetComponent<SphereCollider>();
        _spherePosShift = new Vector3(0, collider.radius, 0);
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1") && _presenter.IsAlive())
        {
            StopMovement();
            _presenter.UpdateMovement();
        }        
    }

    private void FixedUpdate()
    {
        if (_presenter!=null&&_presenter.TryMove(out var dir))
        {     
            _rigidbody.AddForce( dir  * forceScale);
        }        
    }

 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(Constants.TAG_PLATFORM) && _presenter != null) {
            _presenter.SavePosition(collision.collider.transform.position + _spherePosShift);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag(Constants.TAG_PLATFORM) && _presenter != null)
        {
            _presenter.DestroyPlatform(collision.collider);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_KILL_ZONE))
        {
            _presenter.KillSphereSlowly();
            StartCoroutine("ExecuteBeforeDie");
           
        }
    }

    private IEnumerator ExecuteBeforeDie() {
        yield return new WaitForSeconds(1);
        StopMovement();
        _presenter.AwakeSphere();
    }

    private void StopMovement()
    {
        _rigidbody.velocity = Vector3.zero;
         _rigidbody.angularVelocity = Vector3.zero;
    }

}
