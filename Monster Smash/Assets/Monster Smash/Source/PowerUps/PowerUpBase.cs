using UnityEngine;
using System.Collections;

public abstract class PowerUpBase : MonoBehaviour {

	public float maxPosX = 750;
	public float maxPosY = 650;

	protected GameObject[] monsters;

	void Start(){
		transform.localPosition = new Vector3(Random.Range (-maxPosX, maxPosX), Random.Range(-maxPosY, maxPosY), 0);
		monsters = GameObject.FindGameObjectsWithTag("Monster");
	}

	public abstract IEnumerator Effect();

	void OnClick(){
		this.collider.enabled = false;
		this.GetComponent<UIButton>().UpdateColor(false, true);
		StartCoroutine(Effect());
	}
}
