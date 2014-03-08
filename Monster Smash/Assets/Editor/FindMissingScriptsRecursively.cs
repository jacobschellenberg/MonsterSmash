using UnityEngine;
using UnityEditor;
public class FindMissingScriptsRecursively : EditorWindow 
{
	static int gameObjectCount = 0;
	static int componentsCount = 0;
	static int missingCount = 0;

	[MenuItem("UnityExtensionsAndTools/Misc/Find Missing Scripts In Selected")]
	private static void FindInSelected(){
		GameObject[] selectedGameObjects = Selection.gameObjects;
		gameObjectCount = 0;
		componentsCount = 0;
		missingCount = 0;

		foreach (GameObject gameObject in selectedGameObjects)
			FindInGO(gameObject);
		Debug.Log(string.Format("Searched {0} GameObjects, {1} components, found {2} missing", gameObjectCount, componentsCount, missingCount));
	}

	private static void FindInGO(GameObject gameObject){
		gameObjectCount++;
		Component[] components = gameObject.GetComponents<Component>();
		for (int i = 0; i < components.Length; i++){
			componentsCount++;
			if (components[i] == null){
				missingCount++;
				string gameObjectName = gameObject.name;
				Transform gameObjectTransform = gameObject.transform;

				while (gameObjectTransform.parent != null){
					gameObjectName = gameObjectTransform.parent.name + "/" + gameObjectName;
					gameObjectTransform = gameObjectTransform.parent;
				}
				Debug.LogWarning(gameObjectName + " has an empty script attached in position: " + i, gameObject);
			}
		}
		// Recursive search
		foreach (Transform child in gameObject.transform)
			FindInGO(child.gameObject);
	}
}