using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어 구현 클래스
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

    //이동 관련 기능 담당함수
    public void HandleMovement()
    {
        Vector3 deltaPosition = transform.TransformVector(_inputHandler.GetMoveInput()) * Time.deltaTime;

        transform.position += deltaPosition;
    }
}
