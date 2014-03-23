using UnityEngine;
using System.Collections;

public class MenuViewController : MonoBehaviour {

	public void OnPlayButtonClicked(){
		Application.LoadLevelAsync("Game");
	}
}
