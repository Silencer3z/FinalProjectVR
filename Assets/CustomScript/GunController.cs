using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GunController : MonoBehaviour
{
    public Transform muzzle;
    public GameObject bulletPrefab;
    public float bulletSpeed = 30f;
    public int currentAmmo;
    public int maxAmmo = 0;

    

    public TextMeshProUGUI ammoText;


    public AudioSource gunAudioSource;

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
        gunAudioSource = GetComponent<AudioSource>();
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.activated.AddListener(Shoot);

        currentAmmo = maxAmmo;

      
    }

    void Update()
    {
        ammoText.text = currentAmmo.ToString("");

        // Check for firing mode switch with cooldown
       
       
       
    }

    void Shoot(ActivateEventArgs arg)
    {
        PlayGunSound();
        if (currentAmmo <= 0)
        {
            Reload();
            return;
        }
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
    void Shoot(DeactivateEventArgs args)
    {
        if (currentFiringMode == FiringMode.Automatic)
        {
            CancelInvoke("ShootBullet"); // Stop continuous shooting when trigger is released
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

    void Reload()
    {
        // Check if the current ammo is less than the maximum ammo and if there is available ammo to reload
        int ammoNeeded = maxAmmo;
        if (ammoNeeded > 0)
        {
            int ammoToReload = Mathf.Min(ammoNeeded); // Calculate how much ammo to reload
            currentAmmo += ammoToReload; // Add ammo to the gun
            // Subtract ammo from available pool
        }
    }

    void PlayGunSound()
    {
        if (gunAudioSource != null && gunAudioSource.clip != null)
        {
            gunAudioSource.PlayOneShot(gunAudioSource.clip); // Play the audio clip
        }
    }
}

