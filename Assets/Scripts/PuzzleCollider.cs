using UnityEngine;
using System.Collections;

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
        Debug.Log("Puzzle");
		Debug.Log(col.collider.gameObject.tag);
		Debug.Log(col.gameObject.tag);
        if(col.gameObject.tag == "Player")
        { 
            Debug.Log("Puzzle Collider");
            GameObject player = col.collider.gameObject;
            player.GetComponent<KeyDoor>().keyNo = keyno;
        }
    }
}
