using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject pracPause;//screen displayed when game is paused before practise phase and between practise and test phases
    public Text pause;//text on pause screen
    public List<GameObject> Books = new List<GameObject>();//List of the books to be picked up
    public int j; //Variable to keep track of the book object in the list
    public Material white; //material to highlight book to be picked up
    GameObject player;
    GameObject detect1; 
    GameObject detect2; 
    GameObject detect3; 
    Detect1 detectScript1;
    Detect2 detectScript2;
    Detect3 detectScript3;
    

    void Start() {
        pracPause.SetActive(true);//activates screen before practice phase 
        StartCoroutine("WaitForPracticePhase");//will turn off pause screen after 30 seconds
        j = 0;
		player = GameObject.Find("FPSController");
        detect1 = GameObject.Find("BookDetector1");
        detect2 = GameObject.Find("BookDetector2");
        detect3 = GameObject.Find("BookDetector3");
        detectScript1 = detect1.GetComponent<Detect1>();
        detectScript2 = detect2.GetComponent<Detect2>();
        detectScript3 = detect3.GetComponent<Detect3>();
        //add the relavant book GameObjects to the List
        for (int i = 1; i < 60; i++) 
        {
            Books.Add(GameObject.Find(i.ToString()));
        }
        Books[j].GetComponent<Renderer>().material = white;//highlights the book to be selected in white
        Books[j].GetComponent<PickUp>().theDest = GameObject.Find("Destination").transform;
        
        //Creates Log.txt file in Contents folder
        string path = Application.dataPath + "/Log.txt";
        if(!File.Exists(path)){
            File.WriteAllText(path, "Data\n\n");
        }  
        
    }

    IEnumerator WaitForPracticePhase() {
		yield return new WaitForSeconds(30);
        pracPause.SetActive(false);  
	}

    //Records data to the Log.txt file at the end of te game
    void LogData() {
        string path = Application.dataPath + "/Log.txt";
        string totalScore  = (detectScript1.correct1 + detectScript2.correct2 + detectScript3.correct3).ToString();
        string totalWrong  = (detectScript1.wrong1 + detectScript2.wrong2 + detectScript3.wrong3).ToString();
        string content = "Player Score: " + totalScore + "\n" + "Player Wrongs: " + totalWrong + "\n";
        File.AppendAllText(path, content);
    }

    //check whether the practice is over or whether the task has been completed 
    public void taskCheck() {
        j++;//moves to the next book in the list
        Books[j].GetComponent<Renderer>().material = white;//highlights the next book in white
        int total = (detectScript1.total1 + detectScript2.total2 + detectScript3.total3);
        
        //check whether practise phases has ended
        if(total == 18) {
            pause.text = "That is the end of the practise. The task will resume in 50 seconds.";
            pracPause.SetActive(true); 
            StartCoroutine("WaitForTestPhase"); //will turn off pause screen after 50 seconds
        }


        //check whether test phase has ended
        if(total == 58) {
            LogData();
            Application.Quit();
        }
    }

    IEnumerator WaitForTestPhase() {
		yield return new WaitForSeconds(50);
        pracPause.SetActive(false); 
    }
}
