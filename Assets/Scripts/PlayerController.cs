using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private bool _isPlayer1 = true;  // Flag to check if the player is Player 1
    [SerializeField, Range(0, 1000), Tooltip("Player max Speed")]
    private float _maxSpeed;  // Maximum speed of the player
    [SerializeField, Range(0, 1000), Tooltip("Player acceleration")]
    private float _acceleration;  // Acceleration of the player
    [SerializeField, Range(0, 10000), Tooltip("Player Jump Height")]
    private float _jumpHeight;  // Jump height of the player
    private Rigidbody _rigidbody;  // Reference to the Rigidbody component
    [SerializeField]
    private Vector3 _groundCheck;  // Position to check if the player is grounded
    [SerializeField]
    private float _groundCheckRadius;  // Radius of the ground check sphere
    private bool _jumpInput = false;  // Flag to check if the jump input is pressed
    private bool _isGrounded = false;  // Flag to check if the player is grounded
    private Vector3 _moveDirection;  // Direction in which the player is moving

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();  // Get the Rigidbody component
        Debug.Assert(_rigidbody != null, "Rigidbody is null");  // Assert that the Rigidbody component is not null
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialization code here
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is Player 1
        if (_isPlayer1)
        {
            // Get the horizontal input for Player 1
            _moveDirection = new Vector3(Input.GetAxisRaw("Player1Horizontal"), 0, 0);
            // Check if the jump input for Player 1 is pressed
            _jumpInput = Input.GetAxisRaw("Player1Jump") != 0;
        }
        else
        {
            // Get the horizontal input for Player 2
            _moveDirection = new Vector3(Input.GetAxisRaw("Player2Horizontial"), 0, 0);
            // Check if the jump input for Player 2 is pressed
            _jumpInput = Input.GetAxisRaw("Player2Jump") != 0;
        }
    }

    private void FixedUpdate()
    {
        // Check if the player is grounded
        _isGrounded = Physics.OverlapSphere(transform.position + _groundCheck, _groundCheckRadius).Length > 1;
        // Apply force to the player in the direction of movement
        _rigidbody.AddForce(_moveDirection * _acceleration * Time.fixedDeltaTime, ForceMode.VelocityChange);

        // Clamp the velocity of the player to the maximum speed
        Vector3 velocity = _rigidbody.velocity;
        float newXSpeed = Mathf.Clamp(_rigidbody.velocity.x, -_maxSpeed, _maxSpeed);
        velocity.x = newXSpeed;
        _rigidbody.velocity = velocity;

        // If the jump input is pressed and the player is grounded, make the player jump
        if (_jumpInput && _isGrounded)
        {
            float force = Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y);
            _rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
        }
    }

#if UNITY_EDITOR
    // Draw a sphere in the editor to visualize the ground check
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + _groundCheck, _groundCheckRadius);
    }
#endif
}
