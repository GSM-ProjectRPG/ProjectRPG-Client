using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    //매개변수 : 받은피해, 공격자
    public Action<float, GameObject> OnHit;

    //attacker로부터 damage 만큼의 피해를 가진 공격을 받음
    public void HandleHit(float damage, GameObject attacker = null)
    {
        OnHit?.Invoke(damage, attacker);
    }
}
