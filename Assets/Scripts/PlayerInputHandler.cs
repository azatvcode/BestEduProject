using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private CharacterMovement _characterMovement;
    [SerializeField] private GameObject _playerCamera;
    private Vector2 _inputMove;
    //private PlayerInputAction _input;
    private bool _isSprint;
    private float _sprintSpeed = 5;
    private float _walkSpeed = 2;
    private Vector2 _inputDirection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _characterMovement = GetComponent<CharacterMovement>();
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
    private void UpdateInputData()
    {
        Vector3 forward = _playerCamera.transform.forward;
        Vector3 right = _playerCamera.transform.right;
        forward.y = 0f;
        right.y = 0f;
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
