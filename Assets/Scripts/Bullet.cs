using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 70.0f;

    public float explosionRadius = 0.0f;

    public GameObject impactEffect;

    public int damageAmount = 50;

    public void seek(Transform target)
    {
        this.target = target;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(direction.magnitude <= distanceThisFrame)
        {
            hitTarget();
            return;
        }

        transform.Translate (direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void hitTarget()
    {
        GameObject effectInstance = (GameObject) Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance,5.0f );
        if(explosionRadius > 0.0f)
        {
            explode();

        }else
        {
            damage(target);
            
        }

        
        Destroy (gameObject);
    }

    void explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy") 
            {
                damage(collider.transform);
            }
        }
    }

    void damage (Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if(e != null)
        {
               e.TakeDamage(damageAmount);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

}
