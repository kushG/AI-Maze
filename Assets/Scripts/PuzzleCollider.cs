using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PuzzleCollider : MonoBehaviour {
    public int keyno = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {

        if(col.gameObject.tag == "Player")
        { 
            GameObject player = col.collider.gameObject;
            player.GetComponent<KeyDoor>().keyNo = keyno;
			GameObject.Find("Key").GetComponent<Text>().text = " Key No : " + keyno;
        }
    }
}
