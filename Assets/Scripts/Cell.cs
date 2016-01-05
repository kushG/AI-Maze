using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cell : MonoBehaviour {
	public int weight;
	public List<Transform> adjacents;
	public int AdjacentsOpened = 0;
    public bool isVisited = false;
    public int splitCount = 0;
    public bool isGoal = false;
    public int PrimAdjacents = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
