using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Grid : MonoBehaviour
{

	public Vector2 gridSize;
	public List<List<Transform>> WeightSet;
	List<Transform> PrimSet;
    public List<Transform> NonPrimSet;
	List<Cell> PrimSetCell;
	public Transform CellPrefab;
	public Transform BoundaryPrefab;
	public Transform FPSController;
    public Transform bldgPrefab;
    public Texture roadText;
    Transform[,] grid;
	bool spawned = false;
    public GameObject buildingCells;
    public List<Transform> buildingPrefabs;
    public Transform bldg1;
    public Transform bldg2;
    public Transform bldg3;
    public List<int> PuzzleCube;
    public Texture number1;
    public Texture number2;
    public Texture number3;


    // Use this for initialization
    void Start()
	{
		InitializeGrid();
		SetAdjacents();
		SetupWeightLists();
		StartPrim();
        SetBoundaryWalls();
        AddBuildings();
        AddPuzzle();
    }

	// Update is called once per frame
	void Update()
	{
		if (!spawned)
		{
			SpawnPlayer();
		}

        //// Make atleast 3 doors operable
        //GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");

        //GameObject door1 = buildings[(buildings.Length - 1) / 3].transform.FindChild("Door").gameObject;
        //door1.AddComponent<Door>();
        //door1.GetComponent<Door>().keyNo = 1;

        //GameObject door2 = buildings[((buildings.Length - 1) / 3) + 3].transform.FindChild("Door").gameObject;
        //door1.AddComponent<Door>();
        //door1.GetComponent<Door>().keyNo = 2;

        //GameObject door3 = buildings[((buildings.Length - 1) / 3) + 2*3].transform.FindChild("Door").gameObject;
        //door1.AddComponent<Door>();
        //door1.GetComponent<Door>().keyNo = 3;


        //foreach (GameObject bldg in buildings)
        //{

        //}

	}


	void InitializeGrid()
	{
		grid = new Transform[(int)gridSize.x, (int)gridSize.y];
		for (int x = 0; x < gridSize.x; x++)
		{
			for (int z = 0; z < gridSize.y; z++)
			{
				grid[x, z] = Instantiate(CellPrefab, new Vector3(x, 0, z), Quaternion.identity) as Transform;
				int randomWeight = Random.Range(1, 10);
				//grid[x, z].GetComponent<GUIText>().text = randomWeight.ToString();
				grid[x, z].GetComponent<Cell>().weight = randomWeight;
				grid[x, z].transform.parent = transform;
			}
		}
	}

	void SetAdjacents()
	{
		for (int x = 0; x < gridSize.x; x++)
		{
			for (int z = 0; z < gridSize.y; z++)
			{
				// Boundary cells
				if (x - 1 >= 0)
				{
					//Left
					grid[x, z].GetComponent<Cell>().adjacents.Add(grid[x - 1, z]);
				}
				if (x + 1 < gridSize.x)
				{
					//Right;
					grid[x, z].GetComponent<Cell>().adjacents.Add(grid[x + 1, z]);
				}
				if (z - 1 >= 0)
				{
					//bot
					grid[x, z].GetComponent<Cell>().adjacents.Add(grid[x, z - 1]);
				}
				if (z + 1 < gridSize.y)
				{
					//top;
					grid[x, z].GetComponent<Cell>().adjacents.Add(grid[x, z + 1]);
				}

				grid[x, z].GetComponent<Cell>().adjacents.Sort(SortByLowestWeight);
            }
		}
	}

	int SortByLowestWeight(Transform inputA, Transform inputB)
	{
		//Given two transforms, find which cell's Weight
		// is the highest. Sort by the Weights.
		int a = inputA.GetComponent<Cell>().weight; //a's weight
		int b = inputB.GetComponent<Cell>().weight; //b's weight
		return a.CompareTo(b);
	}

	/// <summary>
	/// Create a list for each weight value
	/// List of weight 0, List of weight 1....
	/// </summary>
	void SetupWeightLists()
	{
		WeightSet = new List<List<Transform>>();
		for(int i=0; i<10; i++)
		{
			WeightSet.Add(new List<Transform>());
		}
	}

	void StartPrim()
	{
		
		Transform startCellTransform = grid[0, 0];
		startCellTransform.GetComponent<Renderer>().material.color = Color.green;
		PrimSet = new List<Transform>();
		PrimSet.Add(startCellTransform);
		Cell startCell = grid[0, 0].GetComponent<Cell>();

		// Assign all ajacents to their corresponing weight List
		foreach(Transform adj in startCell.adjacents)
		{
			WeightSet[adj.GetComponent<Cell>().weight].Add(adj);
		}

		FindNext(startCellTransform);
	}

	/// <summary>
	/// Uses Prim's Algorithm to find next cell to be added in PrimSet
	/// </summary>
	void FindNext(Transform currentCellTransform)
	{
		Cell currentCell = currentCellTransform.GetComponent<Cell>();
		foreach (Transform adj in currentCell.adjacents)
		{
			adj.GetComponent<Cell>().AdjacentsOpened++;
			if (!PrimSet.Contains(adj) && !WeightSet[adj.GetComponent<Cell>().weight].Contains(adj)) {
				WeightSet[adj.GetComponent<Cell>().weight].Add(adj);
			}
		}

		Transform next;

		do
		{
			//start from lowest to highest till we get atleast one cell in weight list
			bool empty = true;
			int lowestWeight = 0;
			for (int i = 0; i < 10; i++)
			{
				lowestWeight = i;
				if (WeightSet[lowestWeight].Count > 0)
				{
					empty = false;
					break;
				}
			}
			
			if(empty)
			{
                foreach (Transform cell in grid)
                {
                    if (!PrimSet.Contains(cell))
                    {
                        NonPrimSet.Add(cell);



                        cell.GetComponent<Renderer>().material.color = Color.black;
                        //cell.transform.Translate(0, 1, 0);
                        //Vector3 posForBldg = cell.transform.position;
                        //Transform bldg = Instantiate(bldgPrefab, new Vector3(posForBldg.x, posForBldg.y - 0.26f, posForBldg.z - 0.43f), Quaternion.identity) as Transform;
                        //bldg.parent = buildingCells.transform;
                        
                    }
                }
                //for (int x = 0; x < gridSize.x; x++)
                //{
                //    for (int z = 0; z < gridSize.y; z++)
                //    {
                //        if (!PrimSet.Contains(grid[x, z]))
                //        {
                //            Transform bldg = Instantiate(bldgPrefab, new Vector3(x, 1.15f, z), Quaternion.identity) as Transform;
                //            bldg.transform.Translate(new Vector3(0, 0, 0.321f));
                //        }
                //    }
                //}


                PrimSet[PrimSet.Count - 1].GetComponent<Renderer>().material.color = Color.red;
				return;
			}

			next = WeightSet[lowestWeight][0];
			WeightSet[lowestWeight].Remove(next);			
		} while (next.GetComponent<Cell>().AdjacentsOpened >= 2);


		//next.GetComponent<Renderer>().material.color = Color.yellow;
        next.GetComponent<Renderer>().material.mainTexture = roadText;
		PrimSet.Add(next);
		FindNext(next);

	}

	void SetBoundaryWalls()
	{
		for(int x=0; x < gridSize.x; x++)
		{
			for(int z=0; z< gridSize.y; z++)
			{
				if (x == 0)
				{
                    Vector3 posForBldg = grid[x, z].transform.position;
                    Transform wall = Instantiate(buildingPrefabs[Random.Range(0, buildingPrefabs.Count)], new Vector3(posForBldg.x - 1, posForBldg.y - 0.26f, posForBldg.z -0.57f), Quaternion.identity) as Transform;
                    
                    wall.gameObject.name = "Boundary";
					//wall.GetComponent<Renderer>().material.color = Color.black;
				}

				if(x == gridSize.x - 1)
				{
                    Vector3 posForBldg = grid[x, z].transform.position;
                    Transform wall = Instantiate(buildingPrefabs[Random.Range(0, buildingPrefabs.Count)], new Vector3(posForBldg.x + 1, posForBldg.y -0.26f, posForBldg.z - 0.57f), Quaternion.identity) as Transform;
                    wall.gameObject.name = "Boundary";
                }
				if (z == 0)
				{
                    Vector3 posForBldg = grid[x, z].transform.position;
                    Transform wall = Instantiate(buildingPrefabs[Random.Range(0, buildingPrefabs.Count)], new Vector3(posForBldg.x, posForBldg.y - 0.26f, posForBldg.z - 1 -0.43f), Quaternion.identity) as Transform;
                    wall.gameObject.name = "Boundary";
                    //wall.GetComponent<Renderer>().material.color = Color.black;
                }
				if (z == gridSize.y - 1)
				{
                    Vector3 posForBldg = grid[x, z].transform.position;
                    Transform wall = Instantiate(buildingPrefabs[Random.Range(0, buildingPrefabs.Count)], new Vector3(posForBldg.x, posForBldg.y - 0.26f, posForBldg.z + 1 - 0.57f), Quaternion.identity) as Transform;
                    wall.gameObject.name = "Boundary";
                    //wall.GetComponent<Renderer>().material.color = Color.black;
                }
			}
		}
	}

    void AddBuildings()
    {
        for(int i=0; i<NonPrimSet.Count; i++)
        {
            Vector3 posForBldg = NonPrimSet[i].transform.position;
            if (i == NonPrimSet.Count / 3)
            {
                Transform bldg = Instantiate(bldg1, new Vector3(posForBldg.x, posForBldg.y - 0.26f, posForBldg.z - 0.43f), Quaternion.identity) as Transform;
                bldg.parent = buildingCells.transform;
            }
            else if(i == (NonPrimSet.Count * 2) / 3)
            {
                Transform bldg = Instantiate(bldg2, new Vector3(posForBldg.x, posForBldg.y - 0.26f, posForBldg.z - 0.43f), Quaternion.identity) as Transform;
                bldg.parent = buildingCells.transform;
            }
            else if (i == ((NonPrimSet.Count * 3) / 3) - 1)
            {
                Transform bldg = Instantiate(bldg3, new Vector3(posForBldg.x, posForBldg.y - 0.26f, posForBldg.z - 0.43f), Quaternion.identity) as Transform;
                bldg.parent = buildingCells.transform;
            }
            else
            {
                Transform bldg = Instantiate(buildingPrefabs[Random.Range(0, buildingPrefabs.Count)], new Vector3(posForBldg.x, posForBldg.y - 0.26f, posForBldg.z - 0.43f), Quaternion.identity) as Transform;
                bldg.parent = buildingCells.transform;
            }

        }
    }

    void AddPuzzle()
    {
        int count = 0;
        int cellNo = Random.Range(0, PrimSet.Count);
        while (count < 3)
        {
            while (PuzzleCube.Contains(cellNo))
            {
                cellNo = Random.Range(0, PrimSet.Count);
            }
            PuzzleCube.Add(cellNo);
            PrimSet[cellNo].GetComponent<Renderer>().material.color = Color.cyan;
            count++;
        }
        PrimSet[PuzzleCube[0]].GetComponent<Renderer>().material.mainTexture = number1;
        PrimSet[PuzzleCube[0]].gameObject.AddComponent<PuzzleCollider>();
        PrimSet[PuzzleCube[0]].GetComponent<PuzzleCollider>().keyno = 1;
        PrimSet[PuzzleCube[0]].rotation = Quaternion.Euler(0, 0, 180);

        PrimSet[PuzzleCube[1]].GetComponent<Renderer>().material.mainTexture = number2;
        PrimSet[PuzzleCube[1]].gameObject.AddComponent<PuzzleCollider>();
        PrimSet[PuzzleCube[1]].GetComponent<PuzzleCollider>().keyno = 2;
        PrimSet[PuzzleCube[1]].rotation = Quaternion.Euler(0, 0, 180);

        PrimSet[PuzzleCube[2]].GetComponent<Renderer>().material.mainTexture = number3;
        PrimSet[PuzzleCube[2]].gameObject.AddComponent<PuzzleCollider>();
        PrimSet[PuzzleCube[2]].GetComponent<PuzzleCollider>().keyno = 3;
        PrimSet[PuzzleCube[2]].rotation = Quaternion.Euler(0, 0, 180);

    }

    void SpawnPlayer()
	{
		Instantiate(FPSController);
		Camera.main.enabled = false;
		spawned = true;
	}

}
