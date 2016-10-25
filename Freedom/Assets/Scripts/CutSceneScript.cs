using UnityEngine;
using System.Collections.Generic;
using System.Collections;



public class CutSceneScript : MonoBehaviour {

	public int SequencePos;
	public List<SpriteRenderer> ImageList;

	IEnumerator Fade()
	{
		for (float f = 1f; f >= 0f; f -= 0.01f) {
			Color alfha = ImageList [SequencePos].color;
			alfha.a = f;
			ImageList [SequencePos].color = alfha;

		}
		yield return null;
	}


	void Start()
	{
		SequencePos = 0;
	}

	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {
			StartCoroutine ("Fade");
			SequencePos++;
		}
		//display images
		//if i==7
		//do zoom thing

		//if key press then i++




	
	}
}
