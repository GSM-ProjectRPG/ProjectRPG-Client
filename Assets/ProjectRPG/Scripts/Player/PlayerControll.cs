using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�÷��̾� ���� Ŭ����
public class PlayerControll : MonoBehaviour
{
    [Header("�����")]
    [SerializeField] bool _showCameraVerticalAngleRange;
    [Header("ī�޶�")]
    [SerializeField] Camera _camera;
    [SerializeField] Transform _cameraRotateCentor;
    [SerializeField] Transform _cameraLookCentor;
    [SerializeField] float _minCameraDistance;
    [SerializeField] float _maxCameraDistance;
    [SerializeField] float _minCameraVerticalAngle;
    [SerializeField] float _maxCameraVerticalAngle;
    [Header("�÷��̾� �̵�/ȸ�� ����")]
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

    // �̵�,ȸ�� ��� ����Լ�
    public void HandleMovement()
    {
        Vector3 moveInput = _inputHandler.GetMoveInput();

        Vector3 deltaPosition = Vector3.zero;

        deltaPosition += transform.TransformVector(Time.deltaTime * _moveSpeed * moveInput);

        transform.Rotate(new Vector3(0, Time.deltaTime * _ratationSpeed * _inputHandler.GetLookInputHorizontal(), 0));

        transform.position += deltaPosition;
    }

    // ī�޶� ��� �Լ�
    public void HandleControllCamera()
    {
        _curruntCameraRotation.x = transform.rotation.eulerAngles.y;
        RotateCamera(0, _inputHandler.GetLookInputVertical());
        AddCameraDistance(_inputHandler.GetInputZoom());

        UpdateCamera();
    }

    // ī�޶� ��ǥ ����
    public void UpdateCamera()
    {
        _curruntCameraRotation.y = Mathf.Clamp(_curruntCameraRotation.y, _minCameraVerticalAngle, _maxCameraVerticalAngle);
        _curruntCameraDistance = Mathf.Clamp(_curruntCameraDistance, _minCameraDistance, _maxCameraDistance);

        if (_camera == null) return;
        Vector3 cameraPos = _cameraRotateCentor.position + Quaternion.Euler(_curruntCameraRotation.y, _curruntCameraRotation.x, 0) * new Vector3(0, 0, -_curruntCameraDistance);

        _camera.transform.position = cameraPos;
        _camera.transform.LookAt(_cameraLookCentor);
    }

    // ī�޶� ȸ��
    public void RotateCamera(float horizontalDelta, float verticalDelta)
    {
        _curruntCameraRotation = new Vector2(_curruntCameraRotation.x + horizontalDelta, _curruntCameraRotation.y + -verticalDelta);
    }

    // ī�޶� �� ����
    public void AddCameraDistance(float delta)
    {
        _curruntCameraDistance -= delta;
    }
}
