using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private MovementData _currentMovementData;
    private CharacterController _characterController;
    private float _rotationSmoothTime = 0.1f;
    private float _targetRotation;
    private float _rotationVelocity;
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
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
        if (_currentMovementData.direction == Vector3.zero) return;
        HandleRotation();
        _characterController.Move(_currentMovementData.direction * _currentMovementData.speed * Time.fixedDeltaTime);

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
        _targetRotation = Mathf.Atan2(_currentMovementData.direction.x, _currentMovementData.direction.z) * Mathf.Rad2Deg;  

        float rotation = Mathf.SmoothDampAngle(
            transform.eulerAngles.y,
            _targetRotation,
            ref _rotationVelocity,
            _rotationSmoothTime
        );

        transform.rotation = Quaternion.Euler(0f, rotation, 0f);
    }
}
