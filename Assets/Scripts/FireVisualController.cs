using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireVisualController : MonoBehaviour
{
    [Header("Light2D (URP用)")]
    public Light2D fireLight;

    [Header("Particle")]
    public ParticleSystem fireParticle;

    [Header("強さカーブ(0から1のFireValueで制御)")]
    public AnimationCurve intensityCurve = AnimationCurve.Linear(0, 0.2f, 1, 1.2f);
    public AnimationCurve sizeCurve = AnimationCurve.Linear(0, 0.5f, 1, 1.5f);
    public AnimationCurve emissionCurve = AnimationCurve.Linear(0, 10f, 1, 50f);

    private ParticleSystem.MainModule mainModule;
    private ParticleSystem.EmissionModule emModule;

    void Start()
    {
        if (fireParticle != null)
        {
            mainModule = fireParticle.main;
            emModule = fireParticle.emission;
        }
    }

    /// <summary>
    /// 火の強さを元に見た目の強弱をつける
    /// </summary>
    public void UpdateFireVisual(float fireValue)
    {
        // Light2D の強さ
        if (fireLight != null)
        {
            fireLight.intensity = intensityCurve.Evaluate(fireValue);
        }

        // Particle のサイズ＆放出量
        if (fireParticle != null)
        {
            mainModule.startSize = sizeCurve.Evaluate(fireValue);
            emModule.rateOverTime = emissionCurve.Evaluate(fireValue);
        }
    }
}
