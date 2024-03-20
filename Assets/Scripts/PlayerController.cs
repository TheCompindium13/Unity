using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private bool _isPlayer1 = true;
    [SerializeField, Range(0,1000), Tooltip("Player max Speed")]
    private float _maxSpeed;
    [SerializeField, Range(0, 1000), Tooltip("Player acceleration")]
    private float _acceleration;
    [SerializeField, Range(0, 10000), Tooltip("Player Jump Height")]
    private float _jumpHeight;
    private Rigidbody _rigidbody;
    [SerializeField]
    private Vector3 _groundCheck;
    [SerializeField]
    private float _groundCheckRadius;
    private bool _jumpInput = false;
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
    // Update is called once per frame
    void Update()
    {
        if (_isPlayer1)
        {
            _moveDirection = new Vector3(Input.GetAxisRaw("Player1Horizontal"), 0, 0);
            _jumpInput = Input.GetAxisRaw("Player1Jump") != 0;
        }
        else
        {
            _moveDirection = new Vector3(Input.GetAxisRaw("Player2Horizontial"), 0, 0);
            _jumpInput = Input.GetAxisRaw("Player2Jump") != 0;
        }
       
        
    }
    private void FixedUpdate()
    {
        _isGrounded = Physics.OverlapSphere(transform.position + _groundCheck, _groundCheckRadius).Length > 1;
        _rigidbody.AddForce(_moveDirection * _acceleration * Time.fixedDeltaTime, ForceMode.VelocityChange);

        Vector3 velocity = _rigidbody.velocity;
        float newXSpeed = Mathf.Clamp(_rigidbody.velocity.x, -_maxSpeed, _maxSpeed);
        //float newZSpeed = Mathf.Clamp(_rigidbody.velocity.z, -_maxSpeed, _maxSpeed);
        velocity.x = newXSpeed;
        //velocity.z = newZSpeed;
        _rigidbody.velocity = velocity;

        if (_jumpInput && _isGrounded)
        {
            float force = Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y);
            _rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + _groundCheck, _groundCheckRadius);
    }
#endif
}
