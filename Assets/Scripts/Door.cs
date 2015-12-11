using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
    public float speed = 0.5f;
    bool opendoor = false;
    public float threshold = 0;
    public float threshOldReturn;
    // Use this for initialization
    void Start () {
        threshOldReturn = transform.localPosition.x;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            opendoor = !opendoor;
            threshold = transform.localPosition.x + 0.545f;
            threshOldReturn = transform.localPosition.x - 0.545f; 

            //transform.position = new Vector3(transform.position.x + 0.545f * speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        if (opendoor)
        {
            if (transform.localPosition.x < threshold)
            {
                transform.Translate(0.545f * speed * Time.deltaTime, 0, 0);
            }
        }
        else
        {
            if (transform.localPosition.x > threshOldReturn)
            {
                transform.Translate(-0.545f * speed * Time.deltaTime, 0, 0);
            }
        }
    }

    
}
