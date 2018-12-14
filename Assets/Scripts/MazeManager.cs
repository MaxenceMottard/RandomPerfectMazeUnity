using System.Collections;
using System.Collections.Generic;
using MazeBankCSHARP_Classes;
using UnityEngine;

public class MazeManager : MonoBehaviour
{

	public GameObject CellGameObject;
	public GameObject CaracterGameObject;
	public Material TransparentMaterial;
	
	void Start ()
	{
		int Size = 10;
		Maze maze = new Maze(Size);

		for (int x = 0; x < maze.Size; x++)
		{
			for (int y = 0; y < maze.Size; y++)
			{
				GameObject obj =Instantiate(CellGameObject, new Vector3(x, 0, -y), Quaternion.identity);
				CellManager cell = obj.GetComponent<CellManager>();
				
				cell.SetWall(maze.Table[x,y]);

				if (y == 0)
				{
					cell.TopWall.GetComponent<Renderer>().material = TransparentMaterial;
				} else if (y == Size - 1)
				{
					cell.BottomWall.GetComponent<Renderer>().material = TransparentMaterial;
				}
				if (x == 0)
				{
					cell.LeftWall.GetComponent<Renderer>().material = TransparentMaterial;
				} else if (x == Size -1 )
				{
					cell.RightWall.GetComponent<Renderer>().material = TransparentMaterial;
				}
			}
		}

		Debug.Log("X : " + maze.StartCell.Coord.X + ", Y : " + maze.StartCell.Coord.Y);
		Instantiate(CaracterGameObject, new Vector3(maze.StartCell.Coord.X, 0.55f, -maze.StartCell.Coord.Y),
			Quaternion.identity);
	}
	
	
	// Update is called once per frame
	void Update () {
		
	}
}
