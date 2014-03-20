using UnityEngine;
using System.Collections.Generic;

public class MonsterObjectiveTrigger : MonoBehaviour {

	public GameViewController gameViewController;

	public void OnTriggerEnter(Collider trigger){
		gameViewController.OnHit(trigger.gameObject);
	}
}
