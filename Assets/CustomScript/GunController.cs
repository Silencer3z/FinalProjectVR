using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunController : MonoBehaviour
{
    public Transform muzzle;
    public GameObject bulletPrefab;
    public float bulletSpeed = 30f;
    public int currentAmmo;
    public int maxAmmo = 0;

    public enum FiringMode
    {
        Single,
        Burst,
        Automatic
    }

    public FiringMode currentFiringMode = FiringMode.Single;
    public int bulletsPerBurst = 3;
    public float fireRate = 0.1f;
    private float nextFire = 0.0f;

    // Cooldown time before allowing another firing mode switch
    public float modeSwitchCooldown = 1.0f;
    private float nextModeSwitch = 0.0f;

    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.activated.AddListener(Shoot);

        currentAmmo = maxAmmo;
    }

    void Update()
    {
        // Check for firing mode switch with cooldown
        if (Time.time > nextModeSwitch)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SwitchFiringMode(FiringMode.Single);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SwitchFiringMode(FiringMode.Burst);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SwitchFiringMode(FiringMode.Automatic);
            }
   
        }
       
    }

    void Shoot(ActivateEventArgs arg)
    {
        if (Time.time > nextFire && currentAmmo > 0)
        {
            nextFire = Time.time + fireRate;

            if (currentFiringMode == FiringMode.Single)
            {
                ShootBullet();
            }
            else if (currentFiringMode == FiringMode.Burst)
            {
                StartCoroutine(ShootBurst());
            }
            else if (currentFiringMode == FiringMode.Automatic)
            {
                InvokeRepeating("ShootBullet", 0.0f, fireRate);
            }
           
        }
       
    }

    void SwitchFiringMode(FiringMode newMode)
    {
        currentFiringMode = newMode;
        nextModeSwitch = Time.time + modeSwitchCooldown;

        // Stop automatic shooting when switching modes
        CancelInvoke("ShootBullet");
    }

    void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = muzzle.forward * bulletSpeed;
            currentAmmo--;
        }

        Destroy(bullet, 3f);
    }

    IEnumerator ShootBurst()
    {
        for (int i = 0; i < bulletsPerBurst; i++)
        {
            ShootBullet();
            yield return new WaitForSeconds(fireRate);
        }
    }
}

