using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GUN : MonoBehaviour
{
    
    public Transform muzzle;
    public GameObject bulletPrefab;
    public float bulletSpeed = 30f;

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.activated.AddListener(Shoot);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Shoot(ActivateEventArgs arg)
    {
        // Instantiate a bullet at the muzzle position and rotation
        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);

        // Get the Rigidbody component of the bullet
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        // Check if the bullet has a Rigidbody (for physics-based bullets)
        if (bulletRigidbody != null)
        {
            // Shoot the bullet forward with specified speed
            bulletRigidbody.velocity = muzzle.forward * bulletSpeed;
        }

        // Destroy the bullet after a certain time (e.g., 3 seconds)
        Destroy(bullet, 3f);
    }

}
