using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossHP : MonoBehaviour {

	private RectTransform HPlength;
	
	// Use this for initialization
	void Start () {
		HPlength = GetComponent<RectTransform>();
		//HPlength.position = new Vector3(400f,525f,0f);
		HPlength.localScale = new Vector3(1f,1f,1f);
	}
	
	public void DecreaseBossHP(float lifeleft, float totallife){
		HPlength.localScale = new Vector3(lifeleft/totallife, 1f, 1f);
	}
	
	
}
