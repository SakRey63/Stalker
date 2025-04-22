using UnityEngine;

public class BotMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _stoppingDistance = 2f;
    [SerializeField] private Transform _target;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _maxVerticalSpeed = 5f;
    [SerializeField] private float _gravityScale = 1f;
    [SerializeField] private float _groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask _groundLayers;

    private bool _isGrounded;
    private float _sqrStoppingDistance;

    void Start()
    {
        InitializeRigidbody();
        CalculateStoppingDistanceSqr();
    }

    void FixedUpdate()
    {
        CheckGround();
        MoveTowardsTarget();
        RotateTowardsTarget();
    }

    private void InitializeRigidbody()
    {
        _rigidbody.freezeRotation = true;
        _rigidbody.useGravity = true;
    }

    private void CalculateStoppingDistanceSqr()
    {
        _sqrStoppingDistance = _stoppingDistance * _stoppingDistance;
    }

    private void CheckGround()
    {
        _isGrounded = Physics.CheckSphere(transform.position, _groundCheckDistance, _groundLayers);
    }

    private void MoveTowardsTarget()
    {
        if (_target != null)
        {
            Vector3 directionToTarget = (_target.position - transform.position);
            float sqrDistanceToTarget = directionToTarget.sqrMagnitude;
            directionToTarget.Normalize();

            Vector3 horizontalDirection = new Vector3(directionToTarget.x, 0f, directionToTarget.z).normalized;
            Vector3 targetVelocity = horizontalDirection * _moveSpeed;

            AdjustVerticalVelocity(ref targetVelocity);

            if (sqrDistanceToTarget > _sqrStoppingDistance)
            {
                _rigidbody.velocity = targetVelocity;
            }
            else
            {
                _rigidbody.velocity = new Vector3(0f, _rigidbody.velocity.y, 0f);
            }
        }
        else
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }

    private void AdjustVerticalVelocity(ref Vector3 targetVelocity)
    {
        if (!_isGrounded)
        {
            targetVelocity.y = _rigidbody.velocity.y;
        }
        else
        {
            targetVelocity.y = Mathf.Max(_rigidbody.velocity.y, 0f);
        }

        if (targetVelocity.y > _maxVerticalSpeed && !_isGrounded)
        {
            targetVelocity.y = _maxVerticalSpeed;
        }
    }

    private void RotateTowardsTarget()
    {
        if (_target != null)
        {
            Vector3 directionToTarget = (_target.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(directionToTarget.x, 0f, directionToTarget.z));
            _rigidbody.rotation = targetRotation;
        }
    }
}