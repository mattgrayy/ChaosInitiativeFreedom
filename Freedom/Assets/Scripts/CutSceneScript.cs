using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;



public class CutSceneScript : MonoBehaviour {

	public int SequencePos;
	public List<SpriteRenderer> ImageList;

	public Text title;
	public Button startButton;



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

		if ((Input.GetKeyDown (KeyCode.Space)) && (SequencePos!= 7)  && (SequencePos!= 8)) {
			StartCoroutine ("Fade");
			SequencePos++;

		}

		if (SequencePos == 7) {
			title.gameObject.SetActive (true);
			startButton.gameObject.SetActive (true);
			SequencePos++;
		}

		//display images
		//if i==7
		//do zoom thing

		//if key press then i++




	
	}
}
