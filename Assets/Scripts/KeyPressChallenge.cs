using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class KeyPressChallenge : MonoBehaviour
{
    [Header("参照")]
    public WoodManager woodManager;
    public FireManager fireManager;
    public ScoreManager scoreManager;

    [Header("UI")]
    public Text challengeKeyText;
    public Text challengeCountText;
    public Text comboText;

    [Header("設定")]
    public KeyCode[] possibleKeys;
    public int minPress = 5;
    public int maxPress = 15;
    public float fuelPerWood = 20f;

    [Header("オート開始設定")]
    public float intervalBetweenChallenges = 1.0f;  // 成功から何秒後に次が始まるか
    private float nextStartTime = 0f;

    [Header("コンボ")]
    public int combo = 0;
    private float lastSuccessTime = -999f;

    private KeyCode currentKey;
    private int requiredPress;
    private int currentPress;
    private bool inChallenge = false;

    void Start()
    {
        // 大量のキーを初期化
        InitKeyList();
        nextStartTime = Time.time + 1f;
    }

    void Update()
    {
        // コンボが切れる処理
        //if (Time.time - lastSuccessTime > comboDecayTime)
        //{
        //    combo = 0;
        //    UpdateComboUI();
        //}

        // ===== チャレンジ中 =====
        if (inChallenge)
        {
            if (Input.GetKeyDown(currentKey))
            {
                currentPress++;
                UpdateChallengeUI();

                if (currentPress >= requiredPress)
                {
                    OnSuccess();
                }
            }
            return;
        }

        // ===== チャレンジ開始（自動）=====
        if (Time.time >= nextStartTime)
        {
            TryStartChallenge();
        }
    }

    // 大量キーリストを設定
    void InitKeyList()
    {
        possibleKeys = new KeyCode[]
        {
            // 上段
            KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T,
            KeyCode.Y, KeyCode.U, KeyCode.I, KeyCode.O, KeyCode.P,

            // 中段
            KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.G,
            KeyCode.H, KeyCode.J, KeyCode.K, KeyCode.L,

            // 下段
            KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V, KeyCode.B,
            KeyCode.N, KeyCode.M,

            // 数字
            KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3,
            KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6,
            KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9,

            // 特殊キー
            KeyCode.Space,
            KeyCode.Return
        };
    }

    void TryStartChallenge()
    {
        inChallenge = true;

        currentKey = possibleKeys[Random.Range(0, possibleKeys.Length)];
        requiredPress = Random.Range(minPress, maxPress + 1);
        currentPress = 0;

        UpdateChallengeUI();
        challengeKeyText.text = $" {currentKey}を押せ！";
    }

    void OnSuccess()
    {
        inChallenge = false;

        woodManager.ConsumeWood(); // 無限木仕様なら常にOK

        combo++;
        lastSuccessTime = Time.time;
        UpdateComboUI();

        float bonusMultiplier = 1f + (combo - 1) * 0.1f;
        float addedFuel = fuelPerWood * bonusMultiplier;

        fireManager.AddFuel(addedFuel, true);

        if (scoreManager != null)
        {
            scoreManager.AddScore(Mathf.RoundToInt(10 * bonusMultiplier));
        }

        challengeKeyText.text = "危ないぜぇ！";
        challengeCountText.text = "";

        // 次のチャレンジ開始までの時間を設定（自動）
        nextStartTime = Time.time + intervalBetweenChallenges;
    }

    void UpdateChallengeUI()
    {
        if (challengeCountText != null)
            challengeCountText.text = $"{currentPress} / {requiredPress}";
    }

    void UpdateComboUI()
    {
        if (comboText != null)
            comboText.text = $"Combo: {combo}";
    }
}
