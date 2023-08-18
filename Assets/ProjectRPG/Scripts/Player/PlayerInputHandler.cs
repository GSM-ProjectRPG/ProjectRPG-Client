using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어 입력담당 클래스
public class PlayerInputHandler : MonoBehaviour
{
    //화살표키 입력값 반환
    public Vector3 GetMoveInput()
    {
        return new Vector3(
                            Input.GetAxisRaw("Horizontal"),
                            0,
                            Input.GetAxisRaw("Vertical")
                          );
    }
}
