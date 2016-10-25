using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	int previousFreedomLevel = 0;
    public float FreedomAmount;
	public float ReduceSpeed = 1f;
	public Image AmountInBar;
    //public ParticleSystem overloadParticle;

	public bool Overload = false;
	public float OverloadCounter = 100f;

	//colours
	private int RainbowColor = 1;
	private Color Red = Color.red;
	private Color Orange = Color.magenta;
	private Color Yellow = Color.yellow;
	private Color Green = Color.green;
	private Color Blue = Color.blue;


	public float duration = 10f;
	public float FadeTimer = 0f;


	public List<GameObject> GameObjectList;



	void GameOver()
	{

		//run the end game sequence
		Application.LoadLevel(0);

	}

	public void UpHigh()
	{
		Debug.Log("up hight start");
		FreedomAmount += 20f;

		if (FreedomAmount > 100f)
        {
			print ("overlaod");
			Overload = true;
            //overloadParticle.Simulate(0);

            FreedomAmount = 100f;
			Debug.Log("freedom 100");
		}
	}

	public void DownHigh()
	{
		Debug.Log ("down");
		FreedomAmount -= 20f;
		AmountInBar.fillAmount -= 0.2f;
	}

	// Use this for initialization
	void Start () {
		FreedomAmount = 100f;
		OverloadCounter = 100f;
		ReduceSpeed = 1.0f;
		RainbowColor = 1;
		AmountInBar.color = Color.white;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (!Overload) {
			FreedomAmount -= ReduceSpeed * Time.deltaTime;
			AmountInBar.fillAmount -= (ReduceSpeed / 100f) * Time.deltaTime;

			if (FreedomAmount <= 0f) {

				GameOver ();

			}

			switch (RainbowColor) {

			//red to orange
			case 1:
				AmountInBar.CrossFadeColor (Orange, 0.7f, true, true);
				FadeTimer++;			
				if (FadeTimer >= 50f) {
					RainbowColor = 2;
					FadeTimer = 0f;
				}				
				break;
			//ornage to yellow
			case 2:
				AmountInBar.CrossFadeColor (Yellow, 0.7f, true, true);

				FadeTimer++;			
				if (FadeTimer >= 50f) {
					RainbowColor = 3;
					FadeTimer = 0f;
				}
				break;

			//yellow to green
			case 3:
				AmountInBar.CrossFadeColor (Green, 0.7f, true, true);
				FadeTimer++;			
				if (FadeTimer >= 50f) {
					RainbowColor = 4;
					FadeTimer = 0f;
				}
				break;

			//green to blue
			case 4:
				AmountInBar.CrossFadeColor (Blue, 0.7f, true, true);
				FadeTimer++;			
				if (FadeTimer >= 50f) {
					RainbowColor = 5;
					FadeTimer = 0f;
				}
				break;

			//blue To red
			case 5:
				AmountInBar.CrossFadeColor (Red, 0.7f, true, true);
				FadeTimer++;		
                    	
				if (FadeTimer >= 60f) {
					RainbowColor = 1;
					FadeTimer = 0f;
				}
				break;
			}
		} else {
			//do overlaod stuff

			AmountInBar.CrossFadeColor (Color.red, 1f, true, true);
			OverloadCounter -= 10f * Time.deltaTime;

			if (OverloadCounter <= 0f) {
				//overloadParticle.Stop();
				Overload = false;
				AmountInBar.CrossFadeColor (Color.blue, 1f, true, true);
				OverloadCounter = 100f;
			}
		}






		//send the freedom value to all things
		//list of tihgs

		switch (previousFreedomLevel) {
		case 0:
			if (FreedomAmount < 50) {
				foreach (GameObject thing in GameObjectList) {
					Debug.Log (FreedomAmount);
					thing.GetComponent<SpriteControler> ().setFreedomAmount (1);
				}
				previousFreedomLevel = 1;
			}
			break;
		case 1:
			if (FreedomAmount >= 50 || FreedomAmount < 25) {
				int newFreedomLevel;

				if (FreedomAmount >= 50) {
					newFreedomLevel = 0;
				} else {
					newFreedomLevel = 2;
				}

				foreach (GameObject thing in GameObjectList) {
					Debug.Log (FreedomAmount);
					thing.GetComponent<SpriteControler> ().setFreedomAmount (newFreedomLevel);
				}
				previousFreedomLevel = newFreedomLevel;
			}
			break;
		case 2:
			if (FreedomAmount >= 25) {
				foreach (GameObject thing in GameObjectList) {
					Debug.Log (FreedomAmount);
					thing.GetComponent<SpriteControler> ().setFreedomAmount (1);
				}
				previousFreedomLevel = 1;
			}
			break;
		default:
			break;
		}
	}
}