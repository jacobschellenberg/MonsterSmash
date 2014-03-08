using UnityEngine;
using System.Collections;

public class GrowShrink : MonoBehaviour {

	public Vector2 MaxGrowth;
	public Vector2 MinShrink;
	public float ScaleSpeed;
	public bool ShrinkOnly = false;

	Vector3 currentScale;
	bool isGrowing = true;

	void Start(){
		currentScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}
	
	void Update () {

		if(ShrinkOnly){
			Shrink();
		}
		else{
			GrowAndShrinkObject();
		}

	}

	void GrowAndShrinkObject(){
		if(isGrowing){
			currentScale.x += ScaleSpeed * Time.deltaTime;
			currentScale.y += ScaleSpeed * Time.deltaTime;
			if(currentScale.x > MaxGrowth.x){
				isGrowing = false;
			}
		}
		else{
			currentScale.x -= ScaleSpeed * Time.deltaTime;
			currentScale.y = currentScale.x;
			if(currentScale.x < MinShrink.x){
				isGrowing = true;
			}
		}

		transform.localScale = new Vector3(currentScale.x,currentScale.y,currentScale.z);
	}

	void Shrink(){
			currentScale.x -= ScaleSpeed * Time.deltaTime;
			currentScale.y = currentScale.x;

		transform.localScale = new Vector3(currentScale.x,currentScale.y,currentScale.z);
	}
}
