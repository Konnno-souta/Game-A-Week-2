using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountdownManager : MonoBehaviour
{
    [Header("UI")]
    public Text countdownText;

    [Header("ゲーム管理スクリプト")]
    public FireManager fireManager;
    public KeyPressChallenge keyPress;

    [Header("カウント設定")]
    public int countdownSeconds = 5;

    //private bool isCounting = false;

    void Start()
    {
        // ゲーム開始直後にカウントダウン開始
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        //isCounting = true;

        // 暫定的に動作を止める
        fireManager.enabled = false;
        keyPress.enabled = false;

        countdownText.gameObject.SetActive(true);

        int time = countdownSeconds;

        while (time > 0)
        {
            countdownText.text = time.ToString();
            yield return new WaitForSeconds(1f);
            time--;
        }

        // 最後に "START!" を 1 秒表示
        countdownText.text = "START!!";
        yield return new WaitForSeconds(1f);

        // カウントダウン終了 → UIを消す
        countdownText.gameObject.SetActive(false);

        // ゲーム開始（スクリプトを有効化）
        fireManager.enabled = true;
        keyPress.enabled = true;

        //isCounting = false;
    }
}
