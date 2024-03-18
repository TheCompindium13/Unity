using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(0,10000), Tooltip("Player max Speed")]
    private float _maxSpeed;
    [SerializeField, Range(0, 10000), Tooltip("Player acceleration")]
    private float _acceleration;
    [SerializeField, Range(0, 100000), Tooltip("Player Jump Force")]
    private float _jumpForce;
    private Rigidbody _rigidbody;
    private bool _isGrounded = false;
    private Vector3 _moveDirection;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Debug.Assert(_rigidbody != null, "Rigidbody is null");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        _isGrounded = true;
    }
    // Update is called once per frame
    void Update()
    {
        _moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0,0);
        //float jumpInput = 0;
        //if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        //{
        //    jumpInput = 1;
        //    _isGrounded = false;
        //}
        //_rigidbody.AddForce(Vector3.up * jumpInput * _jumpForce * Time.deltaTime, ForceMode.Impulse);
       
        
    }
    private void FixedUpdate()
    {
        _rigidbody.AddForce(_moveDirection * _acceleration * Time.fixedDeltaTime, ForceMode.VelocityChange);

        if (_rigidbody.velocity.magnitude > _maxSpeed)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * _maxSpeed;
        }
    }
}
