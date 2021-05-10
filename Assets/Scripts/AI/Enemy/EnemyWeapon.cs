using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyWeapon : MonoBehaviour
{
    [Header("Shoot")]
    [SerializeField]
    private float damage = 10f;
    [SerializeField]
    private float fireRate = 15f;
    [SerializeField]
    private float range = 100f;
    
    [Header("Ammo")]
    [SerializeField]
    private int maxAmmo = 10;
    private int currentAmmo;
    [SerializeField]
    private float reloadTime = 2f;
    private bool isReloading = false;
    
    // [Header("Sound")]
    // [SerializeField]
    // private UnityEvent onShoot;
    // [SerializeField]
    // private UnityEvent onReload;
    
    [SerializeField]
    private ParticleSystem muzzleFlash;

    private float nextTimeToFire = 0f;

    [SerializeField]
    private Animator animator;

    [SerializeField] 
    private AudioManager audioManager;
    
    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void OnEnable() 
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }
    
    IEnumerator Reload()
    {
        isReloading = true;

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);

        animator.SetBool("Reloading", false);

        audioManager.PlaySound("PistolReload");

        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        isReloading = false;

    }

    public void Shoot(Vector3 direction, Vector3 position)
    {
        if (isReloading)
        {
            return;
        }

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;

            MakeShoot(direction, position);
        }
    }

    void MakeShoot(Vector3 direction, Vector3 position)
    {
        muzzleFlash.Play();
        audioManager.PlaySound("PistolShoot");

        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(position, direction, out hit, range))
        {
            HealthController player = hit.transform.GetComponent<HealthController>();

            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }
}
