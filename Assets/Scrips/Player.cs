using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")] [SerializeField] private float _speed = 4f;

    [Header("Jump")] [SerializeField] private float _jumpForce = 4f;

    [SerializeField] private Transform _checkGround;
    [SerializeField] private float _raycastLength;
    [SerializeField] private LayerMask _groundLayer;
    private bool _isGrounded;
    private PlayerDash _playerDash;
    private Rigidbody2D _rb;
    public float Direction { get; private set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerDash = GetComponent<PlayerDash>();
    }

    private void Update()
    {
        Direction = Input.GetAxisRaw("Horizontal");
        if (!_playerDash.IsDashing) Jump();
    }

    private void FixedUpdate()
    {
        if (!_playerDash.IsDashing) Move();
    }

    private void Move()
    {
        _rb.velocity = new Vector2(Direction * _speed, _rb.velocity.y);
    }

    private void Jump()
    {
        _isGrounded = Physics2D.Raycast(_checkGround.position, Vector2.down, _raycastLength, _groundLayer);

        if (Input.GetButtonDown("Jump") && _isGrounded) _rb.velocity = Vector2.up * _jumpForce;
    }
}