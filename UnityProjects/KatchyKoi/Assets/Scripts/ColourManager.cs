using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourManager : MonoBehaviour {
	public int numColours = 5;
	public float colourChangeTime = 20;
	public static int currentColour;
	float lastTime = 0;

	void Awake(){
		currentColour = FirstColour();
	}

	void Update () {
		if(GameManager.gamePlay){
			currentColour = SelectColour();
		}
		else{
			lastTime = Time.timeSinceLevelLoad;
		}
	}

	int SelectColour(){
		if(lastTime + colourChangeTime < Time.timeSinceLevelLoad){
			lastTime = Time.timeSinceLevelLoad;
			return Random.Range(0, numColours);
		}
		else{
			return currentColour;
		}
	}

	int FirstColour(){
		return Random.Range(0, numColours);
	}
}
