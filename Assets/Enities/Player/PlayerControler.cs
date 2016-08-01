using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour {
	
	public static float scoreCaps = 100f;
	public GameObject weapon;
	public float speed = 5.0f;
	public float padding = 1.0f;
	public float laserSpeed;
	public float fireingRate=0.2f;
	
	private int score;
	
	public AudioClip fireSound;
	
	private LifeKeeper lifeKeeper;
	private ScoreKeeper scoreKeeper;
	
	private float minX;
	private float maxX;
	private float minY;
	private float maxY;
	

	
	void Start(){
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		minX = leftmost.x + padding;
		maxX = rightmost.x - padding;
		
		minY = leftmost.y + padding/2f;
		maxY = minY + 2f;
		
		lifeKeeper = GameObject.Find("Lives").GetComponent<LifeKeeper>();
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
		score = scoreKeeper.CheckScore();
		
	}

	// Update is called once per frame
	void Update () {
		score = scoreKeeper.CheckScore();
		GainLifeHighScore();
		
		Movement();
		if (Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating("Fire",0.000001f,fireingRate);
		}
		if (Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke("Fire");
		}
	}
	
	void GainLifeHighScore(){
		if (score >= scoreCaps){
			lifeKeeper.Lives(1);
			scoreCaps +=150;
		}
	}
	void Fire(){
		if (lifeKeeper.CheckLife()>=20){
			BasicFire(0);
			BasicFire(0.5f);
			BasicFire(-0.5f);
			AudioSource.PlayClipAtPoint(fireSound, transform.position);
		}else if (lifeKeeper.CheckLife()>=10) {
			BasicFire(0.5f);
			BasicFire(-0.5f);
			AudioSource.PlayClipAtPoint(fireSound, transform.position);
		}else { 
			BasicFire(0);
			AudioSource.PlayClipAtPoint(fireSound, transform.position);
		}
	}
	
	void BasicFire(float posFireX){
		GameObject laser = Instantiate(weapon, transform.position + new Vector3(posFireX,0,0), Quaternion.identity) as GameObject;
		laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0,laserSpeed,0);
	}
	
	void Movement(){
		if (Input.GetKey(KeyCode.RightArrow)){
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else if (Input.GetKey(KeyCode.LeftArrow)){
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.UpArrow)){
			transform.position += Vector3.up * speed * Time.deltaTime;
		}else if (Input.GetKey(KeyCode.DownArrow)){
			transform.position += Vector3.down * speed * Time.deltaTime;
		}
		//restrict player to game space
		float newX = Mathf.Clamp(transform.position.x, minX, maxX);
		float newY = Mathf.Clamp(transform.position.y, minY, maxY);
		//this.transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
		this.transform.position = new Vector3 (newX, newY, transform.position.z);
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if (missile){
			missile.Hit();
			lifeKeeper.Lives(-1);
			Debug.Log("I've been hit!");
			Debug.Log("I've got " + lifeKeeper.CheckLife() + " health left!");
			if (lifeKeeper.CheckLife() <= 0){
				Die ();
			}
		}
	}
	
	void Die(){
		//lifeKepper.LifeReset();
		ReSetExpCap();
		LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		man.LoadLevel("Lose Screen");
		Destroy(gameObject);
	}
	
	public static void ReSetExpCap(){
		scoreCaps = 100f;
	}
}
