using UnityEngine;
using UnityEngine.Serialization;

public class Game : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [FormerlySerializedAs("_playerMovement")] [SerializeField] private PlayerMover playerMover;

    private void OnEnable()
    {
        _playerInput.OnMoveInput += playerMover.HandleMoveInput;
        _playerInput.OnJumpButtonPressed += CallJumpOnPlayerMovement;
        _playerInput.OnMouseXInput += playerMover.HandleMouseXInput;
        _playerInput.OnMouseYInput += playerMover.HandleMouseYInput;
    }

    private void OnDisable()
    {
        _playerInput.OnMoveInput -= playerMover.HandleMoveInput;
        _playerInput.OnJumpButtonPressed -= CallJumpOnPlayerMovement;
        _playerInput.OnMouseXInput -= playerMover.HandleMouseXInput;
        _playerInput.OnMouseYInput -= playerMover.HandleMouseYInput;
    }

    private void CallJumpOnPlayerMovement()
    {
        playerMover.Jump();
    }
}