using System;
using System.Collections;
using UnityEngine;

public class FragmentManager : MonoBehaviour
{
    public float checkTimer = 10.0f;
    public float destroyTimer = 5.0f;

    [HideInInspector]
    public GameObject[] temporaryObjects = null;

    [Space]
    public bool destroy = true;
    public bool destroyFragmentsRigidbody = false;
    public bool removeShadow = false;

    [Space]
    public bool showDebugLogMessages = true;

    private Coroutine managerCoroutine;

    private void Start()
    {
        if (checkTimer > 0f && destroyTimer >= 0f)
        {
            managerCoroutine = StartCoroutine(ManualChecker());
        }
        else if (checkTimer == 0)
        {
            DebugManager(false, "Fragment Manager = Only Manual mode");
        }
        else
        {
            DebugManager(true, "Fragment Manager Error = The variables must be bigger than 0");
        }
    }
    public void MenagePosFragments()
    {
        try
        {
            temporaryObjects = GameObject.FindGameObjectsWithTag("newFragment");
        }
        catch(Exception e)
        {
            DebugManager(true, e.Message);
            return;
        }

        foreach (GameObject aux in temporaryObjects)
        {
            if (aux != null)
            {
                if (aux.name.Contains("_x"))
                {
                    InvertProperty();
                    ProcessManagement(aux);
                    InvertProperty();
                }
                else if (aux.name.Contains("_i"))
                {
                    aux.tag = "Untagged";
                    DebugManager(false, "Fragment Manager = The object " + aux.name + " was ignored");
                }
                else
                {
                    ProcessManagement(aux);
                }                
            }            
        }
        temporaryObjects = null;
    }

    private void InvertProperty()
    {
        destroy = !destroy;
        destroyFragmentsRigidbody = !destroyFragmentsRigidbody;
        removeShadow = !removeShadow;
    }

    private void ProcessManagement(GameObject aux)
    {
        if (destroy)
        {
            Destroy(aux.gameObject, destroyTimer);
        }
        else
        {
            aux.tag = "Untagged";
            aux.isStatic = true;
            if (destroyFragmentsRigidbody)
            {
                Transform[] tempChildrens = aux.GetComponentsInChildren<Transform>();
                foreach (Transform auxC in tempChildrens)
                {
                    if (removeShadow)
                    {
                        auxC.GetComponentInChildren<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                        auxC.GetComponentInChildren<Renderer>().receiveShadows = false;
                    }

                    Destroy(auxC.GetComponent<Rigidbody>());
                }
            }
        }
    }

    public void ForceStop()
    {
        StopCoroutine(managerCoroutine);
    }

    public void ManualCheckByInput()
    {
        MenagePosFragments();
    }

    public void DebugManager(bool isWarning, string debugText)
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

    // ==========

    IEnumerator ManualChecker()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(checkTimer);            
            MenagePosFragments();
        }        
    }
}