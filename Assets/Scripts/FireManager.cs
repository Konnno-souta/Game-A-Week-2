using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FireManager : MonoBehaviour
{
    [Header("燃料設定")]
    public float maxFuel = 100f;
    public float currentFuel = 100f;
    public float burnSpeed = 1f;   // 1秒あたり何燃料減るか

    [Header("参照")]
    public Slider fuelSlider;                            // UIゲージ
    public YourNamespace.VFX_FireController fireController; // 炎コントローラ

    [Header("コンボカウント")]
    public int comboCount = 0;

    [Header("ゲージの色設定")]
    public Image fuelFillImage;   // Slider の Fill の Image

    [Header("テーマカラー設定（炎 + ゲージ共通）")]
    public Color fireLowColor = new Color(0.7f, 0.1f, 0.0f);     // 暗赤
    public Color fireMidColor = new Color(1.0f, 0.5f, 0.0f);     // オレンジ
    public Color fireHighColor = new Color(1.0f, 0.9f, 0.3f);    // 黄色

    [Header("ゲージの揺れ設定")]
    public RectTransform fuelSliderRect;  // Slider全体を揺らす
    public float shakeAmount = 5f;        // 揺れの強さ
    public float shakeSpeed = 20f;        // 揺れスピード
    Vector3 originalPos;                  // 元の位置

    [Header("リザルト管理")]
    // 正しく false で初期化：ゲーム開始時はまだゲームオーバーではない
    bool isGameOver = false;
    public StopwatchManager stopwatchManager;

    void Start()
    {
        currentFuel = maxFuel;

        if (fuelSlider != null)
        {
            fuelSlider.minValue = 0f;
            fuelSlider.maxValue = maxFuel;
            fuelSlider.value = currentFuel;
        }

        if (fuelSliderRect != null)
            originalPos = fuelSliderRect.anchoredPosition;
    }

    void Update()
    {
        if (isGameOver) return; // 既にゲームオーバーなら何もしない（安全策）

        currentFuel -= burnSpeed * Time.deltaTime;
        currentFuel = Mathf.Clamp(currentFuel, 0f, maxFuel);

        UpdateUI();
        UpdateFireIntensity();

        // --- ゲームオーバー判定 ---
        if ((currentFuel <= 0f || fireController.GetFireIntensity() <= 0.1f) && !isGameOver)
        {
            isGameOver = true;
            GameOver();
        }

    }

    /// <summary>
    /// 木材投入で燃料を増やす
    /// </summary>
    public void AddFuel(float amount, bool visualBoost = true)
    {
        if (isGameOver) return; // ゲームオーバー後は無効

        currentFuel += amount;
        currentFuel = Mathf.Clamp(currentFuel, 0f, maxFuel);

        comboCount++;

        UpdateUI();

        // 基本の見た目更新
        UpdateFireIntensity();

        if (visualBoost && fireController != null)
        {
            // 木材投入直後に「火がバッと強くなる」演出
            float boost = 0.3f;  // 好みに応じて 0.1から1.0 で調整
            float boostedIntensity = Mathf.Clamp(fireController.GetFireIntensity() + boost, 0f, 4f);
            fireController.SetFireIntensity(boostedIntensity);
        }
    }

    /// <summary>
    /// スライダー更新
    /// </summary>
    void UpdateUI()
    {
        if (fuelSlider != null)
        {
            // スムーズに追従（fuelSlider.maxValue == maxFuel を前提）
            fuelSlider.value = Mathf.Lerp(fuelSlider.value, currentFuel, Time.deltaTime * 8f);

            float fuel01 = fuelSlider.value / maxFuel;

            if (fuelFillImage != null)
            {
                // テーマカラーに基づく補間
                Color themeColor = GetThemeColor(fuel01);
                fuelFillImage.color = themeColor;
            }

            // 揺れ処理
            if (fuelSliderRect != null)
            {
                if (fuel01 < 0.25f)
                {
                    float shakeX = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
                    float shakeY = Mathf.Cos(Time.time * shakeSpeed * 0.8f) * shakeAmount;

                    fuelSliderRect.anchoredPosition = originalPos + new Vector3(shakeX, shakeY, 0);
                }
                else
                {
                    fuelSliderRect.anchoredPosition = originalPos;
                }
            }
        }
    }

    /// <summary>
    /// VFXの炎強度を燃料に合わせて変化させる
    /// </summary>
    void UpdateFireIntensity()
    {
        if (fireController == null) return;

        float fuel01 = currentFuel / maxFuel;

        // 炎の強度
        float targetIntensity = Mathf.Lerp(0.1f, 3f, fuel01);
        float currentIntensity = fireController.GetFireIntensity();
        float finalIntensity = Mathf.Lerp(currentIntensity, targetIntensity, Time.deltaTime * 5f);

        fireController.SetFireIntensity(finalIntensity);

        // 炎の色もテーマカラーで同期（fireController が SetFireColor を持つこと）
        Color themeColor = GetThemeColor(fuel01);
        fireController.SetFireColor(themeColor);
    }

    Color GetThemeColor(float t)
    {
        if (t < 0.3f)
            return Color.Lerp(fireLowColor, fireMidColor, t / 0.3f);

        if (t < 0.7f)
            return Color.Lerp(fireMidColor, fireHighColor, (t - 0.3f) / 0.4f);

        return fireHighColor;
    }

    public int GetMaxCombo()
    {
        return comboCount;

    }

    void GameOver()
    {
        // safety: stopwatchManager がセットされていれば停止し、ElapsedTime プロパティを利用
        if (stopwatchManager != null)
        {
            stopwatchManager.StopTimer();
            ResultData.survivedTime = stopwatchManager.ElapsedTime; // StopwatchManager に ElapsedTime の public getter を用意すること
        }
        else
        {
            Debug.LogWarning("StopwatchManager not set on FireManager!");
            ResultData.survivedTime = 0f;
        }

        ResultData.maxCombo = comboCount;

        // シーン名は必ず Project のシーン名と一致・Build Settings に登録
        SceneManager.LoadScene("Result");
    }

    // オプション：外部から強制的に GameOver を呼べるようにしておくと便利
    public void ForceGameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            GameOver();
        }
    }
}

