using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultSceneManager : MonoBehaviour
{
    public Text resultTimeText;
    public Text comboText;

    void Start()
    {
        // •\Ž¦
        resultTimeText.text = $"Time: {FormatTime(ResultData.survivedTime)}";
        comboText.text = $"Combo: {ResultData.maxCombo}";
    }

    public void OnRetry()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnTitle()
    {
        SceneManager.LoadScene("Title");
    }

    string FormatTime(float time)
    {
        int min = (int)(time / 60);
        int sec = (int)(time % 60);
        return $"{min:00}:{sec:00}";
    }
}
