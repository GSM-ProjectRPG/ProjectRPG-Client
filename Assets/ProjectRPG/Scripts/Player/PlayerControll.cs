using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어 구현 클래스
public class PlayerControll : MonoBehaviour
{
    [Header("디버그")]
    [SerializeField] bool _showCameraVerticalAngleRange;
    [Header("카메라")]
    [SerializeField] Camera _camera;
    [SerializeField] Transform _cameraRotateCentor;
    [SerializeField] Transform _cameraLookCentor;
    [SerializeField] float _minCameraDistance;
    [SerializeField] float _maxCameraDistance;
    [SerializeField] float _minCameraVerticalAngle;
    [SerializeField] float _maxCameraVerticalAngle;
    [Header("플레이어 이동/회전 관련")]
    [SerializeField] float _moveSpeed;
    [SerializeField] float _ratationSpeed;

    PlayerInputHandler _inputHandler;

    Vector2 _curruntCameraRotation;
    float _curruntCameraDistance;

    void Start()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
    }

    void Update()
    {
        HandleMovement();
        HandleControllCamera();
    }

    private void OnDrawGizmos()
    {
        if (_showCameraVerticalAngleRange)
        {
            Debug.DrawRay(_cameraRotateCentor.position, transform.rotation * Quaternion.Euler(_minCameraVerticalAngle, 0, 0) * new Vector3(0, 0, -_maxCameraDistance), Color.red);
            Debug.DrawRay(_cameraRotateCentor.position, transform.rotation * Quaternion.Euler(_maxCameraVerticalAngle, 0, 0) * new Vector3(0, 0, -_maxCameraDistance), Color.red);
        }
    }

    // 이동,회전 기능 담당함수
    public void HandleMovement()
    {
        Vector3 moveInput = _inputHandler.GetMoveInput();

        Vector3 deltaPosition = Vector3.zero;

        deltaPosition += transform.TransformVector(Time.deltaTime * _moveSpeed * moveInput);

        transform.Rotate(new Vector3(0, Time.deltaTime * _ratationSpeed * _inputHandler.GetLookInputHorizontal(), 0));

        transform.position += deltaPosition;
    }

    // 카메라 담당 함수
    public void HandleControllCamera()
    {
        _curruntCameraRotation.x = transform.rotation.eulerAngles.y;
        RotateCamera(0, _inputHandler.GetLookInputVertical());
        AddCameraDistance(_inputHandler.GetInputZoom());

        UpdateCamera();
    }

    // 카메라 좌표 갱신
    public void UpdateCamera()
    {
        _curruntCameraRotation.y = Mathf.Clamp(_curruntCameraRotation.y, _minCameraVerticalAngle, _maxCameraVerticalAngle);
        _curruntCameraDistance = Mathf.Clamp(_curruntCameraDistance, _minCameraDistance, _maxCameraDistance);

        if (_camera == null) return;
        Vector3 cameraPos = _cameraRotateCentor.position + Quaternion.Euler(_curruntCameraRotation.y, _curruntCameraRotation.x, 0) * new Vector3(0, 0, -_curruntCameraDistance);

        _camera.transform.position = cameraPos;
        _camera.transform.LookAt(_cameraLookCentor);
    }

    // 카메라 회전
    public void RotateCamera(float horizontalDelta, float verticalDelta)
    {
        _curruntCameraRotation = new Vector2(_curruntCameraRotation.x + horizontalDelta, _curruntCameraRotation.y + -verticalDelta);
    }

    // 카메라 줌 조절
    public void AddCameraDistance(float delta)
    {
        _curruntCameraDistance -= delta;
    }
}
