using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {
	
	public static int score = 0;
	
	private Text myText;
	
	
	void Start(){
		myText = GetComponent<Text>();
		//Reset ();
		myText.text = "Score: " + score.ToString();
	}
	
	public int CheckScore(){
		return score;
	}
	
	public void Score(int points){
		score += points;
		myText.text = "Score: " + score.ToString();
	}
	
	public static void Reset(){
		score = 0;
		//myText.text = "Score: " + score.ToString();
	}
	
	void OnLevelWasLoaded(int level){
		Debug.Log("ScoreKeeper: loaded level " + level);
		if(level==0){
			Reset();
		}
	}
}


