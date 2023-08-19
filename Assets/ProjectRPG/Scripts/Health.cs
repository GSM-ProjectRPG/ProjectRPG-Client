using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //매개변수 : 받은피해, 공격자
    public Action<float, GameObject> OnDamaged;
    //매개변수 : 받은힐량, 치유자
    public Action<float, GameObject> OnHealed;
    //매개변수 : 자신을 처치한 개체
    public Action<GameObject> OnDeath;

    public float MaxHp;
    public float CurruntHp { get; private set; }

    bool _isDead;

    void Start()
    {
        CurruntHp = MaxHp;
    }

    //damageSource로부터 damage 만큼의 피해를 입음
    public void TakeDamage(float damage, GameObject damageSource = null)
    {
        float originHp = CurruntHp;
        CurruntHp = Mathf.Clamp(CurruntHp - damage, 0, MaxHp);

        OnDamaged?.Invoke(originHp - CurruntHp, damageSource);

        if(CurruntHp == 0)
        {
            HandleDie(damageSource);
        }
    }

    //healSource로부터 heal 만큼의 치유를 받음
    public void TakeHeal(float heal, GameObject healSource = null)
    {
        float originHp = CurruntHp;
        CurruntHp = Mathf.Clamp(CurruntHp + heal, 0, MaxHp);

        OnDamaged?.Invoke(CurruntHp - originHp, healSource);
    }

    //killer에게 죽음
    void HandleDie(GameObject killer = null)
    {
        if (_isDead) return;
        _isDead = true;

        OnDeath?.Invoke(killer);
    }
}
