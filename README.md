Overview :
This project is about generating a 3D Maze procedurally. Maze consists of a puzzle. A player can wander inside this maze. Based on player’s movements, certain items can be picked up which can be used to unlock respective doors.

Technical Details:

Maze generation is based on Prim’s algorithm which is a greedy algorithm that finds a minimum spanning tree for a weighted undirected graph.

A Maze grid is generated of size m x n.
Each cell in this grid is a vertex in terms of minimal spanning tree.
Each cell/vertex is supposed to have a random weight.
Prim’s Algorithm would then create a set of Vertices/cells that would be used as a path to determine correct path in the Maze.
Rest of the cells that do not belong to this set would then be turned into buildings which creates a city like architecture.
Each Maze is supposed to have 3 doors with 3 locations in the path that gives a player the keys to open the door.
Once all the doors have been opened, player goes to the final cell. Once this is done, new Maze should be generated. This project only deals with one Maze. 
