using System.Collections;
using UnityEngine;

public class RainEventController : MonoBehaviour
{
    [Header("雨オブジェクト（雨のパーティクル）")]
    public GameObject rainEffect;

    [Header("雨サウンド")]
    public AudioSource rainAudio;

    [Header("発生設定")]
    public float minInterval = 10f;  // 次の雨までの最短時間
    public float maxInterval = 20f;  // 次の雨までの最長時間

    [Header("雨の長さ")]
    public float rainDuration = 5f;  // 雨が降る時間

    [Header("開始から雨が発動可能になるまでの猶予時間")]
    public float startDelay = 8f;  // ← ★ここが重要！
    public FireManager fireManager;
    public float rainDamage = 10f;

    void Start()
    {
        // ゲーム開始直後は雨を OFF
        if (rainEffect != null) rainEffect.SetActive(false);
        if (rainAudio != null) rainAudio.Stop();

        // 雨発生ループ開始（スタート猶予あり）
        StartCoroutine(RainLoop());
    }

    IEnumerator RainLoop()
    {
        // ★ ゲーム開始直後の猶予
        yield return new WaitForSeconds(startDelay);

        while (true)
        {
            // ランダムな待ち時間
            float wait = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(wait);

            // 雨開始
            StartRain();

            // 雨継続時間
            yield return new WaitForSeconds(rainDuration);

            // 雨終了
            StopRain();
        }
    }

    void StartRain()
    {
        if (rainEffect != null) rainEffect.SetActive(true);
        if (rainAudio != null) rainAudio.Play();

        if (fireManager != null)
            fireManager.AddFuel(-rainDamage, false);
    }
    void StopRain()
    {
        if (rainEffect != null) rainEffect.SetActive(false);
        if (rainAudio != null) rainAudio.Stop();
    }
}


