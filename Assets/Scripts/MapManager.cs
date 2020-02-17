using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
 

    //Team1
    public static int xLength = 12;
    public static int yLength = 12;
    public static float cellpadding = 5;
    public Vector2 startOffset = new Vector2(xLength * cellpadding, 0);
    
    //public GameObject EnvironmentObject;
    public Node[,] mazeGrid = new Node[xLength, yLength];
    public Transform wallPrefab;
    public Vector2 bigRoomStart = new Vector2(Mathf.Floor(xLength*.25f), Mathf.Floor(yLength *.25f));
    public int bigRoomSize;

    public void Start()
    {
        GenerateMaze();
        //MapManager map1 = new MapManager();
    }


    //MapManager(int length, int cellpadding, )
    //{
    //    xLength
    //}

    public void GenerateMaze()
    {
        CreateAndRelateNodes();

        //printMaze();
        //Debug.Log("--------------------------------------------");

        int startingPoint = UnityEngine.Random.Range(0, xLength);
        RecursiveHunter(new Vector2(startingPoint, 0));
        GenerateWalls();
        DeleteWalls(bigRoomStart, bigRoomSize);
        //GenerateFloorPlane();
    }

    public void CreateAndRelateNodes()
    {
        //Create Nodes
        for (int i = 0; i < xLength; i++)
        {
            for (int j = 0; j < yLength; j++)
            {
                Vector4 nodeWalls = new Vector4(1, 1, 1, 1);
                Vector2 nodeID = new Vector2(i, j);

                Node newNode = new Node(nodeID, nodeWalls);
                mazeGrid[j, i] = newNode;
            }
        }

        //print nodes in order
        for (int i = 0; i < xLength; i++)
        {
            string row = "";
            for (int j = 0; j < yLength; j++)
            {
                row += mazeGrid[i, j].nodeID.x + "," + mazeGrid[i, j].nodeID.y + "--";
            }
            //print(row);
        }

        //Relate Nodes
        for (int i = 0; i < xLength; i++)
        {
            for (int j = 0; j < yLength; j++)
            {
                //Debug.Log("Working on: "+ i + ", " + j);
                //corner checking
                if (i == 0 && j == 0) //top left
                {
                    mazeGrid[i, j].NorthNeighbor = null;
                    mazeGrid[i, j].EastNeighbor = mazeGrid[i + 1, j];
                    mazeGrid[i, j].SouthNeighbor = mazeGrid[i, j + 1];
                    mazeGrid[i, j].WestNeighbor = null;
                }
                else if (i == xLength - 1 && j == 0) //top right
                {
                    mazeGrid[i, j].NorthNeighbor = null;
                    mazeGrid[i, j].EastNeighbor = null;
                    mazeGrid[i, j].SouthNeighbor = mazeGrid[i, j + 1];
                    mazeGrid[i, j].WestNeighbor = mazeGrid[i - 1, j];

                }
                else if (i == 0 && j == yLength - 1) //bottom left
                {
                    mazeGrid[i, j].NorthNeighbor = mazeGrid[i, j - 1];
                    mazeGrid[i, j].EastNeighbor = mazeGrid[i + 1, j];
                    mazeGrid[i, j].SouthNeighbor = null;
                    mazeGrid[i, j].WestNeighbor = null;
                }
                else if (i == xLength - 1 && j == yLength - 1) //bottom right
                {
                    mazeGrid[i, j].NorthNeighbor = mazeGrid[i, j - 1];
                    mazeGrid[i, j].EastNeighbor = null;
                    mazeGrid[i, j].SouthNeighbor = null;
                    mazeGrid[i, j].WestNeighbor = mazeGrid[i - 1, j];
                }
                else //not a corner - check for edges
                {
                    //Debug.Log("    -Not a Corner");
                    if (i == 0)
                    {
                        mazeGrid[i, j].NorthNeighbor = mazeGrid[i, j - 1];
                        mazeGrid[i, j].EastNeighbor = mazeGrid[i + 1, j];
                        mazeGrid[i, j].SouthNeighbor = mazeGrid[i, j + 1];
                        mazeGrid[i, j].WestNeighbor = null;

                    }
                    else if (i == yLength - 1)
                    {
                        mazeGrid[i, j].NorthNeighbor = mazeGrid[i, j - 1];
                        mazeGrid[i, j].EastNeighbor = null;
                        mazeGrid[i, j].SouthNeighbor = mazeGrid[i, j + 1];
                        mazeGrid[i, j].WestNeighbor = mazeGrid[i - 1, j];
                    }
                    else if (j == 0)
                    {
                        mazeGrid[i, j].NorthNeighbor = null;
                        mazeGrid[i, j].EastNeighbor = mazeGrid[i + 1, j];
                        mazeGrid[i, j].SouthNeighbor = mazeGrid[i, j + 1];
                        mazeGrid[i, j].WestNeighbor = mazeGrid[i - 1, j];
                    }
                    else if (j == yLength - 1)
                    {
                        mazeGrid[i, j].NorthNeighbor = mazeGrid[i, j - 1];
                        mazeGrid[i, j].EastNeighbor = mazeGrid[i + 1, j];
                        mazeGrid[i, j].SouthNeighbor = null;
                        mazeGrid[i, j].WestNeighbor = mazeGrid[i - 1, j];
                    }
                    else
                    {
                        mazeGrid[i, j].NorthNeighbor = mazeGrid[i, j - 1];
                        mazeGrid[i, j].EastNeighbor = mazeGrid[i + 1, j];
                        mazeGrid[i, j].SouthNeighbor = mazeGrid[i, j + 1];
                        mazeGrid[i, j].WestNeighbor = mazeGrid[i - 1, j];
                    }

                }

            }
        }
    }
    //}
    //private void GenerateFloorPlane()
    //{
    //    Instantiate(new Plane(new Vector3(xLength*cellpadding, xLength * cellpadding, 0.01f)), new Vector3(0, 0, 0), true);
    //}
    private void GenerateWalls()
    {
        for (int i = 0; i < xLength; i++)
        {
            for (int j = 0; j < yLength; j++)
            {
                Node cellNode = mazeGrid[i, j];
                //Debug.Log("Cell Nodes(" + cellNode.nodeID +" walls are: " + "NWall:" + cellNode.walls.x + " EWall" + cellNode.walls.y + " SWall" +
                //    cellNode.walls.z + " WWall " + cellNode.walls.w);
                string cellName = "Cell_" + i + "," + j;
                GameObject MazeNode = new GameObject(cellName);
                MazeNode.layer = 9;
                                
                if ((int)cellNode.walls.x == 1) //north wall
                {
                    Transform newWall = Instantiate(wallPrefab, new Vector3(startOffset.x + 2 * i * cellpadding, 0 + 5, -2 * j * cellpadding + cellpadding + startOffset.y), Quaternion.identity, MazeNode.transform);
                    string wallName = cellName + "-N_Wall";
                    newWall.name = wallName;
                }
                if ((int)cellNode.walls.y == 1) //east wall
                {
                    Transform newWall = Instantiate(wallPrefab, new Vector3(startOffset.x + 2 * i * cellpadding + cellpadding, 0 + 5, -2 * j * cellpadding + startOffset.y), Quaternion.Euler(0, 90f, 0f), MazeNode.transform);
                    string wallName = cellName + "-E_Wall";
                    newWall.name = wallName;
                }
                if ((int)cellNode.walls.z == 1) //south wall
                {
                    Transform newWall = Instantiate(wallPrefab, new Vector3(startOffset.x + 2 * i * cellpadding, 0 + 5, -2 * j * cellpadding - cellpadding + startOffset.y), Quaternion.identity, MazeNode.transform);
                    string wallName = cellName + "-S_Wall";
                    newWall.name = wallName;
                }
                if ((int)cellNode.walls.w == 1) //west wall
                {
                    Transform newWall = Instantiate(wallPrefab, new Vector3(startOffset.x + 2 * i * cellpadding - cellpadding, 0 + 5, -2 * j * cellpadding + startOffset.y), Quaternion.Euler(0, 90f, 0f), MazeNode.transform);
                    string wallName = cellName + "-W_Wall";
                    newWall.name = wallName;
                }

            }
        }
    }

    private void DeleteWalls(Vector2 startCoordinates, int sizeToDelete)
    {
        int sideLength = MapManager.xLength;
        //MazeBlueprints.cellpadding;

        for (int i = (int)startCoordinates.x; i < startCoordinates.x + sizeToDelete; i++)
        {
            for (int j = (int)startCoordinates.y; j < startCoordinates.y + sizeToDelete; j++)
            {
                string cellName = "Cell_" + i + "," + j;
                string wallName;
                GameObject cellWallToDelete;

                if (i ==(int)startCoordinates.x)
                {
                    wallName = cellName + "-E_Wall";
                    cellWallToDelete = GameObject.Find(wallName);
                    if (cellWallToDelete != null) Destroy(cellWallToDelete);

                    wallName = cellName + "-S_Wall";
                    cellWallToDelete = GameObject.Find(wallName);
                    if (cellWallToDelete != null) Destroy(cellWallToDelete);

                    wallName = cellName + "-N_Wall";
                    cellWallToDelete = GameObject.Find(wallName);
                    if (cellWallToDelete != null) Destroy(cellWallToDelete);
                }
                else if (i == (int)startCoordinates.x + sizeToDelete - 1)
                {
                    wallName = cellName + "-N_Wall";
                    cellWallToDelete = GameObject.Find(wallName);
                    if (cellWallToDelete != null) Destroy(cellWallToDelete);

                    wallName = cellName + "-S_Wall";
                    cellWallToDelete = GameObject.Find(wallName);
                    if (cellWallToDelete != null) Destroy(cellWallToDelete);

                    wallName = cellName + "-W_Wall";
                    cellWallToDelete = GameObject.Find(wallName);
                    if (cellWallToDelete != null) Destroy(cellWallToDelete);
                }
                else if (j == (int)startCoordinates.x)
                {
                    wallName = cellName + "-W_Wall";
                    cellWallToDelete = GameObject.Find(wallName);
                    if (cellWallToDelete != null) Destroy(cellWallToDelete);

                    wallName = cellName + "-E_Wall";
                    cellWallToDelete = GameObject.Find(wallName);
                    if (cellWallToDelete != null) Destroy(cellWallToDelete);

                    wallName = cellName + "-S_Wall";
                    cellWallToDelete = GameObject.Find(wallName);
                    if (cellWallToDelete != null) Destroy(cellWallToDelete);
                }
                else if (j == (int)startCoordinates.x + sizeToDelete - 1)
                {
                    wallName = cellName + "-N_Wall";
                    cellWallToDelete = GameObject.Find(wallName);
                    if (cellWallToDelete != null) Destroy(cellWallToDelete);

                    wallName = cellName + "-E_Wall";
                    cellWallToDelete = GameObject.Find(wallName);
                    if (cellWallToDelete != null) Destroy(cellWallToDelete);

                    wallName = cellName + "-W_Wall";
                    cellWallToDelete = GameObject.Find(wallName);
                    if (cellWallToDelete != null) Destroy(cellWallToDelete);
                }
                else
                {
                    wallName = cellName + "-N_Wall";
                    cellWallToDelete = GameObject.Find(wallName);
                    if (cellWallToDelete != null) Destroy(cellWallToDelete);

                    wallName = cellName + "-E_Wall";
                    cellWallToDelete = GameObject.Find(wallName);
                    if (cellWallToDelete != null) Destroy(cellWallToDelete);

                    wallName = cellName + "-S_Wall";
                    cellWallToDelete = GameObject.Find(wallName);
                    if (cellWallToDelete != null) Destroy(cellWallToDelete);

                    wallName = cellName + "-W_Wall";
                    cellWallToDelete = GameObject.Find(wallName);
                    if (cellWallToDelete != null) Destroy(cellWallToDelete);

                }
            }
        }
    }



    public void RecursiveHunter(Vector2 index)
    {
        mazeGrid[(int)index.x, (int)index.y].visited = true;

        List<string> directions = new List<string> { "North", "East", "South", "West" };
        string direction = null;

        for (int i = 0; i < 4; i++)
        {
            int randomDirection = UnityEngine.Random.Range(0, 4 - i);
            direction = directions[randomDirection];
            if (direction.Equals("North"))
            {
                Node neighborCheck = mazeGrid[(int)index.x, (int)index.y].NorthNeighbor;
                if (neighborCheck != null && neighborCheck.visited == false)
                {
                    //delete shared wall and call from that node
                    neighborCheck.ClearWall("South");
                    RecursiveHunter(new Vector2(index.x, index.y - 1));
                }
                directions.Remove("North");
            }
            if (direction.Equals("East"))
            {
                Node neighborCheck = mazeGrid[(int)index.x, (int)index.y].EastNeighbor;
                if (neighborCheck != null && neighborCheck.visited == false)
                {
                    //delete shared wall and call from that node
                    neighborCheck.ClearWall("West");
                    RecursiveHunter(new Vector2(index.x + 1, index.y));
                }
                directions.Remove("East");
            }
            if (direction == "South")
            {
                Node neighborCheck = mazeGrid[(int)index.x, (int)index.y].SouthNeighbor;
                if (neighborCheck != null && neighborCheck.visited == false)
                {
                    //delete shared wall and call from that node
                    neighborCheck.ClearWall("North");
                    RecursiveHunter(new Vector2(index.x, index.y + 1));
                }
                directions.Remove("South");
            }
            if (direction == "West")
            {
                Node neighborCheck = mazeGrid[(int)index.x, (int)index.y].WestNeighbor;
                if (neighborCheck != null && neighborCheck.visited == false)
                {
                    //delete shared wall and call from that node
                    neighborCheck.ClearWall("East");
                    RecursiveHunter(new Vector2(index.x - 1, index.y));
                }
                directions.Remove("West");
            }
        }
        return;
    }

    public void printMaze()
    {
        for (int i = 0; i < xLength; i++)
        {
            string horizontalBars = "";
            string verticalBars = "";

            for (int j = 0; j < yLength; j++)
            {
                if ((int)mazeGrid[i, j].walls.x == 1)
                {
                    horizontalBars += "__ ";
                }
                else horizontalBars += " x ";

                if ((int)mazeGrid[i, j].walls.w == 1)
                {
                    verticalBars += "|   ";
                }
                else verticalBars += " ";


            }
        }
    }
}


public class Node
{
    public Vector2 nodeID;
    public Vector4 walls;
    public Node NorthNeighbor;
    public Node EastNeighbor;
    public Node SouthNeighbor;
    public Node WestNeighbor;

    public bool visited;


    public int ClearWall(string wall)
    {
        if (wall == "North")
        {
            walls.x = 0;
            if (NorthNeighbor != null)
            {
                if ((int)NorthNeighbor.walls.z == 1)
                {
                    NorthNeighbor.walls.z = 0;
                    return 1;
                }
            }
            else return 0;
        }
        if (wall == "East")
        {
            walls.y = 0;
            if (EastNeighbor != null)
            {
                if ((int)EastNeighbor.walls.w == 1)
                {
                    EastNeighbor.walls.w = 0;
                    return 1;
                }
            }
            else return 0;
        }
        if (wall == "South")
        {
            walls.z = 0;
            if (SouthNeighbor != null)
            {
                if ((int)SouthNeighbor.walls.x == 1)
                {
                    SouthNeighbor.walls.x = 0;
                    return 1;
                }
            }
            else return 0;
        }
        if (wall == "West")
        {
            walls.w = 0;
            if (WestNeighbor != null)
            {
                if ((int)WestNeighbor.walls.y == 1)
                {
                    WestNeighbor.walls.y = 0;
                    return 1;
                }
            }
            else return 0;
        }
        return 0;
    }
    /**
     * Empty Node
     */
    public Node(Vector2 nodeID)
    {
        this.nodeID = nodeID;
        walls = new Vector4(0f, 0f, 0f, 0f);
        NorthNeighbor = null;
        EastNeighbor = null;
        SouthNeighbor = null;
        WestNeighbor = null;
    }

    /**
    * Just walls
    */
    public Node(Vector2 nodeID, Vector4 walls)
    {
        this.nodeID = nodeID;
        this.walls.x = walls.x;
        this.walls.y = walls.y;
        this.walls.z = walls.z;
        this.walls.w = walls.w;
        NorthNeighbor = null;
        EastNeighbor = null;
        SouthNeighbor = null;
        WestNeighbor = null;
    }

    /**
     * Just neighbors
     */
    public Node(Vector2 nodeID, Node nn, Node en, Node sn, Node wn)
    {
        this.nodeID = nodeID;
        NorthNeighbor = nn;
        EastNeighbor = en;
        SouthNeighbor = sn;
        WestNeighbor = wn;
    }

    /**
     * Neighbors and Walls
     */
    public Node(Vector2 nodeID, Vector4 walls, Node nn, Node en, Node sn, Node wn)
    {
        this.nodeID = nodeID;
        this.walls.x = walls.x;
        this.walls.y = walls.y;
        this.walls.z = walls.z;
        this.walls.w = walls.w;
        NorthNeighbor = nn;
        EastNeighbor = en;
        SouthNeighbor = sn;
        WestNeighbor = wn;
    }

    //public string ToString()
    //{
    //    return walls.ToString();


    //}
}



//public void IterativeHunter(Vector2 index)
//{
//    List<Node> hunterStack = new List<Node>();
//    Node currentNode = mazeGrid[(int)index.x, (int)index.y];
//    currentNode.visited = true;
//    hunterStack.Add(currentNode);

//    //do
//    for (int test = 0; test < 8; test++)
//    {
//        string hunterStackstr = "";
//        for (int i = 0; i < hunterStack.Count; i++)
//        {
//            hunterStackstr += hunterStack[i].nodeID + ", ";
//        }
//        Debug.Log("Hunter Stack is: " + hunterStackstr);
//        Debug.Log("---CurrentNode: " + currentNode.nodeID);

//        //currentNode = mazeGrid[(int)index.x, (int)index.y];
//        currentNode = currentNode.HasUnvisitedNeighbor();

//        if (currentNode != null)
//        {
//            Debug.Log("---New CurrentNode (neighbor of previous): " + currentNode.nodeID);
//            Debug.Log("---Adding to Stack");
//            hunterStack.Add(currentNode);
//            string direction = DetermineDirection(hunterStack);
//            Debug.Log("---Direction backwards is: " + direction);
//            currentNode.ClearWall(direction);
//            currentNode.visited = true;
//        }
//        else
//        {
//            Debug.Log("---Couldn't find it");
//            Debug.Log("---Popping hunter stack");
//            hunterStack.RemoveAt(hunterStack.Count - 1);
//            currentNode = hunterStack[hunterStack.Count - 2];

//        }
//    }
//    //while (hunterStack.Count > 1);


//}



//private string DetermineDirection(List<Node> hunterStack)
//{
//    Debug.Log("------Finding Direction");
//    Node lastNode = hunterStack[hunterStack.Count - 1];
//    Debug.Log("------Last Node is: " + lastNode.nodeID);
//    if (hunterStack.Count == 0)
//        return null;

//    Node SecondToLastNode = hunterStack[hunterStack.Count - 2];
//    Debug.Log("------Second to Last Node is: " + SecondToLastNode.nodeID);

//    int xSign = (int)(lastNode.nodeID.x - SecondToLastNode.nodeID.x);
//    int ySign = (int)(lastNode.nodeID.y - SecondToLastNode.nodeID.y);
//    Debug.Log("-----Signs are: " + xSign + ySign);
//    if (xSign  == 0 && ySign == 1)
//    {
//        return "North";
//    }
//    else if (xSign == -1 && ySign == 0)
//    {
//        return "East";
//    }
//    else if (xSign == 0 && ySign == -1)
//    {
//        return "South";
//    }
//    else if (xSign == 1 && ySign == 0)
//    {
//        return "West";
//    }
//    return null;

//}


//In Node
//public Node HasUnvisitedNeighbor()
//{
//    Debug.Log("------In HasUnvisitedNeighbor of " + nodeID);
//    //Debug.Log("-----All Neighbors are(NESW): " + NorthNeighbor.nodeID + ", " + EastNeighbor.nodeID + ", " + SouthNeighbor.nodeID + ", " + WestNeighbor.nodeID);
//    List<Node> goodNeighbors = new List<Node>();
//    if (NorthNeighbor != null && NorthNeighbor.visited == false)
//        goodNeighbors.Add(NorthNeighbor);
//    if (EastNeighbor != null && EastNeighbor.visited == false)
//        goodNeighbors.Add(EastNeighbor);
//    if (SouthNeighbor != null && SouthNeighbor.visited == false)
//        goodNeighbors.Add(SouthNeighbor);
//    if (WestNeighbor != null && WestNeighbor.visited == false)
//        goodNeighbors.Add(SouthNeighbor);

//    string nstr = "";
//    //foreach(Node neighbor in goodNeighbors)
//    //{
//    //    nstr += neighbor.nodeID + ", ";
//    //}
//    //Debug.Log("-------Goodneighbors are: " + nstr);
//    if (goodNeighbors.Count > 0)
//        return goodNeighbors[UnityEngine.Random.Range(0, goodNeighbors.Count - 1)];
//    else
//        return null;
//}


//Debug.Log("Maze Grid at: " + mazeGrid[i, j].nodeID + "'s neighbors are: ");

//try {Debug.Log(" ---- " + mazeGrid[i, j].NorthNeighbor.nodeID);}
//catch {Debug.Log(" ---- North Not found");}

//try { Debug.Log(" ---- " + mazeGrid[i, j].EastNeighbor.nodeID); }
//catch { Debug.Log(" ---- East Not found"); }

//try{Debug.Log(" ---- " + mazeGrid[i, j].SouthNeighbor.nodeID);}
//catch { Debug.Log(" ---- South Not found"); }

//try{ Debug.Log(" ---- " + mazeGrid[i, j].WestNeighbor.nodeID); }
//catch { Debug.Log(" ---- West Not found"); }