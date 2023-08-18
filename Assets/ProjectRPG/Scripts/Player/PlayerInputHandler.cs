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

    //마우스 X좌표 변화량
    public float GetLookInputHorizontal()
    {
        return Input.GetAxis("Mouse X");
    }

    //마우스 Y좌표 변화량
    public float GetLookInputVertical()
    {
        return Input.GetAxis("Mouse Y");
    }

    //마우스 휠 입력
    public float GetInputZoom()
    {
        return Input.GetAxis("Mouse ScrollWheel");
    }
}
