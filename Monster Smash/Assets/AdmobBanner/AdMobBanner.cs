using UnityEngine;
using System.Collections;

public class AdMobBanner : MonoBehaviour {
	
	public string publisherId = "a1503918d21668c";
    public float refreshRate = 30.0F;
	public bool dontDestroyOnLoad = true;
	
	void Awake(){
		#if UNITY_ANDROID && !UNITY_EDITOR
		if(dontDestroyOnLoad){
			DontDestroyOnLoad(gameObject);
		}
		#endif
		
		Debug.LogWarning("AdMobs are disabled within the Unity Editor.");
	}
	
	#if UNITY_ANDROID && !UNITY_EDITOR
	IEnumerator Start () {
		AndroidJavaClass plugin = new AndroidJavaClass("com.shatteredskygames.unity3dgoogleadmob.GoogleAdMob");
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
		
        while (true) {
			plugin.CallStatic("CreateAdMobBanner", activity, publisherId);
			yield return new WaitForSeconds(Mathf.Max(30.0f, refreshRate));
        }
    }
	#endif
}