using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    private Transform target;
    private Enemy targetEnemy;

    [Header("General")]
    public float range = 15.0f;

    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    public float fireRate = 1.0f;
    private float fireCountdown = 0.0f;

    [Header("Use Laser")]
    public bool useLaser = false;

    public int damageOverTime = 30;
    public float slowAmount = 0.5f;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public float turnSpeed = 10.0f;
    
    
    public Transform firePoint;

   

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("updateTarget", 0.0f, 0.5f);
    }

    void updateTarget ()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject nearestEnemy = null;

        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distancetoEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distancetoEnemy < shortestDistance)
            {
                shortestDistance = distancetoEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ( target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }

            }
            return;
        }

        LockOnTarget();
        if (useLaser)
        {
            Laser();
        }else
        {
            if (fireCountdown <= 0.0f)
            {
                Shoot();
                fireCountdown = 1.0f / fireRate;
            }

            fireCountdown -= Time.deltaTime;

        }

    }

    void LockOnTarget()
    {
        Vector3 direction = target.position - transform.position;
        // Quaternions are used to represent rotations
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        // Unity uses Euler angles to do rotations
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        // Only rotates axis Y
        partToRotate.rotation = Quaternion.Euler(0.0f, rotation.y, 0.0f);
    }

    void Laser()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowAmount);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactLight.enabled = true;
            impactEffect.Play();
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        // Vector pointing from the target to the firepoint
        Vector3 dir = firePoint.position - target.position;

        
        // Rotates the particle effect to the direction of the laser
        // and sets the position of the particle effect to the middle of the laser
        impactEffect.transform.SetPositionAndRotation(target.position + dir.normalized, Quaternion.LookRotation(dir));

        
    }


    void Shoot()
    {
        GameObject bulletGO = (GameObject) Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.seek(target);
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
