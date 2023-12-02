using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform firePoint; // Point from where the bullets are spawned
    [SerializeField] private GameObject bulletPrefab; // Prefab of the bullet

    [SerializeField] private float bulletForce = 20f; // Force applied to the bullet
    [SerializeField] private float bulletLifetime = 3f; // Time in seconds before the bullet disappears
    [SerializeField] public float damage = 5f;

    [SerializeField] private float shootingCooldown = 1f; // Cooldown between shots

    private bool canShoot = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Create a new bullet instance at the fire point's position and rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Get the Rigidbody component of the bullet
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        // Apply force to the bullet to make it move
        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);

        // Destroy the bullet after the specified lifetime
        Destroy(bullet, bulletLifetime);

        // Start the cooldown
        StartCoroutine(ShootingCooldown());
    }

    System.Collections.IEnumerator ShootingCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootingCooldown);
        canShoot = true;
    }

}
