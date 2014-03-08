using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerPrefsUtilities
{
	[MenuItem ("UnityExtensionsAndTools/Player Prefs/Delete All")]
	static void DeleteAllPlayerPrefs (){
		PlayerPrefs.DeleteAll();
		Debug.Log("All PlayerPrefs deleted.");
	}
}
