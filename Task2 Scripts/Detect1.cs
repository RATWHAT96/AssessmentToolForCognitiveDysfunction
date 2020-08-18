using UnityEngine;
using System.IO;
using System.Collections;
using UnityEngine.UI;


public class Detect1 : MonoBehaviour
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
	private Transform tr; //position of next books
	BoxCollider b;
	Collider other;
	private float startTime;
    private float t;

	private void Start() {
		//Creates Log.txt file
		string path = Application.dataPath + "/Log.txt";
        if(!File.Exists(path)){
            File.WriteAllText(path, "Data\n\n");
        }
		StartCoroutine("StartScreen");
	}

	//Display the start screen for 30 seconds and then display first sequence
	IEnumerator StartScreen() {
		yield return new WaitForSeconds(30);
		pracPause.SetActive(false);
		startTime = Time.time;
		correctNotify2.SetActive(false);
        wrongNotify2.SetActive(false);
		one   = false;
		two   = false;
		three = false;
		four  = false;
		Wrong = false;
		tr = GameObject.Find("1").transform;
		r1.text = "Ascending";
		r2.text = "";
		d1.text = "6";
		d2.text = "8";
		d3.text = "3";
		d4.text = "5";
		d5.text = "4";
		d6.text = "";
		d7.text = "";
		d8.text = "";
		StartCoroutine("WaitForFiveSecs");
	}

	//Hide sequence and rules after 5 seconds
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

	void resetBooks() {
		//destroy books
		Destroy(GameObject.Find("0"));
		//moves replacement book into position
		tr.position = tr.position + new Vector3(3.17f,0,0);
		StartCoroutine("WaitForAnotherSec");
	}

	//Displays second sequence
	IEnumerator WaitForAnotherSec() {
		yield return new WaitForSeconds(6);
		Change.text = "";
		r1.text = "Descending";
		r2.text = "Numbers Only";
		yield return new WaitForSeconds(2);
		d1.text = "6";
		d2.text = "B";
		d3.text = "A";
		d4.text = "1";
		d5.text = "3";
		d6.text = "";
		d7.text = "";
		d8.text = "";
		StartCoroutine("WaitForFiveSecs");
	}

	//Records the time the book is correctly categorised in the Log.txt file
	public void logCorrectTime() {
		t = Time.time - startTime;
		string content = "Correct answer at: " + t.ToString() + "\n\n";
		string path = Application.dataPath + "/Log.txt";
		File.AppendAllText(path, content);
	}

	//Records the sequence changes in the Log.txt file
	public void logChange() {
		t = Time.time - startTime;
		remaining.text = (int.Parse(remaining.text) + 1).ToString();
		string content = "Change at: " + t.ToString() + "\n\n";
		string path = Application.dataPath + "/Log.txt";
		File.AppendAllText(path, content);
	}

	//Records the time the book is correctly categorised in the Log.txt file
	public void logWrongTime() {
		t = Time.time - startTime;
		string content = "Wrong answer at: " + t.ToString() + "\n\n";
		string path = Application.dataPath + "/Log.txt";
		File.AppendAllText(path, content);
	}

	//Logic for book detection of the first sequence
	void OnTriggerEnter(Collider Other)
	{	
		other = Other;
		if(Other.CompareTag("Wrong"))
		{	
			logWrongTime();
			wrongNotify2.SetActive(true);
			logChange();
			Wrong = true;
		}

		if(Other.CompareTag("3"))
		{
			logCorrectTime();
			correctNotify2.SetActive(true);
			one = true;
		}

		if(Other.CompareTag("4") && one == false)
		{
			logWrongTime();
			wrongNotify2.SetActive(true);
			logChange();
			Wrong = true;
		}

		if(Other.CompareTag("4") && one == true)
		{
			logCorrectTime();
			correctNotify2.SetActive(true);
			two = true;
		}

		if(Other.CompareTag("5") && two == false)
		{
			logWrongTime();
			wrongNotify2.SetActive(true);
			logChange();
			Wrong = true;
		}

		if(Other.CompareTag("5") && two == true)
		{
			logCorrectTime();
			correctNotify2.SetActive(true);
			three = true;
		}

		if(Other.CompareTag("6") && three == false)
		{
			logWrongTime();
			wrongNotify2.SetActive(true);
			logChange();
			Wrong = true;
		}

		if(Other.CompareTag("6") && three == true)
		{
			logCorrectTime();
			correctNotify2.SetActive(true);
			four = true;
		}

		if(Other.CompareTag("8") && four == false)
		{
			logWrongTime();
			wrongNotify2.SetActive(true);
			logChange();
			Wrong = true;
		}

		if(Other.CompareTag("8") && four == true)
		{
			logCorrectTime();
			correctNotify2.SetActive(true);
			logChange();
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
}
