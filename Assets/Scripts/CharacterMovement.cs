using DG.Tweening;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private MovementData _currentMovementData;
    [SerializeField]private CharacterController _characterController;
    [SerializeField]private float _rotationSmoothTime = 0.1f;
    private float _targetRotation;
    private float _rotationVelocity;
    private bool _isGrounded;
    private float _gravity = -9.81f;
    private float _gravityVelocity = -2f;
    public LayerMask groundLayer;
    private void Awake()
    {
        //_characterController = GetComponent<CharacterController>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {   
        CheckGrounded();
        HandleGravity();
        HandleRotation();
        HandleMovement();
        //Debug.Log(_gravityVelocity);
        Debug.Log(_isGrounded);
        //_characterController.Move(Vector3.forward * Time.fixedDeltaTime);
    }
    public void SetMovementFromData(MovementData movementData)
    {
        _currentMovementData.direction = movementData.direction;
        _currentMovementData.speed = movementData.speed;
    }
    public void StopMove()
    {
        _currentMovementData.direction = Vector3.zero;
        _currentMovementData.speed = 0;
    }
    private void HandleRotation()
    {
        if(_currentMovementData.direction == Vector3.zero) return;
        _targetRotation = Mathf.Atan2(_currentMovementData.direction.x, _currentMovementData.direction.z) * Mathf.Rad2Deg;  

        float rotation = Mathf.SmoothDampAngle(
            transform.eulerAngles.y,
            _targetRotation,
            ref _rotationVelocity,
            _rotationSmoothTime
        );

        transform.rotation = Quaternion.Euler(0f, rotation, 0f);
    }
    private void CheckGrounded()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y + 0.2f,
        transform.position.z);
        _isGrounded = Physics.CheckSphere(spherePosition,_characterController.radius, groundLayer, QueryTriggerInteraction.Ignore);

    }
    private void HandleGravity()
    {
        if (_isGrounded)
        {
            _gravityVelocity = 0;
        }
        else
        {
            _gravityVelocity += _gravity * Time.fixedDeltaTime;
        }
    }
    private void HandleMovement()
    {
        Vector3 horizontalMotion = new Vector3(_currentMovementData.direction.x, 0f, _currentMovementData.direction.z)
        * _currentMovementData.speed * Time.fixedDeltaTime;

        Vector3 verticalMotion = new Vector3(0f, _gravityVelocity, 0f) * Time.fixedDeltaTime;
        Debug.Log(horizontalMotion);
        //Debug.Log(horizontalMotion);
        //Debug.Log(horizontalMotion + verticalMotion);
        _characterController.Move(horizontalMotion + verticalMotion);
    }
    private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (_isGrounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(
                new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z),
                _characterController.radius);
        }
}
