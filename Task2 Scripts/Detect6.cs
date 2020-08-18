using UnityEngine;
using System.IO;
using System.Collections;
using UnityEngine.UI;

public class Detect6 : MonoBehaviour
{
	//UI elements for indivuals memebers of text sequence
	public Text d1;
	public Text d2;
	public Text d3;
	public Text d4;
	public Text d5;
	public Text d6;
	public Text d7;
	public Text d8;
	//UI Elements for order rules
	public Text r1;
	public Text r2;
	
	//Bools to measure sequence
	private bool one;
	private bool two;
	private bool three;
	private bool four;
	private bool Wrong;
	
	public Text remaining; //UI element displaying number of books remaining
	public GameObject correctNotify2;//UI element displayed when categorisation is correct
    public GameObject wrongNotify2;//UI element displayed when categorisation is wrong
	public Text Change;//UI element displayed before sequence change
	public GameObject pracPause;//screen displayed when game is paused between practise and test phases
	public Text ppause;//Text UI element on the pause screen
	private Transform tr; //position of next books
	public GameObject player;
	BoxCollider b;
	Collider other;
	private float startTime;
    private float t;
	
	private void Start() {
		startTime = Time.time;
		player = GameObject.Find("FPSController");		
		correctNotify2.SetActive(false);
        wrongNotify2.SetActive(false);
		tr = GameObject.Find("6").transform;
		one   = false;
		two   = false;
		Wrong = false;
		ppause.text = "The test sequences will begin in 30 seconds and will consist of 9 sequences.";
	}

	void resetBooks() {
		//destroy books
		Destroy(GameObject.Find("5"));
		//move others
		tr.position = tr.position + new Vector3(8.78f,0,0);
		StartCoroutine("WaittForASec");
	}

	//Logic for book detection of the 6th sequence
	void OnTriggerEnter(Collider Other)
	{	
		other = Other;
		if(Other.CompareTag("Wrong"))
		{	
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logWrongTime();
			wrongNotify2.SetActive(true);
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logChange();
			Wrong = true;
		}

		if(Other.CompareTag("F"))
		{
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logCorrectTime();
			correctNotify2.SetActive(true);
			one = true;
		}

		if(Other.CompareTag("D") && one == false)
		{
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logWrongTime();
			wrongNotify2.SetActive(true);
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logChange();
			Wrong = true;
		}

		if(Other.CompareTag("D") && one == true)
		{
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logCorrectTime();
			correctNotify2.SetActive(true);
			two = true;
		}

		if(Other.CompareTag("B") && two == false)
		{
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logWrongTime();
			wrongNotify2.SetActive(true);
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logChange();
			Wrong = true;
		}

		if(Other.CompareTag("B") && two == true)
		{
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logCorrectTime();
			correctNotify2.SetActive(true);
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logChange();;
			Wrong = true;
		}

		Other.enabled = false;

		StartCoroutine("WaitForASec");
		
		if (Wrong == true) {
			b = gameObject.GetComponent<BoxCollider>();
			b.enabled = false;
			player.SetActive(false);
			pracPause.SetActive(true);
			resetBooks();
		}
	}

	IEnumerator WaittForASec() {
		yield return new WaitForSeconds(30);
		Change.text = "Next Sequence Will Be Displayed for 5 Seconds";
		pracPause.SetActive(false);
		player.SetActive(true);
		StartCoroutine("WaitForAnotherSec");
	}

	IEnumerator WaitForASec() {
		yield return new WaitForSeconds(2);
		correctNotify2.SetActive(false);
		wrongNotify2.SetActive(false);
		other.enabled = true;
	}

	//CHANGE CHANGE CHANGE
	//Displays sequence and rules
	IEnumerator WaitForAnotherSec() {
		yield return new WaitForSeconds(6);
		Change.text = "";
		r1.text = "Ascending";
		r2.text = "Numbers Then Letters";
		yield return new WaitForSeconds(2);
		d1.text = "";
		d2.text = "A";
		d3.text = "1";
		d4.text = "G";
		d5.text = "9";
		d6.text = "8";
		d7.text = "D";
		d8.text = "F";
		StartCoroutine("WaitForFiveSecs");
	}
	//Hide sequence and rules
	IEnumerator WaitForFiveSecs() {
		yield return new WaitForSeconds(5);
		d1.text = "";
		d2.text = "";
		d3.text = "";
		d4.text = "";
		d5.text = "";
		d6.text = "";
		d7.text = "";
		d8.text = "";
		r1.text = "";
		r2.text = "";
	}
}
