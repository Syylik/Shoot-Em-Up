using UnityEditor;
using UnityEngine;

public class GameDebugWindow : EditorWindow
{
    [MenuItem("Tools/Game Debug")]
    public static void Open()
    {
        GetWindow<GameDebugWindow>("Game Debug");
    }

    private bool _showRegistry = true;
    private Vector2 _scroll;

    private void OnGUI()
    {
        GUILayout.Label("Debug Info", EditorStyles.boldLabel);
        _showRegistry = EditorGUILayout.Foldout(_showRegistry, "Enemies Registry", true);

        if (_showRegistry)
        {
            _scroll = GUILayout.BeginScrollView(_scroll);

            foreach (var enemy in Registry<Enemy>.Items)
            {
                if (enemy == null)
                {
                    GUILayout.Label("NULL (destroyed)");
                    continue;
                }

                GUILayout.BeginHorizontal();

                if (!enemy.gameObject.activeInHierarchy) GUI.color = Color.yellow;

                GUILayout.Label(enemy.name);
                GUILayout.Label(enemy.GetType().Name);

                if (GUILayout.Button("Ping", GUILayout.Width(50))) EditorGUIUtility.PingObject(enemy.gameObject);

                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();
        }
    }

    private void OnEnable() => EditorApplication.update += Repaint;

    private void OnDisable() => EditorApplication.update -= Repaint;
}