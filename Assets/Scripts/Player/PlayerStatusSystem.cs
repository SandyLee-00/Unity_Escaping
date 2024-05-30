using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 플레이어 체력 시스템
/// 필요한 곳에 이벤트를 등록하여 사용하기
/// </summary>
public class PlayerStatusSystem : MonoBehaviour
{
    public event Action OnChangeStatus;

    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    public event Action OnInvincibilityEnd;

    [SerializeField]
    private float hpChangeDelay = 0.5f;

    private float timeSinceLastChange = float.MaxValue;
    private bool isAttacked = false;

    public float CurrentHP { get; private set; }
    public float CurrentMP { get; private set; }
    public float MaxHP => playerStatHandler.CurrentStat.maxHP;
    public float MaxMP => playerStatHandler.CurrentStat.maxMP;

    private PlayerStatHandler playerStatHandler;

    private void Awake()
    {
        playerStatHandler = gameObject.GetOrAddComponent<PlayerStatHandler>();
    }

    private void Start()
    {
        CurrentHP = MaxHP;
        CurrentMP = MaxMP;
    }

    void Update()
    {
        if (isAttacked && timeSinceLastChange < hpChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;

            if (timeSinceLastChange >= hpChangeDelay)
            {
                OnInvincibilityEnd?.Invoke();
                isAttacked = false;
            }
        }
    }

    public bool ChangeHP(float change)
    {
        // 체력이 0이면 false 반환
        if (timeSinceLastChange < hpChangeDelay)
        {
            return false;
        }

        OnChangeStatus?.Invoke();

        timeSinceLastChange = 0f;
        CurrentHP += change;
        CurrentHP = Mathf.Clamp(CurrentHP, 0, MaxHP);

        if (CurrentHP <= 0f)
        {
            CallDeath();
            return true;
        }
        if (change > 0)
        {
            OnHeal?.Invoke();
        }
        else
        {
            OnDamage?.Invoke();
            isAttacked = true;
        }
        return true;
    }

    public bool ChangeMP(float change)
    {
        // 마나가 부족하면 false 반환
        if (change < 0 && CurrentMP < Mathf.Abs(change))
        {
            return false;
        }

        OnChangeStatus?.Invoke();

        CurrentMP += change;
        CurrentMP = Mathf.Clamp(CurrentMP, 0, MaxMP);

        return true;
    }

    private void CallDeath()
    {
        OnDeath?.Invoke();
    }
}
