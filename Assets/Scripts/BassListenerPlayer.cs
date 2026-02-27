using UnityEngine;

public class BassListenerPlayer : MonoBehaviour
{
    private AudioManager audioManager;
    private PlayerStatManager statManager;
    private float timeToNextBassEffect = 0f;
    private float lastBassIntensity = 0f;
    private float currentBass;
    [SerializeField] private float bassThreshold = 0.05f;
    [SerializeField] private float bassEffectCooldown = 0.02f;
    private float smoothedBass;
    [SerializeField] private float smoothingSpeed = 5f;
    [SerializeField] private float beatMultiplier = 0.3f;
    [SerializeField] private float pulseReturnSpeed = 5f;
    private bool isAbove;
    private bool wasAbove;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        statManager = GetComponent<PlayerStatManager>();

    }

    // Update is called once per frame
    void Update()
    {
        currentBass = audioManager.GetBass();
        smoothedBass = Mathf.Lerp(smoothedBass, currentBass, Time.deltaTime * smoothingSpeed);

        isAbove = currentBass >= bassThreshold && currentBass > smoothedBass * beatMultiplier;

        if (currentBass >= bassThreshold && currentBass > smoothedBass * beatMultiplier && Time.time >= timeToNextBassEffect)
        {
            statManager.ScoreIncrease();
            timeToNextBassEffect = Time.time + bassEffectCooldown;
        }
        
        wasAbove = isAbove;
    }
}
