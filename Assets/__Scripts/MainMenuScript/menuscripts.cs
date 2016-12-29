using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/**
 * 
 Author : PraveenMax
 * 
 */
public class menuscripts : MonoBehaviour {
	
	private float startTime;
	private Rect scoreRect=new Rect(Screen.width/2-100,1,200,22);
	public Text playerOneScore,playerTwoScore;
	private string s_playerOneScore,s_playerTwoScore;

	// Use this for initialization
	void Start () {

	}


	void Awake() {
		Debug.Log ("Current scene : "+Application.loadedLevelName);
		if(Application.loadedLevelName=="gameover")
		{
			displayScore();
			Debug.Log ("You are in gameover screen");
			startTime=Time.time;
		}
	}

	private void displayScore()
	{
		s_playerOneScore=PlayerPrefs.GetString("Player1Score");
		playerOneScore.text="Player 1 \nScore : "+s_playerOneScore;


		if(PlayerPrefs.GetString("isSinglePlayer")=="False")
		{
			s_playerTwoScore=PlayerPrefs.GetString("Player2Score");
			playerTwoScore.text="Player 2 \nScore : "+s_playerTwoScore;
		}
		else
			playerTwoScore.text="";

	}

	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown()
	{
		Debug.Log ("Currently Loaded scene:"+Application.loadedLevelName);

		
		if(gameObject.name=="instruction")
		{
			Application.LoadLevel("instruction");
		}

		if(gameObject.name=="coopbutton")
		{
			Application.LoadLevel("gamecoop");
		}

		if (gameObject.name == "singleplayerbutton") {
			Application.LoadLevel("game");
			Debug.Log ("play clicked");
		}

		if(gameObject.name=="backbutton")
		{
			Debug.Log ("back clicked");
			Application.LoadLevel("main");
		}

		if(gameObject.name=="credits")
		{
			Debug.Log ("credits clicked");
			Application.LoadLevel("credits");
		}

		if(gameObject.name=="aboutbutton")
			Application.LoadLevel("about");

		if(gameObject.name=="exitbutton")
			Application.Quit();//check this	
	}
}
