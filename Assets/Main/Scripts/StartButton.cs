using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class StartButton : MonoBehaviour
{
    [Header("シーン設定")]
    public string nextSceneName = "MainScene"; // 移動先シーン名

    [Header("サウンド設定")]
    public AudioClip startSE;       // スタートボタンのSE
    public float volume = 1f;       // 音量（0〜1）
    private AudioSource audioSource;

    private void Start()
    {
        // AudioSource を探す or 自動追加
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // ボタンを取得してクリックイベント登録
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnStartButtonClicked);
        }
    }

    void OnStartButtonClicked()
    {
        // 効果音を再生
        if (startSE != null && audioSource != null)
        {
            audioSource.PlayOneShot(startSE, volume);
        }

        // 効果音が鳴り終わるまで少し待ってからシーン切り替え
        StartCoroutine(LoadSceneAfterDelay(0.3f));
    }

    private IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MainScene");
    }
}
