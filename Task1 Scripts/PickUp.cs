using UnityEngine;
using System.IO;

public class PickUp : MonoBehaviour
{	
	// New transform position for the picked up object  
	public Transform theDest;
	
	// Book materials
	public Material redBook;
    public Material blueBook;
    public Material greenBook;

	//Array used to change book colors with each number corresponding to a specific book material (1 = red, 2 = blue, 3 = green)
	int[] colors = new int[58] {3,2,2,3,2,1,2,2,1,1,1,3,3,1,1,1,2,2,2,3,1,3,2,3,1,2,3,2,1,2,1,2,1,3,1,3,2,1,2,3,1,2,1,3,3,2,1,2,1,2,1,3,1,3,1,2,1,2};

	GameObject g;
	GameManager gg;
	public int i;
	private int k;
	private float startTime;
    private float t;

	private void Start() {
		startTime = Time.time;
		g = GameObject.Find("GameManager");
		gg = g.GetComponent<GameManager>();
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
		//gg.books is a list in GameManager.cs that contains the book game objects that need to be picked up in a predetermined order 
		//guarantees the correct book gameobject (gg.Books[i]) will be picked up and only once the player (FPSController) is close enough (<=6) 
		if (((GameObject.Find("FPSController").transform.position.x - this.transform.position.x) <= 6) && gameObject == gg.Books[gg.j])
		{	
			logTimeUp();
			i = gg.j; //variable in GameManager.cs that correspond to a number in a pre-determined order of books that need to be picked up
			k = colors[i];
			//Will change the material of the book once picked up based on the colors array
			switch(k){
				case 1:
					GetComponent<Renderer>().material = redBook;
					break;
				case 2:
					GetComponent<Renderer>().material = blueBook;
					break;
				case 3:
					GetComponent<Renderer>().material = greenBook;
					break;
			}
			
			GetComponent<Rigidbody>().useGravity = false;
			GetComponent<Rigidbody>().freezeRotation = true;
			GetComponent<BoxCollider>().enabled = !GetComponent<BoxCollider>().enabled;  //disables box collider
			// Changes the position of the selected book
			this.transform.position = theDest.position;
			this.transform.parent = GameObject.Find("Destination").transform;
		}
	}

	void OnMouseUp()
	{		
		if ((GameObject.Find("FPSController").transform.position.x - this.transform.position.x) <= 6 && gameObject == gg.Books[gg.j])
		{
			logTimeDown();
			//allows for the book gameobject to be dropped
			this.transform.parent = null;
			GetComponent<Rigidbody>().freezeRotation = false; 
			GetComponent<Rigidbody>().useGravity = true;
			GetComponent<BoxCollider>().enabled = true;
		}
	}
}

