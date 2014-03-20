using UnityEngine;
using System.Collections;

public class EveryPlayController : MonoBehaviour {

	public GameController gameController;

	public bool IsRecordingSupported{ 
		get{
			return Everyplay.SharedInstance.IsRecordingSupported();
		} 
	}

	void Start(){
		if(Everyplay.SharedInstance != null) {
			Everyplay.SharedInstance.RecordingStarted += RecordingStarted;
			Everyplay.SharedInstance.RecordingStopped += RecordingStopped;
			Everyplay.SharedInstance.WasClosed += RecordingWasClosed;
		}

		StartCoroutine(DelayedRecording());
	}

	IEnumerator DelayedRecording(){
		yield return new WaitForSeconds(2);
		StartRecording();
	}

	public void StartRecording(){
		if(Everyplay.SharedInstance.IsRecordingSupported())
			Everyplay.SharedInstance.StartRecording();
	}
	
	public void StopRecording(){
		if(Everyplay.SharedInstance.IsRecordingSupported())
			Everyplay.SharedInstance.StopRecording();
	}

	void RecordingStarted(){}

	void RecordingStopped() {
		if(Everyplay.SharedInstance.IsRecordingSupported())
			Everyplay.SharedInstance.PlayLastRecording();
	}

	void RecordingWasClosed(){
		gameController.LoadEndScene();
	}
}
