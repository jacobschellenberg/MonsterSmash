using UnityEngine;
using System.Collections;

public class TutorialViewController : MonoBehaviour {

	public void OnContinueButtonClicked(){
		Application.LoadLevelAsync("Game");
	}
}
