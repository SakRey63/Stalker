using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";
    private const string JumpButton = "Jump";
    private const string MouseXAxis = "Mouse X";
    private const string MouseYAxis = "Mouse Y";

    public event Action<Vector3> OnMoveInput;
    public event Action OnJumpButtonPressed;
    public event Action<float> OnMouseXInput;
    public event Action<float> OnMouseYInput;

    private void Update()
    {
        float horizontalInput = Input.GetAxis(HorizontalAxis);
        float verticalInput = Input.GetAxis(VerticalAxis);
        
        Vector3 moveInput = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (moveInput.magnitude > 0)
        {
            OnMoveInput?.Invoke(moveInput);
        }
        else
        {
            OnMoveInput?.Invoke(Vector3.zero);
        }

        if (Input.GetButtonDown(JumpButton))
        {
            OnJumpButtonPressed?.Invoke();
        }

        float mouseXInput = Input.GetAxis(MouseXAxis);
        
        OnMouseXInput?.Invoke(mouseXInput);

        float mouseYInput = Input.GetAxis(MouseYAxis);
        
        OnMouseYInput?.Invoke(mouseYInput);
    }
}