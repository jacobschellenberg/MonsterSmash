using UnityEngine;
using System.Collections;

public class FreezeAll : PowerUpBase {

	public float frozenTimer = 2;

	public override IEnumerator Effect(){
		foreach(var monster in monsters){
			if(monster != null){
				var currentMonster = monster.GetComponent<Monster>();
				currentMonster.movementSpeed = 0;
				currentMonster.sideMovementSpeed = 0;
				
				var scaleTween = currentMonster.gameObject.AddComponent<TweenScale>();
				scaleTween.duration = 0.5f;
				scaleTween.from = currentMonster.transform.localScale;
				scaleTween.to = currentMonster.transform.localScale * 1.1f;
				scaleTween.style = UITweener.Style.PingPong;
				scaleTween.ResetToBeginning();
				scaleTween.PlayForward();
			}
		}
		
		yield return new WaitForSeconds(frozenTimer);
		
		foreach(var monster in monsters){
			if(monster != null){
				var currentMonster = monster.GetComponent<Monster>();
				currentMonster.movementSpeed = currentMonster.originalSpeed;
				currentMonster.sideMovementSpeed = currentMonster.originalSideMovementSpeed;
			}
		}

		Destroy(this.gameObject);
	}
}
