using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseSoundManager : MonoBehaviour
{
    [Header("サウンド設定")]
    public AudioClip winSE;    // 勝利サウンド
    public AudioClip loseSE;   // 敗北サウンド
    public float volume = 1f;  // 音量

    [Header("シーン設定")]
    public string titleSceneName = "TitleScene"; // タイトルに戻るシーン名
    public string retrySceneName = "MainScene";  // リトライ用シーン名

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    /// <summary>
    /// プレイヤーが勝ったときに呼び出す
    /// </summary>
    public void PlayWinSound()
    {
        Debug.Log("プレイヤー勝利！");
        if (winSE != null)
        {
            audioSource.PlayOneShot(winSE, volume);
        }

        // リザルト演出などを入れる場合はここに
        StartCoroutine(ReturnToTitleAfterDelay());
    }

    /// <summary>
    /// プレイヤーが負けたときに呼び出す
    /// </summary>
    public void PlayLoseSound()
    {
        Debug.Log("プレイヤー敗北...");
        if (loseSE != null)
        {
            audioSource.PlayOneShot(loseSE, volume);
        }

        // リザルト演出などを入れる場合はここに
        StartCoroutine(ReturnToTitleAfterDelay());
    }

    private System.Collections.IEnumerator ReturnToTitleAfterDelay()
    {
        // 効果音を聞かせるため少し待つ
        yield return new WaitForSeconds(2.5f);

        // ここでボタンUIを出すなども可
        // → 例：タイトルへ戻る or リトライ
    }

    // ↓ シーン切り替えボタン用に呼び出し可能
    public void OnClickRetry()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnClickTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
