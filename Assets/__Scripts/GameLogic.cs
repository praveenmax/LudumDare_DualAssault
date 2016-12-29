using UnityEngine;
using System.Collections;

/**
 * 
 *This class handles all the game logic such as HUD drawing,game condition check, enemies generation 
 * 

 * 
 Author : PraveenMax
 * 
 */
public class GameLogic : MonoBehaviour {

	//game props
	public bool isSinglePlayer=false;

	//player props
	public GameObject playerOne, playerTwo;

	//enemy props
	public GameObject topSpawnPoint,bottomSpawnPoint;
	private float startTimeNormalEnemy, startTimeBossEnemy;
	public GameObject normalEnemy,bossEnemy_tank; 
	private float bossEnemy_SpawnTime=10f;
	private float normalEnemy_SpawnTime=2.2f;

	//GUI rects
	private Rect territortCapturedRect;
	private Rect scoreRectPlayerOne=new Rect(10,1,100,22);
	private Rect scoreRectPlayerTwo;

	//HUD props
	private int territoryCaptured=0;

	// Use this for initialization
	void Start () {
		cleanUpDataFromDisk();
		startTimeNormalEnemy=Time.time;
		startTimeBossEnemy=Time.time;

		playerOne.GetComponent<Player>().score=0;
		playerTwo.GetComponent<Player>().score=0;
	}
	
	// Update is called once per frame
	void Update () {
	
		generateEnemies();
		checkGameEndingCondition();
	}

	void FixedUpdate()
	{
		scoreRectPlayerTwo=new Rect(Screen.width-120,1,100,22);
		territortCapturedRect=new Rect(Screen.width/2-100,1,200,22);

	}

	public void OnGUI()
	{
		GUI.Box(territortCapturedRect, "Territory captured : "+territoryCaptured);

		if(playerOne!=null)
		{
			displayScore(scoreRectPlayerOne,playerOne.GetComponent<Player>().score);
		}

		if(playerTwo!=null  && isSinglePlayer==false)
		{
			displayScore(scoreRectPlayerTwo,playerTwo.GetComponent<Player>().score);
		}
	}

	private void displayScore(Rect scoreRect,int score)
	{
		GUI.Box(scoreRect, "Score : "+score);
	}

	private void generateEnemies()
	{
		if(Time.time - startTimeNormalEnemy > normalEnemy_SpawnTime)
		{
			
			//create enemy
			//Debug.Log ("creating top enemy");
			
			Vector3 enemyPositionTopLeft = new Vector3(topSpawnPoint.transform.position.x, topSpawnPoint.transform.position.y,topSpawnPoint.transform.position.z );
		//	Vector3 enemyPositionBottomLeft = new Vector3(this.transform.position.x, this.transform.position.y,Random.Range(-2f,-3f) );
		//	Vector3 enemyPositionTopRight = new Vector3(this.transform.position.x, this.transform.position.y,3.6f );
		//	Vector3 enemyPositionBottomRight = new Vector3(this.transform.position.x, this.transform.position.y,-2.4f );

			//topleft enemies
				GameObject enemyCloneTL1 = (GameObject)Instantiate(normalEnemy,enemyPositionTopLeft,topSpawnPoint.transform.rotation);
				enemyCloneTL1.GetComponent<EnemyScript_NavMesh>().createEnemyLeft(true,this.gameObject);
				//enemyCloneTL1.transform.Rotate(new Vector3(90,90,90));

			//	GameObject enemyCloneTL2 = (GameObject)Instantiate(normalEnemy,enemyPositionTopLeft,this.transform.rotation);
			//	enemyCloneTL2.GetComponent<EnemyScriptTweened>().createAtTopLeft(false,this.gameObject);

			/*
			//topright enemies
				GameObject enemyCloneTR1 = (GameObject)Instantiate(normalEnemy,enemyPositionTopRight,this.transform.rotation);
				enemyCloneTR1.GetComponent<EnemyScriptTweened>().createAtTopRight(true,this.gameObject);

				GameObject enemyCloneTR2 = (GameObject)Instantiate(normalEnemy,enemyPositionTopRight,this.transform.rotation);
				enemyCloneTR2.GetComponent<EnemyScriptTweened>().createAtTopRight(false,this.gameObject);

			//bottom left enemies
				GameObject enemyCloneBL1= (GameObject)Instantiate(normalEnemy,enemyPositionBottomLeft,this.transform.rotation);
				enemyCloneBL1.GetComponent<EnemyScriptTweened>().createAtBottomLeft(true,this.gameObject);

				GameObject enemyCloneBL2 = (GameObject)Instantiate(normalEnemy,enemyPositionBottomLeft,this.transform.rotation);
				enemyCloneBL2.GetComponent<EnemyScriptTweened>().createAtBottomLeft(false,this.gameObject);

			//bottom right enemies
				GameObject enemyCloneBR1 = (GameObject)Instantiate(normalEnemy,enemyPositionBottomRight,this.transform.rotation);
				enemyCloneBR1.GetComponent<EnemyScriptTweened>().createAtBottomRight(true,this.gameObject);

				GameObject enemyCloneBR2 = (GameObject)Instantiate(normalEnemy,enemyPositionBottomRight,this.transform.rotation);
				enemyCloneBR2.GetComponent<EnemyScriptTweened>().createAtBottomRight(false,this.gameObject);
				*/
			startTimeNormalEnemy=Time.time;
		}

		if(Time.time - startTimeBossEnemy > bossEnemy_SpawnTime)
		{
		//	Debug.Log ("Created Boss Tank");
			//create enemy
			//Debug.Log ("creating top enemy");
			
			Vector3 bossEnemyPositionLeft = new Vector3(this.transform.position.x, this.transform.position.y,0.7f );

			//topleft enemies
		//	GameObject enemyCloneTL1 = (GameObject)Instantiate(bossEnemy_tank,bossEnemyPositionLeft,this.transform.rotation);
		//	enemyCloneTL1.GetComponent<EnemyScriptTweened>().createAtCenter(true,this.gameObject);

			startTimeBossEnemy=Time.time;
		}
	}

	public void updateScore(string playerName)
	{
		if(playerName=="Player1")
			playerOne.GetComponent<Player>().score++;

		if(playerName=="Player2")
		{
			playerTwo.GetComponent<Player>().score++;
			//Debug.Log("Updating playertwo score");
		}
	}

	public void updateTerritoryCapturedCount()
	{
		territoryCaptured++;
	}

	public void checkGameEndingCondition()
	{
		if(territoryCaptured>30)
		{
			dumpDataToDisk();
			Debug.Log("Game over");
			Application.LoadLevel("gameover");
		}
	}

	private void dumpDataToDisk()
	{
		PlayerPrefs.SetString("isSinglePlayer",""+isSinglePlayer);
		PlayerPrefs.SetString("Player1Score",""+playerOne.GetComponent<Player>().score);

		if(!isSinglePlayer)
			PlayerPrefs.SetString("Player2Score",""+playerTwo.GetComponent<Player>().score);

		Debug.Log ("***** Score data written to disk");
	}

	private void cleanUpDataFromDisk()
	{
		if(PlayerPrefs.HasKey("isSinglePlayer"))
			PlayerPrefs.DeleteKey("isSinglePlayer");

		if(PlayerPrefs.HasKey("Player1Score"))
			PlayerPrefs.DeleteKey("Player1Score");

		if(PlayerPrefs.HasKey("Player2Score"))
			PlayerPrefs.DeleteKey("Player2Score");

		Debug.Log("Cleaned old game data");
	}
}
	//publiv void updatePlayerOne



