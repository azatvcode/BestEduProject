using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerCamera : MonoBehaviour
{
    private Tweener _zoomTweener;
    private Vector3 _lookInput;
    private Vector2 _mouseDelta;
    private Vector2 _scrollDelta;
    [SerializeField] private GameObject _cameraTarget;
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private float _threshold = 0.1f;
    public float TopClamp = 50.0f;
    public float BottomClamp = -50.0f;
    public float CameraAngleOverride = 0.0f;
    private CinemachineThirdPersonFollow _3rdPersonFollowComp;
    public float zoomSensivity = 0.5f;
    public float zoomTimeAnim = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _3rdPersonFollowComp = GetComponent<CinemachineThirdPersonFollow>();
    }
    void FixedUpdate()
    {   
        CameraRotate();
        CameraZoom();
    }
    private void CameraRotate()
    {
        if(_mouseDelta.sqrMagnitude > _threshold)
        {
            float deltaTimeMultiplier = 1.0f;

            _cinemachineTargetYaw += _mouseDelta.x * deltaTimeMultiplier;
            _cinemachineTargetPitch += _mouseDelta.y * deltaTimeMultiplier;
        }
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        _cameraTarget.transform.rotation = Quaternion.Euler(-_cinemachineTargetPitch + CameraAngleOverride,
            _cinemachineTargetYaw, 0.0f);

    }
    private void CameraZoom()
    {
        if (_scrollDelta.y == 0) return;
        float newDistance = _3rdPersonFollowComp.CameraDistance + -_scrollDelta.y * zoomSensivity;
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
    public void SetMouseDelta(Vector2 mouseDelta)
    {
        _mouseDelta = mouseDelta;
    }
    public void SetScrollDelta(Vector2 scrollDelta)
    {
        _scrollDelta = scrollDelta;
    }
    public Vector3 GetForwardVector()
    {
        return transform.forward;
    }
    public Vector3 GetRightVector()
    {
        return transform.right;
    }
}
