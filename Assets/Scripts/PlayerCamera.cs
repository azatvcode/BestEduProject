using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerCamera : MonoBehaviour
{
    private PlayerInputAction _input;
    private Tweener _zoomTweener;
    private Vector3 _lookInput;
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private float _threshold = 0.1f;
    public float TopClamp = 50.0f;
    public float BottomClamp = -50.0f;
    public float CameraAngleOverride = 0.0f;
    public GameObject _CameraTarget;
    private CinemachineThirdPersonFollow _3rdPersonFollowComp;
    public float zoomSensivity = 0.5f;
    public float zoomTimeAnim = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _input = new PlayerInputAction();
        _input.Enable();
        _3rdPersonFollowComp = GetComponent<CinemachineThirdPersonFollow>();
    }
    void FixedUpdate()
    {   
        CameraRotate();
        CameraZoom();
    }
    private void CameraRotate()
    {
        Vector2 mouseDelta =_input.Player.Look.ReadValue<Vector2>();
        if(mouseDelta.sqrMagnitude > _threshold)
        {
            float deltaTimeMultiplier = 1.0f;

            _cinemachineTargetYaw += mouseDelta.x * deltaTimeMultiplier;
            _cinemachineTargetPitch += mouseDelta.y * deltaTimeMultiplier;
        }
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

            
        _CameraTarget.transform.rotation = Quaternion.Euler(-_cinemachineTargetPitch + CameraAngleOverride,
            _cinemachineTargetYaw, 0.0f);

    }
    private void CameraZoom()
    {
        Vector2 scrollDelta = _input.Player.Scroll.ReadValue<Vector2>();
        if (scrollDelta.y == 0) return;
        float newDistance = _3rdPersonFollowComp.CameraDistance + -scrollDelta.y * zoomSensivity;
        newDistance = Mathf.Clamp(newDistance, 2, 10);
        float currentDistance = _3rdPersonFollowComp.CameraDistance;

        _zoomTweener?.Kill();
        _zoomTweener = DOVirtual.Float(
            currentDistance,
            newDistance,
            zoomTimeAnim,
            (value) =>
            {
                _3rdPersonFollowComp.CameraDistance = value;
            }
        );
    }
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
}
