using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    private PlayerData _playerData;
    //private PlayerCamera _playerCamera;
    private PlayerInput _playerInput;

    //private PlayerInputAction _input;
    private Vector2 _moveInput;

    private float _rotationSpeed = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake()
    {
        base.Awake();
        //_playerCamera = GetComponent<PlayerCamera>();
        //_playerData = GetComponent<PlayerData>();
        _playerInput = GetComponent<PlayerInput>();

        //_input = new PlayerInputAction();
        //_input.Enable();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    void FixedUpdate()
    {

    }
}
