
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class SceneNavigation : EditorWindow
{
    [MenuItem("Window/Scene Navigation")]
    static void Setup ()
    {
        SceneNavigation window   = (SceneNavigation)EditorWindow.GetWindow(typeof(SceneNavigation), false, "Scene Navigation");
        
        window.minSize      = new Vector2(10, 10);
        
        window.Show();
    }
    
    void OnGUI ()
    {
        GUIStyle background             = new GUIStyle("box");
        background.margin               = new RectOffset();
        background.normal.background    = Resources.Load("background", typeof(Texture2D)) as Texture2D;
        background.stretchWidth         = true;
        
        GUIStyle toggle                 = new GUIStyle("toggle");
        toggle.margin                   = new RectOffset(0, 0, 5, 0);
        toggle.fixedHeight              = 20;
        
        GUIStyle button                 = new GUIStyle("button");
        button.margin                   = new RectOffset(3, 0, 0, 0);
        button.stretchWidth             = true;
        button.richText                 = true;
        button.alignment                = TextAnchor.MiddleLeft;
        button.fixedHeight              = 20;
        
        GUIStyle number                 = new GUIStyle("box");
        number.margin                   = new RectOffset(0, 0, 0, 0);
        number.fixedWidth               = 20;
        number.richText                 = true;
        
        GUILayout.BeginVertical(background);
        
        GUILayout.Label("Scenes in build:", button);
        
        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            string colorable;
            
            if (SceneManager.GetActiveScene().path == EditorBuildSettings.scenes[i].path)
            {
                colorable = "<color=white>";
                GUI.color = Color.green;
            }
            else
            {
                colorable = "<color=grey>";
                GUI.color = new Color(0.3f, 0.3f, 0.3f, 1.0f);
            }
            
            GUILayout.BeginHorizontal(background);
            
            GUILayout.Toggle(EditorBuildSettings.scenes[i].enabled, "", toggle, GUILayout.Width(15));
            string[] pathPieces = EditorBuildSettings.scenes[i].path.Split('/');
            if (GUILayout.Button(colorable + pathPieces[pathPieces.Length - 1] + "</color>", button))
            {
                if (Application.isPlaying)
                {
                    SceneManager.LoadScene(i);
                }
                else
                {
                    EditorSceneManager.OpenScene(EditorBuildSettings.scenes[i].path);
                }
            }
            
            GUILayout.Label(colorable + i + "</color>", number);
            
            GUILayout.EndHorizontal();
            
            GUI.color = Color.white;
            
            GUILayout.Space(-1);
        }
        
        GUILayout.EndVertical();
    }
}
