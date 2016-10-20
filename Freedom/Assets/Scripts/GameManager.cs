using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Text Bar;
    public float FreedomAmount;
	public float ReduceSpeed = 1f;
	public Image AmountInBar;

	public bool Overload = false;
	public float OverloadCounter = 100f;


	private int RainbowColor = 1;


	//colours
	private Color Red = Color.red;
	private Color Orange = Color.magenta;
	private Color Yellow = Color.yellow;
	private Color Green = Color.green;
	private Color Blue = Color.blue;


	public float duration = 10f;
	public float FadeTimer = 0f;







	public void UpHigh()
	{
		FreedomAmount += 20f;
		AmountInBar.fillAmount += 0.2f;

		if (FreedomAmount >= 100f) {
			Overload = true;
			FreedomAmount = 100f;
		}


	}


	public void DownHigh()
	{

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
			Bar.text = FreedomAmount.ToString ();



			switch (RainbowColor)
			{

			//red to orange
			case 1:
				AmountInBar.CrossFadeColor (Orange, 0.7f, true, true);
				FadeTimer++;			
				if (FadeTimer >= 50f) {
					Debug.Log ("in");
					RainbowColor = 2;
					FadeTimer = 0f;
				}				
				break;
			//ornage to yellow
			case 2:
				AmountInBar.CrossFadeColor (Yellow, 0.7f, true, true);

				FadeTimer++;			
				if (FadeTimer >= 50f) {
					Debug.Log ("in");
					RainbowColor = 3;
					FadeTimer = 0f;
				}
				break;

			//yellow to green
			case 3:
				AmountInBar.CrossFadeColor (Green, 0.7f, true, true);
				FadeTimer++;			
				if (FadeTimer >= 50f) {
					Debug.Log ("in");
					RainbowColor = 4;
					FadeTimer = 0f;
				}
				break;

			//green to blue
			case 4:
				AmountInBar.CrossFadeColor (Blue, 0.7f, true, true);
				FadeTimer++;			
				if (FadeTimer >= 50f) {
					Debug.Log ("in");
					RainbowColor = 5;
					FadeTimer = 0f;
				}
				break;

			//blue To red
			case 5:
				AmountInBar.CrossFadeColor (Red, 0.7f, true, true);
				FadeTimer++;			
				if (FadeTimer >= 60f) {
					Debug.Log ("in");
					RainbowColor = 1;
					FadeTimer = 0f;


				}
				break;
		
			}


		} else {


			//do overlaod stuff

			AmountInBar.CrossFadeColor (Color.red, 1f, true, true);
			OverloadCounter -= 10f * Time.deltaTime;
			if (OverloadCounter <= 0) {

				Overload = false;
				AmountInBar.CrossFadeColor (Color.blue, 1f, true, true);
				OverloadCounter = 100f;

			}




		}






	}
}
