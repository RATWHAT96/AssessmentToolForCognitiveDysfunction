using UnityEngine;
using System.IO;

public class PickUp : MonoBehaviour
{	
	public Transform theDest; // New transform position for the picked up object 
	private float startTime;
    private float t;
	public int i;
	private int j;

	private void Start() {
		startTime = Time.time;
	}

	//Records the time an object is picked up on Log.txt
	void logTimeUp() {
		t = Time.time - startTime;
		string content = "Picked up book at: " + t.ToString() + "\n";
		string path = Application.dataPath + "/Log.txt";
		File.AppendAllText(path, content);
	}

	//Records the time an object is dropped on Log.txt
	void logTimeDown() {
		t = Time.time - startTime;
		string content = "Dropped book at: " + t.ToString() + "\n";
		string path = Application.dataPath + "/Log.txt";
		File.AppendAllText(path, content);
	}

	void OnMouseDown()
	{	
		//guarantees a book can be picked up only once the player (FPSController) is close enough (<=6) 
		if (((GameObject.Find("FPSController").transform.position.x - this.transform.position.x) <= 6)) 
		{	
			logTimeUp();
			GetComponent<Rigidbody>().useGravity = false;
			GetComponent<Rigidbody>().freezeRotation = true;
			GetComponent<BoxCollider>().enabled = !GetComponent<BoxCollider>().enabled;  //turns off collider when picked up
			this.transform.position = theDest.position;
			this.transform.parent = GameObject.Find("Destination").transform;
		}
	}

	void OnMouseUp()
	{		
		if ((GameObject.Find("FPSController").transform.position.x - this.transform.position.x) <= 6) 
		{
			logTimeDown();
			this.transform.parent = null;
			GetComponent<Rigidbody>().freezeRotation = false; 
			GetComponent<Rigidbody>().useGravity = true;
			GetComponent<BoxCollider>().enabled = true; //enables collider once picked up 
		}
	}
}


