using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Detect2 : MonoBehaviour
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
		Wrong = false;
		tr = GameObject.Find("2").transform;
	}

	void resetBooks() {
		//destroy books
		Destroy(GameObject.Find("1"));
		//move others
		tr.position = tr.position + new Vector3(4.09f,0,0);
		StartCoroutine("WaitForAnotherSec");
	}


	//Logic for book detection of the 2nd sequence
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

		if(Other.CompareTag("6"))
		{
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logCorrectTime();
			correctNotify2.SetActive(true);
			one = true;
		}

		if(Other.CompareTag("3") && one == false)
		{	
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logWrongTime();
			wrongNotify2.SetActive(true);
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logChange();
			Wrong = true;
		}

		if(Other.CompareTag("3") && one == true)
		{
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logCorrectTime();
			correctNotify2.SetActive(true);
			two = true;
		}

		if(Other.CompareTag("1") && two == false)
		{	
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logWrongTime();
			wrongNotify2.SetActive(true);
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logChange();
			Wrong = true;
		}

		if(Other.CompareTag("1") && two == true)
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

	IEnumerator WaitForAnotherSec() {
		yield return new WaitForSeconds(6);
		Change.text = "";
		r1.text = "Ascending";
		r2.text = "Letters Only";
		yield return new WaitForSeconds(2);
		d1.text = "6";
		d2.text = "A";
		d3.text = "G";
		d4.text = "3";
		d5.text = "D";
		d6.text = "";
		d7.text = "";
		d8.text = "";
		
		StartCoroutine("WaitForFiveSecs");
	}

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
