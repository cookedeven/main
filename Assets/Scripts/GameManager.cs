using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Waiting, Playing, GameOver }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState = GameState.Waiting;
    public float gameDuration = 180f; // ∞‘¿”Ω√∞£
    private float timer;

    public GameObject[] characterPrefabs; // ƒ≥∏Ø≈Õ 4∞≥ «¡∏Æ∆’ µÓ∑œ«œ±‚
    public int[] selectedCharacterIndices = new int[4];

    void Awake()
    {
        // ΩÃ±€≈Ê
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else Destroy(gameObject);
    }

    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if (currentState == GameState.Playing)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                EndGame();
            }
        }
    }

    public void StartGame()
    {
        timer = gameDuration;
        currentState = GameState.Playing;
        // TODO: «√∑π¿ÃæÓµÈ √ ±‚»≠, UI µÓ
    }

    public void EndGame()
    {
        currentState = GameState.GameOver;
        // TODO: Ω¬∆– ∆«¥‹, ¡°ºˆ ∫Ò±≥, ∞·∞˙ UI √‚∑¬
        Debug.Log("∞‘¿” ¡æ∑·");
    }

    public float GetRemainingTime()
    {
        return timer;
    }
}












// »˜»˜»˜§”∆R»˜»˜∆R»˜§”§æ»˜»˜ «–±≥ ºˆæ˜ ¡¯¬• Ω»¥Ÿ »˜»˜»˜»˜§”§æ»˜»˜§”§æ»˜»˜»˜§” ≥ª æ∆±ÓøÓ Ω√¿Ã¿Ã¿Ã¿Ã¿Ã¿Ã§”∞£¿Ã§”§”§”§”§”§”§”æ∆§ø§ø§ø§ø§ø§ø§ø§ø§ø§ø§ø§ø§ø§ø
// ¡¯¬•º˜¡¶∞∞¿∫∞≈¥¬¡¯¬•ø÷¡÷¥¬∞≈¡ˆ?«–±≥∞°ø÷≥™«—≈◊º˜¡¶∏¶∞Ëº”«ÿº≠¡÷¥¬∞≈¡ˆ?∞˙¡¶¥¬ππ¿Ã∏Æπ–∑»¡ˆ?ø÷≥™«—≈◊∞Ëº”¿œ∞≈∏Æ∏¶¡‡º≠ºˆ¡§«“Ω√∞£¿ª¡÷¡ˆ∏¶æ ¡ˆ?¥Î√ºø÷?ø÷?ø÷?ø÷?æ∆¥œ.¥Î√ºø÷??????????????????
// ≥¢»˜∆R ≥¢»˜∆R ≥¢»˜∆R ≥¢»˜∆R ≥¢»˜∆R  ø¿¥√ æ∆∏ﬁ∏Æƒ´≥Î∏∏ π˙Ω· 4¿‹¿Ãæﬂ ≥¢»˜∆R ≥ª∏ˆº”ø°º≠ ƒ´∆‰¿Œ¿Ã »Â∏£¥Ÿ∏¯«ÿ ≥—√ƒ≥≠¥Ÿ ≥¢»˜»˜»˜»˜§”»˜
// ∏∏øÏ¿˝¿Œµ• ¥Î√ºø÷ ≥™«—≈◊ ¿Ã∑± ¿œ¿ª Ω√≈≥±Ó? ªÁΩ« ¿Ã∞« ∏µŒ¥Ÿ ∏∏øÏ¿˝ ¿Â≥≠¿Ã∞Ì ªÁΩ« ≥≠ ¿ÃπÃ ƒ⁄µÂ∏¶ ¥Ÿ ¿€º∫«œ∞Ì ¡Ò∞Ã∞‘ ∞‘¿”«œ∞Ì¿÷¥¬∞‘ æ∆¥“±Ó? ≥≠ ¡ˆ±› πª«œ∞Ì¿÷¥¬∞…±Ó? ¿Ã ∏µÁ∞‘ ¿¸∫Œ ¿Â≥≠¿œ±Ó? ¡¯¬•¿Œ¡ˆ ¿Â≥≠¿Œ¡ˆ ±∏∫–µµ æ»∞°≥◊ §ª§ª§ª§ª§ª§ª§ª
// √™¡ˆ««∆º ∏∏ºº ¡¯¬• ∫π±∏ ±›πÊ «ÿ¡÷≥◊.  ¿Ã¡¶ Ω√«Ë ¡ÿ∫Ò«ÿæﬂ¡ˆ dog∞∞¿∫∞≈ §ª§ª§ª
// 4ø˘ 4¿œ.  ¿Ã∞Õ∏∏ ∫∏∞Ì¿÷¿∏∏Èº≠ ¿Ã∫•∆Æ ∏≈¥œ¿˙ «—π¯ ∏∏µÈæÓ∫¡æﬂµ«¥¬µ• «œ±‚ ≥ π´ µŒ∑∆¥Ÿ§ø§ø§ø