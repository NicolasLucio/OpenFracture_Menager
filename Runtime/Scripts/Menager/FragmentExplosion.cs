using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FragmentExplosion : MonoBehaviour
{
    public float explosionForce = 20.0f;
    public float explosionRadios = 10.0f;
    
    void Start()
    {   
        Explosion();        
        Destroy(this.gameObject, 0.25f);
    }    

    void Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadios);

        foreach (Collider nearby in colliders)
        {
            Rigidbody rb = nearby.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, this.transform.position, explosionRadios);
            }
        }
    }
}
