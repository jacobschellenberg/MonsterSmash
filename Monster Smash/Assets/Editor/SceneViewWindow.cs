using System.IO;
using UnityEditor;
using UnityEngine;

public class SceneViewWindow : EditorWindow{

	private Vector2 scrollPos;

	[MenuItem("Window/Scene View")]
	static void Init(){
		var window = (SceneViewWindow)GetWindow(typeof(SceneViewWindow), false, "Scene View");
		window.position = new Rect(window.position.xMin + 100f, window.position.yMin + 100f, 200f, 400f);
	}

	void OnGUI(){
		EditorGUILayout.BeginVertical();
		this.scrollPos = EditorGUILayout.BeginScrollView(this.scrollPos, false, false);
		
		GUILayout.Label("Active Scenes In Build", EditorStyles.boldLabel);
		for (var i = 0; i < EditorBuildSettings.scenes.Length; i++){
			var scene = EditorBuildSettings.scenes[i];
			if (scene.enabled){
				var sceneName = Path.GetFileNameWithoutExtension(scene.path);
				var pressed = GUILayout.Button(string.Format("{0}: {1}", i, sceneName), new GUIStyle(GUI.skin.GetStyle("Button")) {alignment = TextAnchor.MiddleLeft});
				if (pressed){
					if (EditorApplication.SaveCurrentSceneIfUserWantsTo())
						EditorApplication.OpenScene(scene.path);
				}
			}
		}
		
		EditorGUILayout.EndScrollView();
		EditorGUILayout.EndVertical();
	}
}