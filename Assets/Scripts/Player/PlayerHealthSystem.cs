using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 플레이어 체력 시스템
/// 필요한 곳에 이벤트를 등록하여 사용하기
/// </summary>
public class PlayerHealthSystem : MonoBehaviour
{
    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    public event Action OnInvincibilityEnd;

    [SerializeField]
    private float healthChangeDelay = 0.5f;

    private float timeSinceLastChange = float.MaxValue;
    private bool isAttacked = false;

    public float CurrentHealth { get; private set; }
    public float MaxHealth => playerStatHandler.CurrentStat.maxHeath;

    private PlayerStatHandler playerStatHandler;

    private void Awake()
    {
        playerStatHandler = gameObject.GetOrAddComponent<PlayerStatHandler>();
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    void Update()
    {
        if (isAttacked && timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;

            if (timeSinceLastChange >= healthChangeDelay)
            {
                OnInvincibilityEnd?.Invoke();
                isAttacked = false;
            }
        }
    }

    public bool ChangeHealth(float change)
    {
        if (timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        timeSinceLastChange = 0f;
        CurrentHealth += change;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        if (CurrentHealth <= 0f)
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

    private void CallDeath()
    {
        OnDeath?.Invoke();
    }
}
