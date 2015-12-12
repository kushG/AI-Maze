using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public float speed = 0.5f;
	// Use this for initialization
	void Start () {
	
	}

	public float mousePosLastFrame;
	// Update is called once per frame
	void Update () {

		if (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.UpArrow)))
		{
			transform.Translate(0, 0, speed * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
			transform.Translate(0, 0, -speed * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
			transform.Translate(-speed * Time.deltaTime, 0, 0);
		}

		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
			transform.Translate(speed * Time.deltaTime, 0, 0);
		}
	}
}
