using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;


public class BassListenerBigEnemy : MonoBehaviour
{
    private AudioManager audioManager;
    private TurnAndCharge turnAndCharge;
    private Rigidbody2D rb;
    private GunTypeA weapon;
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
    [SerializeField] private GameObject gun;
    //[SerializeField] private ParticleSystem chargerEffect;
    private bool isAbove;
    private bool wasAbove;
    //private bool canCharge = false;
    //private bool isCharging = false;
    private int beatCountRotation =0;
    

    void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();   
        turnAndCharge = GetComponent<TurnAndCharge>();
        rb = GetComponent<Rigidbody2D>();
        baseScale = transform.localScale.x;
        weapon = gun.GetComponent<GunTypeA>();
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
            RotationTrigger();
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * baseScale, Time.deltaTime * pulseReturnSpeed);
        }
        //Debug.Log("Bass: " + currentBass);
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
        if (isAbove && !wasAbove)
        {
            weapon.Fire();
            turnAndCharge.TurnTowardsPlayer();
            turnAndCharge.Charging();
        }
        lastBassIntensity = currentBass;
    }

    private void RotationTrigger()
    {
        beatCountRotation++;
        if (beatCountRotation % 4 == 0)
        {
            weapon.TurnGun();
        }
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
