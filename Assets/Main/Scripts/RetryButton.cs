using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    [SerializeField] private string mainSceneName = "MainScene"; // メインゲームシーン名
    [SerializeField] private AudioClip clickSE; // ボタン押下SE

    private AudioSource audioSource;

    private void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnRetryClicked);
        }

        // AudioSource設定
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void OnRetryClicked()
    {
        // SE再生
        if (clickSE != null)
        {
            audioSource.PlayOneShot(clickSE);
        }

        // BGM再開用にAudioManagerを使う
        StartCoroutine(LoadMainSceneAfterDelay(0.3f));
    }

    private System.Collections.IEnumerator LoadMainSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // シーン遷移
        SceneManager.LoadScene("MainScene");
    }
}
