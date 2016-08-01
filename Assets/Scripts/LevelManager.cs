using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	
	public void LoadLevel(string name){
		Debug.Log ("New Level load: " + name);
		Application.LoadLevel (name);
	}

	public void LoadNextLevel(){
		Application.LoadLevel(Application.loadedLevel +1);
	}
	
	public int GetCurrentLevel(){
		return Application.loadedLevel;
	}
	
	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

}
