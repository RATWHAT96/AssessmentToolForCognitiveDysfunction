using UnityEngine;
using System.IO;
using System.Collections;
using UnityEngine.UI;

public class Detect3 : MonoBehaviour
{	
	
	public int  correct3 = 0;
	public int  wrong3 = 0;
	public int total3 = 0;
	private float startTime;
    private float t;
	public Text remaining;//UI element displaying number of books remaining
	public Text correct;//UI element displaying number of correct categorisations
	public Text wrong;//UI element displaying number of wrong categorisations
	Collider other;

	public GameObject correctNotify3;//UI element displayed when categorisation is correct
    public GameObject wrongNotify3;//UI element displayed when categorisation is wrong

	private void Start() {
		startTime = Time.time;
		correctNotify3.SetActive(false);
        wrongNotify3.SetActive(false);
	}

	//Will check whether the book is categorised correctly based on the tags assigned to it 
	void OnTriggerEnter(Collider Other)
	{	
		other = Other;
		if(Other.CompareTag("Correct1"))
		{
			wrong3 += 1;
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logWrongTime();
			wrongNotify3.SetActive(true);
			wrong.text   = (int.Parse(wrong.text) + 1).ToString();//increases the number of wrong categorisation diplayed to the player
			remaining.text = (int.Parse(remaining.text) - 1).ToString();//decreases the number of books remaining to categorise
		}
		if(Other.CompareTag("Correct2"))
		{
			wrong3 += 1;
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logWrongTime();
			wrongNotify3.SetActive(true);
			wrong.text   = (int.Parse(wrong.text) + 1).ToString();
			remaining.text = (int.Parse(remaining.text) - 1).ToString();
		}
		if(Other.CompareTag("Correct3"))
		{
			correct3 += 1;
			GameObject.Find("BookDetector1").GetComponent<Detect1>().logCorrectTime();
			correctNotify3.SetActive(true);
			correct.text   = (int.Parse(correct.text) + 1).ToString();//increase the number of correct categorisation diplayed to the player
			remaining.text = (int.Parse(remaining.text) - 1).ToString();
		}
		
		Other.enabled = false;

		StartCoroutine("WaitForASec");

		total3 = wrong3 + correct3;

		FindObjectOfType<GameManager>().taskCheck();

	}

	//will hide the correctNotify3 or wrongNotify3 from the display after 2 seconds.
	IEnumerator WaitForASec() {
		yield return new WaitForSeconds(2);
		correctNotify3.SetActive(false);
		wrongNotify3.SetActive(false);
		other.enabled = true;
	}

}
