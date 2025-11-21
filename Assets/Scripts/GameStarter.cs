using System.Collections;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public FireManager fireManager;
    public StopwatchManager stopWatch;
    public KeyPressChallenge keyInput;

    void Start()
    {
        fireManager.enabled = false;
        stopWatch.enabled = false;
        keyInput.enabled = false;

        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(5f);   // カウントダウン完了

        fireManager.enabled = true;
        stopWatch.enabled = true;
        keyInput.enabled = true;
    }
}
