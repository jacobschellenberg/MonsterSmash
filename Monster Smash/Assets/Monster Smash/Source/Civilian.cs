using UnityEngine;
using System.Collections.Generic;

public class Civilian : MonoBehaviour {
	
	float speed = 0.3f;
	public List<Texture2D> agentTextures = new List<Texture2D>();

	// Update is called once per frame
	
	void Start(){
		ChooseTexture();
		Destroy(gameObject, 15);
	}
	
	void Update () {
		transform.Translate(Vector3.down * speed * Time.deltaTime);
	}
	
	public void ChooseTexture(){
		GetComponentInChildren<UITexture>().mainTexture = agentTextures[Random.Range(0,agentTextures.Count)];
	}
}
