using UnityEngine;
using System.IO;
using System.Collections;
using UnityEngine.UI;

public class Detect15 : MonoBehaviour
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
	private Transform tr; //position of next books
	BoxCollider b;
	Collider other;
	private float startTime;
    private float t;


	private void Start() {
		startTime = Time.time;
		correctNotify2.SetActive(false);
        wrongNotify2.SetActive(false);
		one   = false;
		two   = false;
		three = false;
		four  = false;
		Wrong = false;
	}

	//CHANGE CHANGE TRANSFORM
	void resetBooks() {
		StartCoroutine("WaitForAnotherSec");
	}

	//Logic for book detection of the 15th sequence
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

		if(Other.CompareTag("J"))
		{
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logCorrectTime();
			correctNotify2.SetActive(true);
			one = true;
		}

		if(Other.CompareTag("I") && one == false)
		{
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logWrongTime();
			wrongNotify2.SetActive(true);
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logChange();
			Wrong = true;
		}

		if(Other.CompareTag("I") && one == true)
		{
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logCorrectTime();
			correctNotify2.SetActive(true);
			two = true;
		}

		if(Other.CompareTag("E") && two == false)
		{
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logWrongTime();
			wrongNotify2.SetActive(true);
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logChange();
			Wrong = true;
		}

		if(Other.CompareTag("E") && two == true)
		{
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logCorrectTime();
			correctNotify2.SetActive(true);
			three = true;
		}

		if(Other.CompareTag("C") && three == false)
		{
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logWrongTime();
			wrongNotify2.SetActive(true);
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logChange();
			Wrong = true;
		}

		if(Other.CompareTag("C") && three == true)
		{
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logCorrectTime();
			correctNotify2.SetActive(true);
			four = true;
		}

		if(Other.CompareTag("A") && four == false)
		{
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logWrongTime();
			wrongNotify2.SetActive(true);
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logChange();
			Wrong = true;
		}

		if(Other.CompareTag("A") && four == true)
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
			Change.text = "Task Complete";
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
		yield return new WaitForSeconds(3);
		Application.Quit();
	}
	//Hide sequence and rules

}
