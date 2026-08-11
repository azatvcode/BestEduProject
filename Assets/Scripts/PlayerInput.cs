using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private CharacterMovement _characterMovement;
    [SerializeField] private PlayerCamera _playerCamera;
    private Vector2 _inputMove;
    //private PlayerInputAction _input;
    private bool _isSprint;
    private float _sprintSpeed = 5;
    private float _walkSpeed = 2;
    private Vector2 _inputDirection;
    private Vector2 _look;
    private Vector2 _scroll;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _characterMovement = GetComponent<CharacterMovement>();
        _playerCamera = FindAnyObjectByType<PlayerCamera>();
    }
    void FixedUpdate()
    {
        UpdateInputData();
    }
    public void OnMove(InputValue value)
    {
        _inputDirection = value.Get<Vector2>();
    }
    public void OnSprint(InputValue value)
    {
        _isSprint = value.isPressed;
    }
    public void OnLook(InputValue value)
    {
        _playerCamera.SetMouseDelta(value.Get<Vector2>());
    }
    public void OnScroll(InputValue value)
    {
        _playerCamera.SetScrollDelta(value.Get<Vector2>());
    }
    private void UpdateInputData()
    {
        Vector3 forward = _playerCamera.GetForwardVector();
        Vector3 right = _playerCamera.GetRightVector();
        right.y = 0f;
        forward.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 direction = (forward * _inputDirection.y) + (right * _inputDirection.x);
        direction.Normalize();

        
        MovementData input = new MovementData
        {
            direction = direction,
            speed = _isSprint ? _sprintSpeed : _walkSpeed
        };
        _characterMovement.SetMovementFromData(input);
    }
}
