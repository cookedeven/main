using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TMP_Text timerText;
    public float totalTime = 60f; // 총 게임 시간 (초 단위)
    private float currentTime;

    private bool isGameOver = false;

    void Start()
    {
        currentTime = totalTime;
    }

    void Update()
    {
        if (isGameOver) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            isGameOver = true;
            timerText.text = "Time’s Up!";
            // 여기서 게임 오버 로직 호출 가능
            return;
        }

        timerText.text = "Time: " + Mathf.CeilToInt(currentTime);
    }
}
