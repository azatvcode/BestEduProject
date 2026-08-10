using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private MovementData _currentMovementData;
    private CharacterController _characterController;
    private float _rotationSmoothTime = 0.1f;
    private float _targetRotation;
    private float _rotationVelocity;
    private bool _isGrounded;
    private float _gravityVelocity = 9.81f;
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {   
        Vector3 hozintalMotion = new Vector3(_currentMovementData.direction.x, 0f, _currentMovementData.direction.z)
        * _currentMovementData.speed * Time.fixedDeltaTime;
        Vector3 verticalMotion = new Vector3(0f, -_gravityVelocity, 0f) * Time.fixedDeltaTime;
        if(hozintalMotion != Vector3.zero)
        {
            HandleRotation();
        }
        _characterController.Move(hozintalMotion + verticalMotion);
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
        _targetRotation =
            Mathf.Atan2(
                _currentMovementData.direction.x,
                _currentMovementData.direction.z
            ) * Mathf.Rad2Deg;  

        float rotation = Mathf.SmoothDampAngle(
            transform.eulerAngles.y,
            _targetRotation,
            ref _rotationVelocity,
            _rotationSmoothTime
        );

        transform.rotation = Quaternion.Euler(0f, rotation, 0f);
    }
    private void ApplyGravity()
    {
        _isGrounded = _characterController.isGrounded;
        if(_isGrounded)
        {
            
        }
        else
        {
            
        }
    }
}
