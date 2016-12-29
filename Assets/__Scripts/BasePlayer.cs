using UnityEngine;
using System.Collections;

/*
 *This is the base class from which the players are inherited.
 *Contains the common logic of the player
 * 
  * 
 Author : PraveenMax
 * 
 */
 */
public class BasePlayer : MonoBehaviour {

	//Bound checks
	protected bool isTopBoundReached=false;
	protected bool isBottomBoundReached=false;

	//Bullet props
	private Vector3 angleVectorLeft=new Vector3(0,0,90);
	private Vector3 angleVectorRight=new Vector3(0,0,270);
	private Vector3 bulletStartPositionLR=new Vector3(0.8f,0,0.05f);//bullet start position from player.LR
	private Vector3 bulletStartPositionRL=new Vector3(-0.9f,0,-0.05f);//bullet start position from player.RL
	private float normalBulletTimeInterval=0.05f;
	private float bulletStartTime;

	//Player states
	public bool isOnLeftSide;//initially the player is on left side
	private float playerXPosition=8.15f;
	public GameObject bullet;
	public int score=0;

	// Use this for initialization
	void Start () {
		bulletStartTime=Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected void fireBullet()
	{
		if(!GetComponent<AudioSource>().isPlaying)
			GetComponent<AudioSource>().Play();
		
		if(Time.time-bulletStartTime>normalBulletTimeInterval)
		{
			GameObject bulletClone;
			
			if(isOnLeftSide)
			{
				bulletClone = (GameObject)Instantiate(bullet,this.transform.position+bulletStartPositionLR,this.transform.rotation);
				bulletClone.transform.Rotate(angleVectorLeft);
				bulletClone.GetComponent<Bullet>().bulletMovement(true,true,this.gameObject);
			}
			else
			{
				bulletClone = (GameObject)Instantiate(bullet,this.transform.position+bulletStartPositionRL,this.transform.rotation);
				bulletClone.transform.Rotate(angleVectorRight);
				bulletClone.GetComponent<Bullet>().bulletMovement(true,false,this.gameObject);
			}
			bulletStartTime=Time.time;
		}
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag=="topboundcube")
		{
			isTopBoundReached=true;
		}
		if(col.gameObject.tag=="bottomboundcube")
		{
			isBottomBoundReached=true;
		}
	}

	protected void swapPosition()
	{
		Vector3 positionX = this.transform.position;
		Vector3 scaleY = this.transform.localScale;
		if(isOnLeftSide)
		{
			//move to right side
			positionX.x = playerXPosition;
			scaleY.y=-scaleY.y;
			Debug.Log ("Now on right side");
			isOnLeftSide=false;
		}
		else
		{
			//move to left side
			positionX.x = -playerXPosition;
			scaleY.y=-scaleY.y;
			Debug.Log ("Now on left side");
			isOnLeftSide=true;
		}
		
		this.transform.localPosition=positionX;
		this.transform.localScale=scaleY;
	}

	protected void movePlayerUp()
	{
		isBottomBoundReached=false;//so that the player can move down when down pressed
		Vector3 positionZ = this.transform.position;
		positionZ.z += 0.1f;
		this.transform.position = positionZ;
		//Debug.Log("up pressed");
	}

	protected void movePlayerDown()
	{
		isTopBoundReached=false;//so that the player can move up when up pressed
		Vector3 positionZ = this.transform.position;
		positionZ.z -= 0.1f;
		this.transform.position = positionZ;
	}
}
