using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class GunController : MonoBehaviour
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

    [Header("Sound")]
    [SerializeField]
    private UnityEvent onShoot;
    [SerializeField]
    private UnityEvent onReload;

    [Header("Other")]
    [SerializeField]
    private Text text;
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private ParticleSystem muzzleFlash;

    private float nextTimeToFire = 0f;

    [SerializeField]
    private Animator animator;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void OnEnable() 
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    void Update() 
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
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f/fireRate;
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo != maxAmmo)
        {
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);

        animator.SetBool("Reloading", false);

        if (onReload != null)
        {
            onReload.Invoke();
        }

        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        setAmmoText();
        isReloading = false;

    }

    void Shoot()
    {
        muzzleFlash.Play();

        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);

            //Target target = hit.transform.GetComponent<Target>();
            EnemyAIController aiController = hit.transform.GetComponent<EnemyAIController>();

            if (aiController != null)
            {
                aiController.TakeDamage(damage);
            }
        }

        if (onShoot != null)
        {
            onShoot.Invoke();
        }

        setAmmoText();
    }

    public void setAmmoText()
    {
        text.text = "Ammo " + currentAmmo;
    }

}
