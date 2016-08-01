using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public float health = 2f;
	public GameObject Weapon;
	public float shotsPerSecond = 0.5f;
	public float projectileSpeed=5;
	public int scoreValue = 150;
	
	public AudioClip fireSound;
	public AudioClip deathSound;
	
	private ScoreKeeper scoreKeeper;

	void OnTriggerEnter2D(Collider2D collider){
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if (missile){
			health -= missile.GetDamage();
			missile.Hit();
			Debug.Log("I have " + health + " left");
			if (health <= 0){
				Die ();
			}
		}
	}
	
	void Die(){
		AudioSource.PlayClipAtPoint(deathSound, transform.position);
		Destroy(gameObject);
		scoreKeeper.Score(scoreValue);
	}
	
	void Start(){
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}
	
	void Update(){
		float probability = Time.deltaTime * shotsPerSecond;
		if (Random.value < probability){
			EnemyFire();
		}
		
	}
	
	void EnemyFire(){
		GameObject Elaser = Instantiate(Weapon, transform.position, Quaternion.identity) as GameObject;
		Elaser.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, -projectileSpeed);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}
}
