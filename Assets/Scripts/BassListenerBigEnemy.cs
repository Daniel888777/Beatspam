using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;


public class BassListenerBigEnemy : MonoBehaviour
{
    private AudioManager audioManager;
    private Rigidbody2D rb;
    private float baseScale;
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

    void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        rb = GetComponent<Rigidbody2D>();
        baseScale = transform.localScale.x;

    }

  
    void Update()
    {
        currentBass = audioManager.GetBass();
        smoothedBass = Mathf.Lerp(smoothedBass, currentBass, Time.deltaTime * smoothingSpeed);
        
        isAbove = currentBass >= bassThreshold && currentBass > smoothedBass * beatMultiplier;
        
        if (currentBass >= bassThreshold && currentBass > smoothedBass * beatMultiplier && Time.time >= timeToNextBassEffect)
        {
            //Skip(currentBass);
            Pulse(currentBass);
            timeToNextBassEffect = Time.time + bassEffectCooldown;
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * baseScale, Time.deltaTime * pulseReturnSpeed);
        }
        Debug.Log("Bass: " + currentBass);
        wasAbove = isAbove;
    }

    private void FixedUpdate()
    {

    }

    private void LateUpdate()
    {
       
    }

    private void Skip(float bass)
    {
        Vector2 skipDirection = Random.insideUnitCircle.normalized;
        float skipIntensity = bass * 10 ; 
        rb.MovePosition(rb.position + skipDirection * skipIntensity * Time.deltaTime);
    }

    private void Pulse(float bass)
    {
       
        transform.localScale = Vector3.one * (baseScale + bass);
        if (transform.localScale.x > baseScale *2)
        {
            transform.localScale = Vector3.one * baseScale *2;
        }
        
        lastBassIntensity = currentBass;
    }



    private void OnDrawGizmos()
    {
        if (currentBass >= bassThreshold && currentBass > smoothedBass * beatMultiplier)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, currentBass);
        }
    }
}
