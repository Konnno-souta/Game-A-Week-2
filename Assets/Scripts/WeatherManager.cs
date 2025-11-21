using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    [Header("‰Jƒp[ƒeƒBƒNƒ‹")]
    public ParticleSystem rainSystem;

    [Header("‰Š‚ÌŠÇ—ƒXƒNƒŠƒvƒg")]
    public FireManager fireManager;

    [Header("‰J‚Ìİ’è")]
    public float minRainInterval = 3f;   // Ÿ‚Ì‰J‚Ü‚Å‚ÌÅ’ZŠÔ
    public float maxRainInterval = 20f;   // Ÿ‚Ì‰J‚Ü‚Å‚ÌÅ’·ŠÔ
    public float rainDuration = 5f;       // ‰J‚ª‘±‚­ŠÔ

    [Header("‰J‚Ìƒyƒiƒ‹ƒeƒB")]
    public float rainDamagePerSecond = 10f;  // –ˆ•b -10

    private bool isRaining = false;
    private float nextRainTime = 0f;

    void Start()
    {
        // Ÿ‚Ì‰J‚ª~‚éŠÔ‚ğƒZƒbƒg
        ScheduleNextRain();
    }

    void Update()
    {
        // ‰J‚ª~‚Á‚Ä‚¢‚é
        if (isRaining)
        {
            // –ˆ•b -10
            fireManager.AddFuel(-rainDamagePerSecond * Time.deltaTime);
            return;
        }

        // ŠÔ‚É‚È‚Á‚½‚ç‰J‚ğŠJn
        if (Time.time >= nextRainTime)
        {
            StartCoroutine(StartRain());
        }
    }

    /// <summary>
    /// Ÿ‚Ì‰J‚Ì”­¶‚Ü‚Å‚ÌŠÔ‚ğŒˆ‚ß‚é
    /// </summary>
    void ScheduleNextRain()
    {
        nextRainTime = Time.time + Random.Range(minRainInterval, maxRainInterval);
    }

    /// <summary>
    /// ‰JŠJn ¨ Œp‘± ¨ ’â~‚Ü‚Å§Œä
    /// </summary>
    private System.Collections.IEnumerator StartRain()
    {
        isRaining = true;

        // ‰JƒGƒtƒFƒNƒg‚ğÄ¶
        if (rainSystem != null)
            rainSystem.Play();

        // w’è•b”‚¾‚¯‰J‚ğ~‚ç‚¹‚é
        yield return new WaitForSeconds(rainDuration);

        // ‰J‚ğ~‚ß‚é
        if (rainSystem != null)
            rainSystem.Stop();

        isRaining = false;

        // Ÿ‚Ì‰J‚ÌŠÔ‚ğ—\–ñ
        ScheduleNextRain();
    }
}

