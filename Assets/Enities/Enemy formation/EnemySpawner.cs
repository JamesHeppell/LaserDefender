using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	
	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speedEnemy = 5f;
	public float spawnDelay = 0.5f;
	public float numWaves = 2f;
	
	private bool movingRight = false;
	private float xmax;
	private float xmin;
	
	
	// Use this for initialization
	void Start () {
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0,0, distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1,0, distanceToCamera));
		xmax = rightBoundary.x;
		xmin = leftBoundary.x;
		SpawnUntilFull();
		
	}
	
	void PopulateFormation(){
		foreach( Transform child in transform){
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}
	
	void SpawnUntilFull(){
		Transform freePosition = NextFreePosition();
		if (freePosition){
			GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if(NextFreePosition()){
			Invoke("SpawnUntilFull",spawnDelay);
		}
	}
	
	public void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}
	
	// Update is called once per frame
	void Update () {
		if (movingRight){
			transform.position += Vector3.right * speedEnemy * Time.deltaTime;
		}else {
			transform.position += Vector3.left * speedEnemy * Time.deltaTime;
		}

		float rightEdgeofFormation = transform.position.x + (0.5f * width);
		float leftEdgeofFormation = transform.position.x - (0.5f * width);
		if (leftEdgeofFormation < xmin) {
			movingRight = true;
		} else if (rightEdgeofFormation > xmax){
			movingRight = false;
		}
		
		if(AllMembersDead()){
			Debug.Log("Empty formation");
			if (numWaves<1){
				LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
				man.LoadNextLevel();
			} else{
				numWaves -=1;
				SpawnUntilFull();
			}
		}
	}
	
	Transform NextFreePosition(){
		foreach(Transform childPositionGameObject in transform){
			if (childPositionGameObject.childCount == 0){
				return childPositionGameObject;
			}
		}
		return null;
	}
	
	bool AllMembersDead(){
		foreach(Transform childPositionGameObject in transform){
			if (childPositionGameObject.childCount >0){
				return false;
			}
		}
		return true;
	}
}
