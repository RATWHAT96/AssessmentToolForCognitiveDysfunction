using UnityEngine;
using System.IO;
using System.Collections;
using UnityEngine.UI;

public class Detect1 : MonoBehaviour
{	
	
	public int  correct1 = 0;
	public int  wrong1 = 0;
	public int total1 = 0;
	private float startTime;
    private float t;
	public Text remaining;//UI element displaying number of books remaining
	public Text correct;//UI element displaying number of correct categorisations
	public Text wrong;//UI element displaying number of wrong categorisations
	Collider other;

	public GameObject correctNotify;//UI element displayed when categorisation is correct
    public GameObject wrongNotify;//UI element displayed when categorisation is wrong


	private void Start() {
		startTime = Time.time;
		correctNotify.SetActive(false);
        wrongNotify.SetActive(false);
	}

	//Record time an answer is correct in Log.txt file 
	public void logCorrectTime() {
		t = Time.time - startTime;
		string content = "Correct answer at: " + t.ToString() + "\n\n";
		string path = Application.dataPath + "/Log.txt";
		File.AppendAllText(path, content);
	}

	//Record time an answer is wrong in Log.txt file 
	public void logWrongTime() {
		t = Time.time - startTime;
		string content = "Wrong answer at: " + t.ToString() + "\n\n";
		string path = Application.dataPath + "/Log.txt";
		File.AppendAllText(path, content);
	}

	//Will check whether the book is categorised correctly based on the tags assigned to it 
	void OnTriggerEnter(Collider Other)
	{	
		other = Other;
		
		if(Other.CompareTag("Correct1"))
		{	
			correct1 += 1;
			logCorrectTime();
			correctNotify.SetActive(true);
			correct.text   = (int.Parse(correct.text) + 1).ToString();//increase the number of correct categorisation diplayed to the player
			remaining.text = (int.Parse(remaining.text) - 1).ToString();//decreases the number of books remaining to categorise
		}
		if(Other.CompareTag("Correct2"))
		{
			wrong1 += 1;
			logWrongTime();
			wrongNotify.SetActive(true);
			wrong.text   = (int.Parse(wrong.text) + 1).ToString();//increase the number of wrong categorisation diplayed to the player
			remaining.text = (int.Parse(remaining.text) - 1).ToString();
		}
		if(Other.CompareTag("Correct3"))
		{
			wrong1 += 1;
			logWrongTime();
			wrongNotify.SetActive(true);
			wrong.text   = (int.Parse(wrong.text) + 1).ToString();
			remaining.text = (int.Parse(remaining.text) - 1).ToString();
		}
		
		Other.enabled = false;
		
		StartCoroutine("WaitForASec");

		total1 = wrong1 + correct1;

		FindObjectOfType<GameManager>().taskCheck();

	}


	//will hide the correctNotify or wrongNotify from the display after 2 seconds.
	IEnumerator WaitForASec() {
		yield return new WaitForSeconds(2);
		correctNotify.SetActive(false);
		wrongNotify.SetActive(false);
		other.enabled = true;
	}
}
