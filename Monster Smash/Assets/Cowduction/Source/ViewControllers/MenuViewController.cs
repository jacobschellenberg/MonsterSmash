using UnityEngine;
using System.Collections;

public class MenuViewController : MonoBehaviour {

	public UISprite cow;
	public UISprite absorbBeam;
	public UISprite spaceShip;

	void Start(){
		cow.gameObject.SetActive(true);
		cow.GetComponent<TweenPosition>().enabled = false;
		cow.GetComponent<TweenRotation>().enabled = false;
		cow.GetComponent<TweenAlpha>().enabled = false;
		absorbBeam.gameObject.SetActive(false);
		absorbBeam.alpha = 0;
		spaceShip.gameObject.SetActive(false);
	}

	public void OnPlayButtonClicked(){
		StartCoroutine(PlaySequence());
	}

	IEnumerator PlaySequence(){
		absorbBeam.gameObject.SetActive(true);
		spaceShip.gameObject.SetActive(true);

		absorbBeam.GetComponent<TweenAlpha>().ResetToBeginning();
		absorbBeam.GetComponent<TweenAlpha>().PlayForward();
		
		spaceShip.GetComponent<TweenPosition>().ResetToBeginning();
		spaceShip.GetComponent<TweenPosition>().PlayForward();

		cow.GetComponent<TweenPosition>().enabled = true;
		cow.GetComponent<TweenRotation>().enabled = true;
		cow.GetComponent<TweenAlpha>().enabled = true;
		
		cow.GetComponent<TweenPosition>().ResetToBeginning();
		cow.GetComponent<TweenRotation>().ResetToBeginning();

		cow.GetComponent<TweenPosition>().PlayForward();
		cow.GetComponent<TweenPosition>().PlayForward();

		yield return new WaitForSeconds(3);
		Application.LoadLevelAsync("Game");
	}
	
}
