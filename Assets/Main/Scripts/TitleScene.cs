using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene"); // ← プレイ画面のシーン名
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

