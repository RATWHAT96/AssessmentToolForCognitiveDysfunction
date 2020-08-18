using UnityEngine;
using System.IO;
using System.Collections;
using UnityEngine.UI;

public class Detect10 : MonoBehaviour
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
	private bool five;
	private bool Wrong;
	
	public Text remaining; //UI element displaying number of books remaining
	public GameObject correctNotify2;//UI element displayed when categorisation is correct
    public GameObject wrongNotify2;//UI element displayed when categorisation is wrong
	public Text Change;//UI element displayed before sequence change
	private Transform tr; //position of next books
	BoxCollider b;
	Collider other;
	private float startTime;
    private float t;

	private void Start() {
		startTime = Time.time;
		correctNotify2.SetActive(false);
        wrongNotify2.SetActive(false);
		tr = GameObject.Find("10").transform;
		one   = false;
		two   = false;
		three = false;
		four  = false;
		five  = false;
		Wrong = false;
	}

	//CHANGE CHANGE TRANSFORM
	void resetBooks() {
		//destroy books
		Destroy(GameObject.Find("9"));
		//move others
		tr.position = tr.position + new Vector3(13.32f,0,0);
		StartCoroutine("WaitForAnotherSec");
	}

	//Logic for book detection of the 10th sequence
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

		if(Other.CompareTag("B"))
		{
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logCorrectTime();
			correctNotify2.SetActive(true);
			one = true;
		}

		if(Other.CompareTag("C") && one == false)
		{
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logWrongTime();
			wrongNotify2.SetActive(true);
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logChange();
			Wrong = true;
		}

		if(Other.CompareTag("C") && one == true)
		{
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logCorrectTime();
			correctNotify2.SetActive(true);
			two = true;
		}

		if(Other.CompareTag("G") && two == false)
		{
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logWrongTime();
			wrongNotify2.SetActive(true);
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logChange();
			Wrong = true;
		}

		if(Other.CompareTag("G") && two == true)
		{
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logCorrectTime();
			correctNotify2.SetActive(true);
			three = true;
		}

		if(Other.CompareTag("3") && three == false)
		{
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logWrongTime();
			wrongNotify2.SetActive(true);
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logChange();
			Wrong = true;
		}

		if(Other.CompareTag("3") && three == true)
		{
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logCorrectTime();
			correctNotify2.SetActive(true);
			four = true;
		}

		if(Other.CompareTag("6") && four == false)
		{
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logWrongTime();
			wrongNotify2.SetActive(true);
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logChange();
			Wrong = true;
		}

		if(Other.CompareTag("6") && four == true)
		{
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logCorrectTime();
			correctNotify2.SetActive(true);
			five = true;
		}

		if(Other.CompareTag("8") && five == false)
		{
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logWrongTime();
			wrongNotify2.SetActive(true);
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logChange();
			Wrong = true;
		}

		if(Other.CompareTag("8") && five == true)
		{
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logCorrectTime();
			correctNotify2.SetActive(true);
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logChange();
			Wrong = true;
		}

		Other.enabled = false;

		StartCoroutine("WaitForASec");

		if (Wrong == true) {
			b = gameObject.GetComponent<BoxCollider>();
			b.enabled = false;
			Change.text = "Next Sequence Will Be Displayed for 5 Seconds";
			resetBooks();
		}
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
		r1.text = "Descending";
		r2.text = "All Letters Then Number";
		yield return new WaitForSeconds(2);
		d1.text = "8";
		d2.text = "b";
		d3.text = "4";
		d4.text = "5";
		d5.text = "C";
		d6.text = "G";
		d7.text = "3";
		d8.text = "e";
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
