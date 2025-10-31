using UnityEngine;

public class SEManager : MonoBehaviour
{
    public static SEManager Instance;

    [Header("効果音")]
    public AudioClip playerGoalSE;
    public AudioClip cpuGoalSE;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    /// <summary>
    /// プレイヤー得点音
    /// </summary>
    public void PlayPlayerGoalSE()
    {
        if (playerGoalSE != null)
            audioSource.PlayOneShot(playerGoalSE);
    }

    /// <summary>
    /// CPU得点音
    /// </summary>
    public void PlayCPUGoalSE()
    {
        if (cpuGoalSE != null)
            audioSource.PlayOneShot(cpuGoalSE);
    }
}
