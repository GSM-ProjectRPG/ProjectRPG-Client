using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�÷��̾� �Է´�� Ŭ����
public class PlayerInputHandler : MonoBehaviour
{
    //ȭ��ǥŰ �Է°� ��ȯ
    public Vector3 GetMoveInput()
    {
        return new Vector3(
                            Input.GetAxisRaw("Horizontal"),
                            0,
                            Input.GetAxisRaw("Vertical")
                          );
    }

    //���콺 X��ǥ ��ȭ��
    public float GetLookInputHorizontal()
    {
        return Input.GetAxis("Mouse X");
    }

    //���콺 Y��ǥ ��ȭ��
    public float GetLookInputVertical()
    {
        return Input.GetAxis("Mouse Y");
    }

    //���콺 �� �Է�
    public float GetInputZoom()
    {
        return Input.GetAxis("Mouse ScrollWheel");
    }
}
