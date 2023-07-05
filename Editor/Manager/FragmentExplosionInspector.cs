using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FragmentExplosion))]
public class FragmentExplosionInspector : Editor
{
    SerializedProperty explosionForce;
    SerializedProperty explosionRadius;

    SerializedProperty selfDestroy;
    SerializedProperty delayToStop;

    SerializedProperty rangeMultiplier;

    SerializedProperty release;
    SerializedProperty timerToRelease;

    SerializedProperty releaseCoroutine;
    SerializedProperty showDebugLogMessages;

    private void OnEnable()
    {
        explosionForce = serializedObject.FindProperty("explosionForce");
        explosionRadius = serializedObject.FindProperty("explosionRadius");

        selfDestroy = serializedObject.FindProperty("selfDestroy");
        delayToStop = serializedObject.FindProperty("delayToStop");

        rangeMultiplier = serializedObject.FindProperty("rangeMultiplier");

        release = serializedObject.FindProperty("release");
        timerToRelease = serializedObject.FindProperty("timerToRelease");

        releaseCoroutine = serializedObject.FindProperty("releaseCoroutine");
        showDebugLogMessages = serializedObject.FindProperty("showDebugLogMessages");
    }

    public override void OnInspectorGUI()
    {
        FragmentExplosion _fragmentExplosion = (FragmentExplosion)target;

        serializedObject.Update();

        EditorGUILayout.PropertyField(explosionForce);
        EditorGUILayout.PropertyField(explosionRadius);

        EditorGUILayout.Space(10);
        EditorGUILayout.PropertyField(selfDestroy);
        if (!_fragmentExplosion.selfDestroy)
        {
            EditorGUILayout.PropertyField(delayToStop);

            EditorGUILayout.Space(10);

            EditorGUILayout.HelpBox(
                "This variable will multiply the \"Explosion Radius\" variable in ordem to catch the objects again to stop their rigidbodies components.\n" +
                "This is useful for situations that the \"Delay to Stop\" variable is set too high, and the fragments are to far away from its origin.\n" + 
                "Be aware that this number can cause unwarted consequences, that could be even in other objetcts on the range or in your game performance.", 
                MessageType.Info, 
                true
            );
            EditorGUILayout.BeginHorizontal();            
            GUILayout.Label("Range Multiplier");
            _fragmentExplosion.rangeMultiplier = EditorGUILayout.Slider(_fragmentExplosion.rangeMultiplier, 1.0f, 10.0f);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(10);
            EditorGUILayout.PropertyField(release);

        }

        if (!_fragmentExplosion.selfDestroy && _fragmentExplosion.release)
        {
            EditorGUILayout.PropertyField(timerToRelease);
        }

        EditorGUILayout.Space(10);
        EditorGUILayout.PropertyField(showDebugLogMessages);

        serializedObject.ApplyModifiedProperties();
    }
}
