using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TitleButton : MonoBehaviour
{
    [Header("シーン設定")]
    [SerializeField] private string titleSceneName = "TitleScene"; // タイトルシーン名

    [Header("サウンド設定")]
    public AudioClip clickSE;       // ボタンを押した時のSE
    public float volume = 1f;       // 音量
    private AudioSource audioSource;

    private void Start()
    {
        // AudioSourceを取得または追加
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // ボタン取得＆クリックイベント登録
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnReturnToTitleClicked);
        }
    }

    void OnReturnToTitleClicked()
    {
        Debug.Log("タイトルに戻ります: " + titleSceneName);

        // 効果音を再生
        if (clickSE != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickSE, volume);
        }

        // 少し待ってからシーン切り替え
        StartCoroutine(ReturnToTitleAfterDelay(0.3f));
    }

    private IEnumerator ReturnToTitleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("TitleScene");
    }
}
