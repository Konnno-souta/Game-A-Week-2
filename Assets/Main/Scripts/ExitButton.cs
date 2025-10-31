using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    private void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnExitButtonClicked);
        }
    }

    void OnExitButtonClicked()
    {
        Debug.Log("アプリ終了"); // 確認用（エディタでは終了しないため）

        // アプリ終了
        Application.Quit();

#if UNITY_EDITOR
        // エディタ上で停止するための処理（Playモード終了）
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
