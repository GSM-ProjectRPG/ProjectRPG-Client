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
}
