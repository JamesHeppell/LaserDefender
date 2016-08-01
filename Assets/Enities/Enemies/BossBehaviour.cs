using UnityEngine;
using System.Collections;

public class BossBehaviour : MonoBehaviour {
	public float health = 2f;
	public float startHP = 2f;
	public GameObject Weapon;
	public GameObject SpecialWeapon;
	public float shotsPerSecond = 0.5f;
	public float projectileSpeed=5;
	public int scoreValue = 1000;
	public float SpecialWeaponFreq = 0.1f;
	public AudioClip fireSound;
	public AudioClip deathSound;
	
	private ScoreKeeper scoreKeeper;
	private BossHP bossHP;
	
	void OnTriggerEnter2D(Collider2D collider){
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if (missile){
			health -= missile.GetDamage();
			missile.Hit();
			bossHP.DecreaseBossHP(health,startHP);
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
		bossHP = GameObject.Find("BossHP").GetComponent<BossHP>();
	}
	
	void Update(){
		float probability = Time.deltaTime * shotsPerSecond;
		float probability2 = Time.deltaTime * SpecialWeaponFreq;
		if (Random.value < probability){
			EnemyFire();
		}
		if (Random.value < probability2){
			SpecialWeaponFire();
		}
		
	}
	
	void EnemyFire(){
		GameObject Elaser = Instantiate(Weapon, transform.position, Quaternion.identity) as GameObject;
		Elaser.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, -projectileSpeed);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}
	
	void SpecialWeaponFire(){
		//Get player coordinates
		Transform playerPos = GameObject.Find("Player").GetComponent<Transform>();
		Debug.Log ("playerPos");
		//same as above
		GameObject SpecW1 = Instantiate(SpecialWeapon, transform.position, Quaternion.identity) as GameObject;
		//SpecW1.rigidbody2D.velocity = new Vector2 (playerPos.position.x, playerPos.position.y);
		SpecW1.GetComponent<Rigidbody2D>().velocity = -new Vector2 (transform.position.x - playerPos.position.x, transform.position.y - playerPos.position.y);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}
	
	
}
