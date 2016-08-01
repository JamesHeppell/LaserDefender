using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LifeKeeper : MonoBehaviour {

	private static float lives = 1f;
	
	private Text myText;
	
	void Start(){
		myText = GetComponent<Text>();
		//LifeReset ();
		myText.text = "Lives: " + lives.ToString();
	}
	
	public float CheckLife(){
		return lives;
	}
	
	public void Lives(int life){
		lives += life;
		myText.text = "Lives: " + lives.ToString();
	}
	
	public static void LifeReset(){
		Debug.Log("REsetting lives");
		lives = 1;
		//myText.text = "Lives: " + lives.ToString();
	}
	
	void OnLevelWasLoaded(int level){
		Debug.Log("LifeKeeper: loaded level " + level);
		if(level==0){
			LifeReset();
			Debug.Log(CheckLife ());
		}
	}

}
