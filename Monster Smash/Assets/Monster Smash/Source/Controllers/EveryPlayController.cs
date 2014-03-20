using UnityEngine;
using System.Collections;

public class EveryPlayController : MonoBehaviour {

	public GameController gameController;

	public bool IsRecordingSupported{
		get{
			return Everyplay.SharedInstance.IsRecordingSupported();
		}
		private set{}
	}

	void Start(){
		if(Everyplay.SharedInstance != null) {
			Everyplay.SharedInstance.RecordingStarted += RecordingStarted;
			Everyplay.SharedInstance.RecordingStopped += RecordingStopped;
			Everyplay.SharedInstance.WasClosed += RecordingWasClosed;
		}
	}

	public void StartRecording(){
		if(Everyplay.SharedInstance.IsRecordingSupported())
			Everyplay.SharedInstance.StartRecording();
	}
	
	public void StopRecording(){
		Everyplay.SharedInstance.StopRecording();
	}

	void RecordingStarted(){}

	void RecordingStopped() {
		Everyplay.SharedInstance.PlayLastRecording();
	}

	void RecordingWasClosed(){
		gameController.LoadEndScene();
	}
}
