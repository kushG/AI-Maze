using UnityEngine;
using System.Collections;

public class AIPlayer : MonoBehaviour {
	public GameObject aiPlayer;
	public float speed = 0.5f;
	bool triggered = false;
	GameObject target;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (triggered)
		{
			aiPlayer.transform.position = Vector3.MoveTowards(aiPlayer.transform.position, target.gameObject.transform.position, speed * Time.deltaTime);
		}
	
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
			triggered = true;
			target = col.gameObject;
			
		}
	}
}
