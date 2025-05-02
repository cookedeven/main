using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // 예시: 재료를 획득했을 때
    public event Action<string> OnIngredientCollected;

    public void IngredientCollected(string ingredientName)
    {
        OnIngredientCollected?.Invoke(ingredientName);
    }

    // 예시: 요리 완성
    public event Action<int> OnDishCompleted;

    public void DishCompleted(int playerId)
    {
        OnDishCompleted?.Invoke(playerId);
    }
}