using System.Collections;
using System.Collections.Generic;
using MazeBankCSHARP_Classes;
using UnityEngine;

public class CellManager : MonoBehaviour
{

	public GameObject TopWall;
	public GameObject RightWall;
	public GameObject BottomWall;
	public GameObject LeftWall;

	public void SetWall(Cell cell)
	{
		if (!cell.Top)
		{
			Destroy(TopWall);
		}
		if (!cell.Right)
		{
			Destroy(RightWall);
		}
		if (!cell.Left)
		{
			Destroy(LeftWall);
		}
		if (!cell.Bottom)
		{
			Destroy(BottomWall);
		}
	}
	
}
