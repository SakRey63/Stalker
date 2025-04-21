using UnityEngine;

public class BotMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _stoppingDistance = 2f;
    [SerializeField] private Transform _target;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _maxVerticalSpeed = 5f;
    [SerializeField] private float _gravityScale = 1f;
    [SerializeField] private float _groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask _groundLayers;

    private bool _isGrounded;

    void Start()
    {
        _rb.freezeRotation = true;
        _rb.useGravity = true;
    }

    void FixedUpdate()
    {
        _isGrounded = Physics.CheckSphere(transform.position, _groundCheckDistance, _groundLayers);

        if (_target != null)
        {
            Vector3 directionToTarget = (_target.position - transform.position).normalized;
            float distanceToTarget = Vector3.Distance(transform.position, _target.position);

            Vector3 horizontalDirection = new Vector3(directionToTarget.x, 0f, directionToTarget.z).normalized;
            Vector3 targetVelocity = horizontalDirection * _moveSpeed;

            if (!_isGrounded)
            {
                targetVelocity.y = _rb.velocity.y;
            }
            else
            {
                targetVelocity.y = Mathf.Max(_rb.velocity.y, 0f);
            }

            if (targetVelocity.y > _maxVerticalSpeed && !_isGrounded)
            {
                targetVelocity.y = _maxVerticalSpeed;
            }

            if (distanceToTarget > _stoppingDistance)
            {
                _rb.velocity = targetVelocity;
            }
            else
            {
                _rb.velocity = new Vector3(0f, _rb.velocity.y, 0f);
            }

            Quaternion targetRotation =
                Quaternion.LookRotation(new Vector3(directionToTarget.x, 0f, directionToTarget.z));
            _rb.rotation = targetRotation;
        }
    }
}