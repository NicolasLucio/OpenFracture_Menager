using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Runtime.Serialization;

public class FragmentManagerEditor : EditorWindow
{    
    GameObject tempObj = null;
    string debugText = "";

    [MenuItem("Window/Ksaa/Fragment Manager")]
    public static void ShowWindow()
    {        
        GetWindow<FragmentManagerEditor>("Fragment Manager");         
    }

    void OnInspectorUpdate()
    {
        Repaint();
    }

    void OnGUI()
    {
        GUILayout.Space(10);
        if(GUILayout.Button("Add Fragment Manager"))
        {
            if (FindObjectOfType<FragmentManager>())
            {
                debugText = "Fragment Manager already exist in this Scene";
            }
        }

        GUILayout.Space(10);
        GUILayout.Label("Explosion Fragment", EditorStyles.boldLabel);
        GUILayout.Label("Select the Game Object", EditorStyles.label);

        if (GUILayout.Button("Add Explosion Objects"))
        {
            ApplyExplosionObject();
        }

        GUILayout.Space(5);
        GUILayout.Label(debugText, EditorStyles.miniLabel);
        
    }

    void ApplyExplosionObject()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            if (obj != null)
            {
                if (!obj.name.Contains("_e"))
                {
                    obj.name += "_e";
                }
                
                if (obj.GetComponent<Fracture>() == null)
                {
                    obj.AddComponent<Fracture>();
                }

                if (obj.GetComponent<Rigidbody>())
                {
                    tempObj = new GameObject();
                    tempObj.name = obj.name + "_Explosion";
                    tempObj.transform.SetParent(obj.transform.parent, false);
                    tempObj.transform.position = obj.transform.position;

                    // Copy Rigidbody properties
                    var thisRigidbody = obj.GetComponent<Rigidbody>();
                    var childRigidbody = tempObj.AddComponent<Rigidbody>();
                    childRigidbody.velocity = thisRigidbody.velocity;
                    childRigidbody.angularVelocity = thisRigidbody.angularVelocity;
                    childRigidbody.drag = thisRigidbody.drag;
                    childRigidbody.angularDrag = thisRigidbody.angularDrag;

                    // Remove gravity of the Child
                    childRigidbody.useGravity = false;

                    // Add the Explosion Script
                    tempObj.AddComponent<FragmentExplosion>();

                    // Turn it off
                    tempObj.SetActive(false);
                }
                else
                {
                    debugText = "The selected object does not have a Rigidbody";
                }
            }
            else
            {
                debugText = "You need to select a object first";
            }
        }
    }
}
