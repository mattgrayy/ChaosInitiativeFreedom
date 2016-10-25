using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;



public class CutSceneScript : MonoBehaviour
{

    public int SequencePos;
    public List<SpriteRenderer> ImageList;

    public Text title;
    public Button startButton;


	public AudioSource Walking;
	public AudioSource Sniffing;
	public AudioSource TVclick;
	public AudioSource TVsound;




    IEnumerator Fade()
    {


        for (float f = 1f; f >= 0f; f -= 0.01f)
        {
            Color alfha = ImageList[SequencePos].color;
            alfha.a = f;
            ImageList[SequencePos].color = alfha;


        }

		if ((Input.GetKeyDown (KeyCode.Space)) && (SequencePos!= 7)  && (SequencePos!= 8)) {
			StartCoroutine ("Fade");
			SequencePos++;


		switch (SequencePos) {
		case 0:
			TVclick.Play ();
			break;
		case 1:
			Sniffing.Play ();
			break;
		}




        yield return null;
    }


		if (SequencePos == 7) {
			Debug.Log ("gfjgfjsdgf");
			title.gameObject.SetActive (true);
			startButton.gameObject.SetActive (true);
			SequencePos++;
		}


    void Start()
    {
        SequencePos = 0;

    }


    // Update is called once per frame
    void Update()
    {
		



        if ((Input.GetKeyDown(KeyCode.Space)) && (SequencePos != 3) && (SequencePos != 4))
        {
			
            StartCoroutine("Fade");
            SequencePos++;

        }

		    if (SequencePos == 3)
            {
				TVsound.mute = true;
				Walking.Play ();
				
                title.gameObject.SetActive(true);
                startButton.gameObject.SetActive(true);
                SequencePos++;
            }

            //display images
            //if i==7
            //do zoom thing

            //if key press then i++





      
    }
}
