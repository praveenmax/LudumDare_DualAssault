using UnityEngine;
using System.Collections;
/**
 * 
 Author : PraveenMax
 * 
 */
public class Bullet : MonoBehaviour {

	// Use this for initialization
	public GameObject player;//from which player the bullet was triggered
	public bool isNormalBullet=false;
	public bool isMovingFromLeft;
	private Vector3 position;

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
		if(isNormalBullet)
		{
			if(isMovingFromLeft)
			{
				position=this.gameObject.transform.position;
				position.x++;
				this.gameObject.transform.position=position;
			}
			else
			{
				position=this.gameObject.transform.position;
				position.x--;
				this.gameObject.transform.position=position;
			}
		}
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag=="bulletdestroyer")
		{
			//Debug.Log("Bullet destroyed");
			Destroy(this.gameObject);
		}

	}

	public void bulletMovement(bool isNormalBullet, bool isMovingFromLeft,GameObject player)
	{
		this.isNormalBullet=isNormalBullet;
		this.isMovingFromLeft=isMovingFromLeft;
		this.player=player;
	}

}


















