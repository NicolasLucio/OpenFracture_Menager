using UnityEditor;

[CustomEditor(typeof(FragmentManager))]
public class FragmentManagerInspector : Editor
{
    SerializedProperty checkTimer;
    SerializedProperty destroyTimer;
    SerializedProperty temporaryObjects;
    SerializedProperty destroy;
    SerializedProperty destroyFragmentsRigidbody;
    SerializedProperty removeShadow;
    SerializedProperty showDebugLogMessages;
    SerializedProperty managerCoroutine;

    private void OnEnable()
    {
        checkTimer = serializedObject.FindProperty("checkTimer");
        destroyTimer = serializedObject.FindProperty("destroyTimer");
        temporaryObjects = serializedObject.FindProperty("temporaryObjects");
        destroy = serializedObject.FindProperty("destroy");
        destroyFragmentsRigidbody = serializedObject.FindProperty("destroyFragmentsRigidbody");
        removeShadow = serializedObject.FindProperty("removeShadow");
        showDebugLogMessages = serializedObject.FindProperty("showDebugLogMessages");
        managerCoroutine = serializedObject.FindProperty("managerCoroutine");
    }
    public override void OnInspectorGUI()
    {
        FragmentManager _fragmentManager = (FragmentManager)target;

        serializedObject.Update();

        EditorGUILayout.PropertyField(checkTimer);
        EditorGUILayout.PropertyField(destroyTimer);

        EditorGUILayout.PropertyField(destroy);
        if (!_fragmentManager.destroy)
        {
            EditorGUILayout.PropertyField(destroyFragmentsRigidbody);
            EditorGUILayout.PropertyField(removeShadow);
        }

        EditorGUILayout.PropertyField(showDebugLogMessages);        

        serializedObject.ApplyModifiedProperties();
    }
}
