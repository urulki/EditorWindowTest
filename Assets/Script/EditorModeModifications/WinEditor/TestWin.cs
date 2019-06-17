using UnityEditor;
using UnityEngine;

namespace Script.EditorModeModifications.WinEditor
{
    public class TestWin : EditorWindow
    {
        string myString = "Hello World";
        bool groupEnabled;
        bool myBool = true;
        float myFloat = 1.23f;
        

        // Add menu named "My Window" to the Window menu
        [MenuItem("Window/Test")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            TestWin window = (TestWin)EditorWindow.GetWindow(typeof(TestWin),false,"Test Window" );
            window.name = "Test Window";
            window.Show();
        }

        void OnGUI()
        {
            GUILayout.Label("Base Settings", EditorStyles.boldLabel);
            myString = EditorGUILayout.TextField("Text Field", myString);
            EditorGUILayout.Knob(Vector2.one, 0.5f,0,1,"M",Color.red, Color.blue, true);
            groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
            myBool = EditorGUILayout.Toggle("Toggle", myBool);
            myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
            EditorGUILayout.EndToggleGroup();
        }
    }
}
