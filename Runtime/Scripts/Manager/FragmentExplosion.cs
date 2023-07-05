using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FragmentExplosion : MonoBehaviour
{
    public float explosionForce = 20.0f;
    public float explosionRadius = 10.0f;

    [Tooltip("If True, it will Self Destroy the object with de Explosion Force Physics")]
    public bool selfDestroy = true;
    public float delayToStop = 0.35f;

    [Tooltip("Range based on the original Explsion Radius variable, it is the range to pick the objects on the air")]
    public float rangeMultiplier = 2.0f;

    public bool release = false;
    public float timerToRelease = 3.0f;

    private Coroutine releaseCoroutine;
    public bool showDebugLogMessages = true;
    
    void Start()
    {   
        Explosion();
        if (selfDestroy)
        {
            Destroy(this.gameObject, 0.25f);
        }
        else
        {           
            Invoke("StopRigidbody", delayToStop);
        }        
    }    

    void Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearby in colliders)
        {
            Rigidbody rb = nearby.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, this.transform.position, explosionRadius);
            }
        }
    }

    private void StopRigidbody()
    {       
        if (rangeMultiplier > 0 && rangeMultiplier <= 10.0f)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius * rangeMultiplier);            

            foreach (Collider nearby in colliders)
            {
                Rigidbody rb = nearby.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    if (release)
                    {
                        rb.Sleep();
                    }
                    else
                    {
                        rb.useGravity = false;
                        rb.angularVelocity = Vector3.zero;
                        rb.angularDrag = 0.0f;
                        rb.velocity = Vector3.zero;
                    }
                }
            }

            if (release)
            {
                releaseCoroutine = StartCoroutine(ReleaseRigidbody(colliders));
            }
        }
        else
        {
            DebugManager(true, "Fragment Explosion = The Range Multiplier variable must be between 1 an 10");
        }
    }
    
    IEnumerator ReleaseRigidbody(Collider[] releaseCollider)
    {
        yield return new WaitForSecondsRealtime(timerToRelease);        

        foreach (Collider aux in releaseCollider)
        {
            try
            {
                Rigidbody rb = aux.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.WakeUp();
                    rb.AddExplosionForce(explosionForce, this.transform.position, explosionRadius);
                }
            }
            catch (Exception e)
            {
                DebugManager(false, e.Message);
            }
        }
        Destroy(this.gameObject, 1.0f);
    }

    private void DebugManager(bool isWarning, string debugText)
    {
        if (showDebugLogMessages)
        {
            if (isWarning)
            {
                Debug.LogWarning(debugText);
            }
            else
            {
                Debug.Log(debugText);
            }
        }        
    }
}
