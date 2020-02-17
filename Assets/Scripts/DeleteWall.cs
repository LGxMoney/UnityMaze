using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteWall : MonoBehaviour
{
    public Vector2 startCoordinates;
    public int sizeToDelete;
    // Start is called before the first frame update
    void Start()
    {
        int sideLength = MapManager.xLength;
        //MazeBlueprints.cellpadding;

        for (int i = (int)startCoordinates.x; i < startCoordinates.x + sizeToDelete; i++)
        {
            for (int j = (int)startCoordinates.y; j < startCoordinates.y + sizeToDelete; j++)
            {
                string cellName = "Cell_" + i + "," + j;
                GameObject cellToDelete = GameObject.Find(cellName);
                //if (cellToDelete != null)
                //{
                Debug.Log("Cell to delete is: " + cellName + " --> ");//+ cellToDelete.ToString());
                //}
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
