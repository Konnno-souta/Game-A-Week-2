using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [Header("通常UI")]
    public Text playerScoreText;
    public Text cpuScoreText;
    public Text matchInfoText;

    [Header("試合結果UI")]
    public GameObject scorePanel;
    public Text resultText;
    public Text finalScoreText;
    public Button retryButton;
    public Button titleButton;

    [Header("サウンド")]
    public AudioSource bgmSource;      // ← 追加：メインBGM
    public AudioClip winSE;            // ← 勝ちSE
    public AudioClip loseSE;           // ← 負けSE
    private AudioSource seSource;      // ← SE専用

    private int playerScore = 0;
    private int cpuScore = 0;
    public int winScore = 5;

    private bool matchEnded = false;

    void Start()
    {
        UpdateUI();
        ShowMessage("Start Ari Hockey!");

        if (scorePanel != null)
            scorePanel.SetActive(false);

        // SE再生用AudioSourceを作成
        seSource = gameObject.AddComponent<AudioSource>();
    }

    public void AddPlayerScore()
    {
        playerScore++;
        UpdateUI();
        if (playerScore >= winScore) EndMatch(true);
        else ShowMessage("CPU Scores!");
    }

    public void AddCPUScore()
    {
        cpuScore++;
        UpdateUI();
        if (cpuScore >= winScore) EndMatch(false);
        else ShowMessage("Player Scores!");
    }

    public void UpdateUI()
    {
        playerScoreText.text = $"CPU : {playerScore}";
        cpuScoreText.text = $"PLAYER : {cpuScore}";
    }

    public void ShowMessage(string message)
    {
        if (matchInfoText != null) matchInfoText.text = message;
    }
    public bool IsGameOver()
    {
        // プレイヤーとCPUのスコアを管理している変数名に合わせて修正
        return playerScore >= 5 || cpuScore >= 5;
    }

    private void EndMatch(bool playerWon)
    {
        if (matchEnded) return;
        matchEnded = true;

        // ここでBGMを停止！
        if (bgmSource != null && bgmSource.isPlaying)
            bgmSource.Stop();

        // 勝敗SEを再生
        if (playerWon && winSE != null)
            seSource.PlayOneShot(winSE);
        else if (!playerWon && loseSE != null)
            seSource.PlayOneShot(loseSE);

        ShowMessage(playerWon ? "CPU Wins!" : "Player Wins！");
        StartCoroutine(ShowScorePanel(playerWon));
    }

    private IEnumerator ShowScorePanel(bool playerWon)
    {
        yield return new WaitForSeconds(1f);
        scorePanel.SetActive(true);

        resultText.text = playerWon ? "YOU LOSE..." : "YOU WIN!!";
        finalScoreText.text = $"FINAL SCORE\nCPU : {playerScore}   PLAYER : {cpuScore}";

        // スコアパネル拡大アニメ
        scorePanel.transform.localScale = Vector3.zero;
        float duration = 0.6f;
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float scale = Mathf.SmoothStep(0, 1, time / duration);
            scorePanel.transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }
        scorePanel.transform.localScale = Vector3.one;

        retryButton.onClick.AddListener(RestartGame);
        titleButton.onClick.AddListener(ReturnToTitle);
    }

    public void RestartGame()
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
