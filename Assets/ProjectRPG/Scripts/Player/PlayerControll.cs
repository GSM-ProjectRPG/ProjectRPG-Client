using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�÷��̾� ���� Ŭ����
public class PlayerControll : MonoBehaviour
{
    PlayerInputHandler _inputHandler;

    void Start()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
    }

    void Update()
    {
        HandleMovement();
    }

    //�̵� ���� ��� ����Լ�
    public void HandleMovement()
    {
        Vector3 deltaPosition = transform.TransformVector(_inputHandler.GetMoveInput()) * Time.deltaTime;

        transform.position += deltaPosition;
    }
}
