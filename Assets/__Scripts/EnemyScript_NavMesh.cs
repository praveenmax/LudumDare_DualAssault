using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/**
 * 
 Author : PraveenMax
 * 
 */
public class EnemyScript_NavMesh : MonoBehaviour {




	//Enemy movement vars
	//for normal enemies
	private bool isMovingStraight,isTopLeftTent,isBottomLeftTent,isTopRightTent,isBottomRightTent;//old
	private bool isMovingLeft;
	private NavMeshAgent navMeshAgentNormalEnemy;

	//for boss enemies
	Vector3 angleVectorLeft=new Vector3(0,0,0);
	Vector3 angleVectorRight=new Vector3(90,90,0);

	//Territory captured level
	GameObject territoryCaptured;

	//Enemy types vars
	public bool isNormalEnemy,isBossEnemy;

	//Enemy health
	private int normalEnemyHealth=0;

	//EnemyCampTop
	private GameObject gameLogicInstance;

	public GameObject player1,player2;


	// Use this for initialization
	void Start () {

		navMeshAgentNormalEnemy=this.GetComponent<NavMeshAgent>();

		if(isMovingLeft)
		{
			//transform.Rotate(Vector3.up,360,Space.World);
			//Debug.Log("Rotated!");
		}
	}



	public void Update()
	{
		transform.rotation = Quaternion.Euler(new Vector3(90,0,0));


		if(isMovingLeft)
		{
			navMeshAgentNormalEnemy.destination=player1.transform.position;
		}
		///transform.Rotate(Vector3.up,360,Space.World);
	}

	void OnCollisionEnter (Collision col)
	{
		//bullet hit
		if(col.gameObject.tag=="playerbullet")
		{
			Destroy (col.gameObject);
			normalEnemyHealth--;
		}

		if(normalEnemyHealth<0)
		{
			this.gameObject.transform.Rotate(angleVectorLeft);
			//updates the score based on the playername in the bullet
			gameLogicInstance.GetComponent<GameLogic>().updateScore(col.gameObject.GetComponent<Bullet>().player.name);

			//Debug.Log("Enemy dead");
			Destroy(this.gameObject);
		}

		//territory captured
		if(col.gameObject.tag=="enemyDestroyer")
		{
			gameLogicInstance.GetComponent<GameLogic>().updateTerritoryCapturedCount();
			Destroy(this.gameObject);
		}
	}

	/*
	public void createAtTopLeft(bool isMovingStraight,GameObject gameLogicInstance)
	{
		this.isNormalEnemy=true;
		this.isTopLeftTent=true;
		this.isMovingStraight=isMovingStraight;
		this.gameLogicInstance=gameLogicInstance;
	}

	public void createAtTopRight(bool isMovingStraight,GameObject gameLogicInstance)
	{
		this.isNormalEnemy=true;
		this.isTopRightTent=true;
		this.isMovingStraight=isMovingStraight;
		this.gameLogicInstance=gameLogicInstance;
	}

	public void createAtBottomLeft(bool isMovingStraight,GameObject gameLogicInstance)
	{
		this.isNormalEnemy=true;
		this.isBottomLeftTent=true;
		this.isMovingStraight=isMovingStraight;
		this.gameLogicInstance=gameLogicInstance;
	}

	public void createAtBottomRight(bool isMovingStraight,GameObject gameLogicInstance)
	{
		this.isNormalEnemy=true;
		this.isBottomRightTent=true;
		this.isMovingStraight=isMovingStraight;
		this.gameLogicInstance=gameLogicInstance;
	}

	public void createAtCenter(bool isMovingLeft, GameObject gameLogicInstance)
	{
		this.isBossEnemy=true;
		this.isMovingLeft=isMovingLeft;
		this.gameLogicInstance=gameLogicInstance;
	}
	*/

	public void moveBossAI(bool isPlayer1)
	{
		if(isPlayer1)
		{

		}
	}

	public void createEnemyLeft(bool isMovingLeft, GameObject gameLogicInstance)
	{
		this.isMovingLeft=isMovingLeft;
		this.gameLogicInstance=gameLogicInstance;
	}
}
