using UnityEngine;
using System.Collections;

public class WallBreak : MonoBehaviour {

    bool startUpdate = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (startUpdate)
        {
            gameObject.SetActive(false);
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            Debug.Log("Wall Player");
            startUpdate = true;
        }
    }
}
