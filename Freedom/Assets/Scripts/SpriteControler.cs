﻿using UnityEngine;
using System.Collections;

public class SpriteControler : MonoBehaviour {

	public float FreedomAmount;
	private SpriteSwapper swapper;


	public void setFreedomAmount (int spriteNo)
	{
		Debug.Log ("stfreedo,amount");
		swapper.swap (spriteNo);

	}




	// Use this for initialization
	void Start () {
	
		swapper = gameObject.GetComponent<SpriteSwapper> ();

	}
}
