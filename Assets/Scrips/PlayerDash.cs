using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [Header("Dash")] [SerializeField] private float _dashingTime = 0.2f;

    [SerializeField] private float _dashForce = 20f;
    [SerializeField] private float _timeCanDash = 1f;
    private float _baseGravity;
    private bool _canDash = true;
    private Player _player;
    private Rigidbody2D _rb;
    public bool IsDashing { get; private set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();
        _baseGravity = _rb.gravityScale;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) StartCoroutine(Dash());
    }

    private IEnumerator Dash()
    {
        if (_player.Direction != 0 && _canDash)
        {
            IsDashing = true;
            _canDash = false;
            _rb.gravityScale = 0f;
            _rb.velocity = new Vector2(_player.Direction * _dashForce, 0f);
            yield return new WaitForSeconds(_dashingTime);
            IsDashing = false;
            _rb.gravityScale = _baseGravity;
            yield return new WaitForSeconds(_timeCanDash);
            _canDash = true;
        }
    }
}