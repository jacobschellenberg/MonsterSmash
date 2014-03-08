using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hotkeys {

	[MenuItem("UnityExtensionsAndTools/Hotkeys/Enable or Disable Selected Objects %#d")]
	static void EnableOrDisableObject(){
		foreach(GameObject gameObject in Selection.gameObjects)
			gameObject.SetActive(gameObject.activeSelf ? false : true);
	}

	[MenuItem("UnityExtensionsAndTools/Hotkeys/Group Selected Objects %#g")]
  static void GroupSelected() {
    GameObject newGroup = new GameObject("New Group");
    newGroup.transform.parent = Selection.activeGameObject.transform.parent;
    foreach (GameObject child in Selection.gameObjects) 
      child.transform.parent = newGroup.transform;
  }

	[MenuItem("UnityExtensionsAndTools/Hotkeys/Create Empty Child GameObject &#c")]
  static void CreateEmptyChildGameObject() {
    GameObject emptyChild = new GameObject("Empty Child");
    emptyChild.transform.parent = Selection.activeGameObject.transform;
  }

	[MenuItem("UnityExtensionsAndTools/Hotkeys/Create Empty Sibling GameObject &#s")]
  static void CreateEmptySiblingGameObject(){
    GameObject emptySibling = new GameObject("Empty Sibling");
    emptySibling.transform.parent = Selection.activeGameObject.transform.parent;
  }
}
