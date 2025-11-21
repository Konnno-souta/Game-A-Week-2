using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameStartManager : MonoBehaviour
{
    [Header("カウントダウン表示")]
    public Text countdownText;

    [Header("無効化する機能")]
    public MonoBehaviour[] systemsToDisable;

    [Header("スタート時間")]
    public int startCount = 5;

    private void Start()
    {
        // ゲーム開始時はすべて OFF
        SetAllSystems(false);

        // カウント開始
        StartCoroutine(StartCountdown());
    }

    //IEnumerator StartCountdown()
    //{
    //    int count = startCount;

    //    while (count > 0)
    //    {
    //        countdownText.text = count.ToString();
    //        yield return new WaitForSeconds(1f);
    //        count--;
    //    }

    //    // Start 表示
    //    countdownText.text = "START!";
    //    yield return new WaitForSeconds(1f);

    //    countdownText.gameObject.SetActive(false);

    //    // 全システム ON
    //    SetAllSystems(true);
    //}

    private void SetAllSystems(bool isOn)
    {
        foreach (var sys in systemsToDisable)
            sys.enabled = isOn;
    }

    IEnumerator StartCountdown()
    {
        int count = startCount;

        while (count > 0)
        {
            countdownText.text = count.ToString();
            yield return new WaitForSeconds(1f);
            count--;
        }

        countdownText.text = "START!";
        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);

        // システム ON
        SetAllSystems(true);

        // カウント終了後にタイマー開始
        FindObjectOfType<StopwatchManager>().StartTimer();
    }


}
