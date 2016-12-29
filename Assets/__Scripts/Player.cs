using UnityEngine;
using System;
using System.Collections;
using System.Globalization;
/**
 * 
 Author : PraveenMax
 * 
 */
public class Player : BasePlayer {


	void Start () {

		score=0;
	}

	// Update is called once per frame
	void Update () {

		processKeyEvents();

	}
	
	void processKeyEvents()
	{
		if(gameObject.name=="Player1")
		{
			if (Input.GetKey (KeyCode.Q) && !isTopBoundReached)
			{
				movePlayerUp();
			}
			else
			if (Input.GetKey (KeyCode.A) && !isBottomBoundReached) 
			{
				movePlayerDown();
			}

			if(Input.GetKey (KeyCode.Z))
			{
				fireBullet();
			}

			if(Input.GetKeyUp(KeyCode.X))
			{
				swapPosition();
			}
		}//player 1
		else
		if(gameObject.name=="Player2")
		{
		
			if (Input.GetKey (KeyCode.UpArrow) && !isTopBoundReached)
			{
				movePlayerUp();
			}
			else
			if (Input.GetKey (KeyCode.DownArrow) && !isBottomBoundReached) 
			{
				movePlayerDown();
			}

			if(Input.GetKey (KeyCode.P))
			{
				fireBullet();
			}
			
			if(Input.GetKeyUp(KeyCode.O))
			{
				swapPosition();
			}

		}//player 2

	}






}