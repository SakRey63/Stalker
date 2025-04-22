using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 540f;
    [SerializeField] private float _cameraRotationSpeedX = 200f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _jumpHeight = 1f;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _minCameraAngleX = -89f;
    [SerializeField] private float _maxCameraAngleX = 89f;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private float _stickToGroundForce = -2f;

    private Vector3 _velocity;
    private bool _isGrounded;
    private float _cameraRotationX;

    private Vector3 _currentMoveInput = Vector3.zero;

    private void Start()
    {
        InitializeCursor();
        InitializeCameraRotation();
    }

    private void Update()
    {
        CheckGrounded();
        ApplyGravity();
        MovePlayer();
    }
    
    public void Jump()
    {
        if (_isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }
    }

    public void HandleMoveInput(Vector3 inputVector)
    {
        _currentMoveInput = new Vector3(inputVector.x, 0f, inputVector.z);
    }

    public void HandleMouseXInput(float mouseX)
    {
        transform.Rotate(Vector3.up * mouseX * _rotationSpeed * Time.deltaTime);
    }

    public void HandleMouseYInput(float mouseY)
    {
        _cameraRotationX -= mouseY * _cameraRotationSpeedX * Time.deltaTime;
        _cameraRotationX = Mathf.Clamp(_cameraRotationX, _minCameraAngleX, _maxCameraAngleX);

        _cameraTransform.localEulerAngles = new Vector3(_cameraRotationX, _cameraTransform.localEulerAngles.y, 0f);
    }

    private void InitializeCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void InitializeCameraRotation()
    {
        _cameraRotationX = _cameraTransform.localEulerAngles.x;
    }

    private void CheckGrounded()
    {
        _isGrounded = _controller.isGrounded;

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = _stickToGroundForce;
        }
    }

    private void ApplyGravity()
    {
        _velocity.y += _gravity * Time.deltaTime;
    }

    private void MovePlayer()
    {
        Vector3 moveInput = _currentMoveInput;
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.z).normalized;
        Vector3 forwardMove = _cameraTransform.forward * moveDirection.z;
        Vector3 rightMove = _cameraTransform.right * moveDirection.x;
        Vector3 movement = (forwardMove + rightMove).normalized * _moveSpeed * Time.deltaTime;
        movement.y = _velocity.y * Time.deltaTime;

        _controller.Move(movement);
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        HandleApplicationFocus(hasFocus);
    }

    private void HandleApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}