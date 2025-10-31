using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("UI管理スクリプト")]
    public UIManager uiManager;

    [Header("スコア設定")]
    public int winScore = 5; // 勝利スコア

    [Header("パック設定")]
    public GameObject puckPrefab;
    public Transform puckSpawnPoint;

    [Header("マレット設定")]
    public Transform playerMallet;
    public Transform cpuMallet;
    public Transform playerStartPos;
    public Transform cpuStartPos;

    [Header("サウンド")]
    public AudioSource bgmAudio; // BGM
    public AudioSource goalSE;   // ゴール音
    public AudioSource winSE;    // 勝利音
    public AudioSource loseSE;   // 敗北音

    private GameObject currentPuck;
    private bool gameEnded = false;

    void Start()
    {
        SpawnPuck();
        uiManager?.ShowMessage("Start Air Hockey!");
        ResumeBGM(); // BGM自動開始
    }

    /// <summary>
    /// パックを中央に再出現
    /// </summary>
    public void SpawnPuck()
    {
        if (currentPuck != null)
            Destroy(currentPuck);

        currentPuck = Instantiate(puckPrefab, puckSpawnPoint.position, Quaternion.identity);
    }

    /// <summary>
    /// ゴールに入ったとき呼び出す（GoalTrigger から）
    /// </summary>
    public void AddScore(string scorerTag)
    {
        if (gameEnded) return;

        // ゴール音
        if (goalSE != null) goalSE.Play();

        if (scorerTag == "PlayerGoal")
        {
            uiManager.AddCPUScore();  // CPU得点
            uiManager.ShowMessage("CPU Scores!");
        }
        else if (scorerTag == "CPUGoal")
        {
            uiManager.AddPlayerScore(); // プレイヤー得点
            uiManager.ShowMessage("Player Scores!");
        }

        // 試合終了チェック
        if (!IsMatchEnded())
        {
            Invoke(nameof(ResetAfterGoal), 2f);
        }
        else
        {
            EndMatch(scorerTag);
        }
    }

    /// <summary>
    /// ゴール後の位置リセット処理
    /// </summary>
    private void ResetAfterGoal()
    {
        
        SpawnPuck();
    }

    /// <summary>
    /// プレイヤー・CPUを初期位置に戻す
    /// </summary>
    private void ResetPositions()
    {
        ResetMallet(playerMallet, playerStartPos);
        ResetMallet(cpuMallet, cpuStartPos);
    }

    private void ResetMallet(Transform mallet, Transform startPos)
    {
        if (mallet == null || startPos == null) return;

        mallet.position = startPos.position;

        Rigidbody rb = mallet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
    //private void ResetPositions()
    //{
    //    if (player != null) player.ResetPosition();
    //    if (enemy != null) enemy.ResetPosition();
    //}
    /// <summary>
    /// 試合終了チェック
    /// </summary>
    /// 
    private bool IsMatchEnded()
    {
        // ここではUIManagerに最終判定を任せる設計
        // UIManagerで勝利条件(例: playerScore >= winScore)をチェックしてtrueを返すのもOK
        return uiManager != null && uiManager.IsGameOver();
    }

    /// <summary>
    /// 試合終了時処理（勝利・敗北・BGM停止）
    /// </summary>
    private void EndMatch(string scorerTag)
    {
        gameEnded = true;

        // BGM停止
        if (bgmAudio != null) bgmAudio.Stop();

        // 勝敗判定
        bool playerWin = (scorerTag == "CPUGoal");

        // SE再生
        if (playerWin && winSE != null) winSE.Play();
        else if (!playerWin && loseSE != null) loseSE.Play();

        // メッセージ表示
        uiManager.ShowMessage(playerWin ? "You Win!" : "You Lose...");
    }

    /// <summary>
    /// リトライボタンなどで再スタート時にBGM再生
    /// </summary>
    public void ResumeBGM()
    {
        if (bgmAudio != null && !bgmAudio.isPlaying)
            bgmAudio.Play();
    }
}
