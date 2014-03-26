﻿using UnityEngine;
using System.Collections;

public class EveryPlayController : MonoBehaviour {

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

	// Have to delay start the recording to allow time for everything to load.
	// 2 seconds hard coded because it should be enough and doesn't need adjusting by designer
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
		GameController.Instance.LoadEndScene();
	}
}
