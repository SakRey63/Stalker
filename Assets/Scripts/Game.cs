using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerMovement _playerMovement;

    private void OnEnable()
    {
        _playerInput.OnMoveInput += _playerMovement.HandleMoveInput;
        _playerInput.OnJumpButtonPressed += CallJumpOnPlayerMovement;
        _playerInput.OnMouseXInput += _playerMovement.HandleMouseXInput;
        _playerInput.OnMouseYInput += _playerMovement.HandleMouseYInput;
    }

    private void OnDisable()
    {
        _playerInput.OnMoveInput -= _playerMovement.HandleMoveInput;
        _playerInput.OnJumpButtonPressed -= CallJumpOnPlayerMovement;
        _playerInput.OnMouseXInput -= _playerMovement.HandleMouseXInput;
        _playerInput.OnMouseYInput -= _playerMovement.HandleMouseYInput;
    }

    private void CallJumpOnPlayerMovement()
    {
        _playerMovement.Jump();
    }
}