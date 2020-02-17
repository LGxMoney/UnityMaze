//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class MazeGenerator : MonoBehaviour
//{
//    public int cellSize = 30;
//    public static int length = 10;
//    public static int height = 10;
//    public Transform parentObj;
//    public Vector3 startPos = new Vector3(0f, 10f, 0f);

//    public Transform wallPrefab;
//    public Transform doorPrefab;

//    public int teams = 2;

//    public string[,] mazeGrid = new string[length, height];
//    // Start is called before the first frame update
//    void Start()
//    {
//        //InitializePerimeter(mazeGrid);
//        //ToString(mazeGrid);

//        //GameObject team = new GameObject("Team 1");
//        //GenerateSpawnPoint(team.name, "West");

//        for(int i = 0; i < teams; i++)
//        {
//            //int j = 2;
//            Team team = new Team("Team " + i);
//        }


//        //Vector2 start = new Vector2(0*cellSize, 0*cellSize);
//        //Vector4 cross1 = new Vector4(0,0,1,1);
//        ////generateCross(start, cross1);
//        //GenerateRectangle(start, new Vector2(2, 4));



//        //ToString(mazeGrid);

//    }

//    public class Team()
//    {
//        public int teamSize;
//        public string color;
//        public string element;
        
//    }

//    public class Maze
//    {
//        public int cellSize = 30;
//        public static int length = 10;
//        public static int height = 10;
//        public Transform parentObj;
//        public Vector3 startPos = new Vector3(0f, 10f, 0f);

//        public Transform wallPrefab;
//        public Transform doorPrefab;

//        public string[,] mazeGrid = new string[length, height];

//    }

//    private void generateCross(Vector2 startCoordinates, Vector4 wallVectors)
//    {
//        /*
//         * wallVectors.x --> North
//         * wallVectors.y --> East
//         * wallVectors.z --> South
//         * wallVectors.w --> West
//         */
//        //Generate WestWalls
//        if ((int)wallVectors.w != 0)
//        {
//            GameObject WWall = new GameObject("WestWall");
//            for (int i = 0; i < wallVectors.w; i++)
//            {
//                Transform newWall = Instantiate(wallPrefab, new Vector3(startCoordinates.x - cellSize * i - (cellSize), startPos.y, startCoordinates.y), Quaternion.identity, WWall.transform);
//                string wallCoordinates = "Westward_(" + i;
//                newWall.name = wallCoordinates;
//            }
//        }
//        //Generate NorthWalls
//        if ((int)wallVectors.x != 0)
//        {
//            GameObject NorthWall = new GameObject("NorthWall");
//            for (int i = 0; i < wallVectors.x; i++)
//            {
//                Transform newWall = Instantiate(wallPrefab, new Vector3(startCoordinates.x - (cellSize / 2), startPos.y, startCoordinates.y + (cellSize / 2) + (i * cellSize)), Quaternion.Euler(0, 90f, 0f), NorthWall.transform);
//                string wallCoordinates = "Northward_" + i;
//                newWall.name = wallCoordinates;
//            }
//        }
//        //Generate EastWalls
//        if((int)wallVectors.y != 0)
//        {
//            GameObject EastWall = new GameObject("EastWall");
//            for (int i = 0; i < wallVectors.y; i++)
//            {
//                Transform newWall = Instantiate(wallPrefab, new Vector3(startCoordinates.x + (cellSize*i), startPos.y, startCoordinates.y), Quaternion.identity, EastWall.transform);
//                string wallCoordinates = "Eastward_" + i;
//                newWall.name = wallCoordinates;
//            }
//        }
//        //Generate South Walls
//        if ((int)wallVectors.z != 0)
//        {
//            GameObject SouthWall = new GameObject("SouthWall");
//            for (int i = 0; i < wallVectors.z; i++)
//            {
//                Transform newWall = Instantiate(wallPrefab, new Vector3(startCoordinates.x - (cellSize / 2), startPos.y, (startCoordinates.y - (cellSize / 2) - (i * cellSize))), Quaternion.Euler(0, 90f, 0f), SouthWall.transform);
//                string wallCoordinates = "Southward_" + i;
//                newWall.name = wallCoordinates;
//            }
//        }
//    }

//    private void GenerateRectangle(Vector2 startCoordinates, Vector2 dimensions)
//    {
//        /*
//         * dimensions.x = length
//         * dimensions.y = width
//         */

//        Vector4 bottomLeftCorner = new Vector4(dimensions.x, dimensions.y, 0f, 0f);
//        generateCross(startCoordinates, bottomLeftCorner);


//        Vector4 topRightCorner = new Vector4(0f, 0f, dimensions.x, dimensions.y);
//        generateCross(new Vector2(startCoordinates.x + cellSize * dimensions.y, startCoordinates.y + cellSize * dimensions.x), topRightCorner);
//    }
//    private void GenerateSpawnPoint(string team, string wallSide)
//    {
// //--------------------//--------------------//--------------------//--------------------//--------------------
//        //if (wallSide == "West")
//        //{
//        //    int randStart = UnityEngine.Random.Range(0, height - 1);
//        //    //generateCross(0,randStart);
//        //}

//        ////generate random startPoint for left Team Bottom Wall
//        //int teamOneSouthSpawnWall = UnityEngine.Random.Range(0, height - 1);
//        //teamOneSouthSpawnWall = 0;
//        //int teamOneNorthSpawnWall = teamOneSouthSpawnWall + 1;

//        //if (teamOneSouthSpawnWall == 0)
//        //{
//        //    int doorDirection = UnityEngine.Random.Range(0, 2);
//        //    int entranceDirection = UnityEngine.Random.Range(0, 2);
//        //    entranceDirection = 1;
//        //    Debug.Log("doorDirection: " + doorDirection);
//        //    Debug.Log("entranceDirection: " + entranceDirection);

//        //    //North Door - make north prefab door if direction 0
//        //    if (doorDirection == 0)
//        //    {
//        //        //Debug.Log("Creating North Door");
//        //        Transform teamOneNorthWall = Instantiate(doorPrefab, new Vector3(startPos.x, startPos.y, (teamOneNorthSpawnWall * cellSize)), Quaternion.identity, parentObj);
//        //        Transform teamOneEastWall = Instantiate(wallPrefab, new Vector3(startPos.x + cellSize / 2, startPos.y, (teamOneSouthSpawnWall * cellSize) + cellSize / 2), Quaternion.Euler(0, 90f, 0f), parentObj);
//        //        //Transform teamOneEastWall = Instantiate(prefab, new Vector3(startPos.x, startPos.y, (leftWall2Start * cellSize)), Quaternion.identity, parentObj);
//        //        //teamOneNorthWall.name = ";
//        //        //teamOneEastWall = "T1_E"
//        //    }
//        //    else //East Door - make 
//        //    {
//        //        //Debug.Log("Creating East Door");
//        //        Transform teamOneNorthWall = Instantiate(wallPrefab, new Vector3(startPos.x, startPos.y, (teamOneNorthSpawnWall * cellSize)), Quaternion.identity, parentObj);
//        //        Transform teamOneEastWall = Instantiate(doorPrefab, new Vector3(startPos.x + cellSize / 2, startPos.y, (teamOneSouthSpawnWall * cellSize) + cellSize / 2), Quaternion.Euler(0, 90f, 0f), parentObj);
//        //    }
//        //    if (entranceDirection == 0)
//        //    {
//        //        //Debug.Log("Destroying West Door");
//        //        string str = "W_(0, " + teamOneSouthSpawnWall + ")";
//        //        GameObject teamOneWestSpawnWall = GameObject.Find(str);
//        //        Destroy(teamOneWestSpawnWall);
//        //    }
//        //    else //south
//        //    {
//        //        //Debug.Log("Destroying South Door");
//        //        string str = "N_(0, 0)";
//        //        GameObject clearSpawnWall = GameObject.Find(str);
//        //        Destroy(clearSpawnWall);
//        //    }

//        //}

//        ////Top left Corner
//        //if (teamOneSouthSpawnWall == height-1)
//        //{
//        //    int doorDirection = UnityEngine.Random.Range(0, 2);
//        //    int entranceDirection = UnityEngine.Random.Range(0, 2);
            
//        //    Debug.Log("doorDirection: " + doorDirection);
//        //    Debug.Log("entranceDirection: " + entranceDirection);

//        //    //South Door - make north prefab door if direction 0
//        //    if (doorDirection == 0)
//        //    {
//        //        Debug.Log("Creating North Door");
//        //        Transform teamOneSouthWall = Instantiate(doorPrefab, new Vector3(startPos.x, startPos.y, (teamOneNorthSpawnWall * cellSize)), Quaternion.identity, parentObj);
//        //        Transform teamOneEastWall = Instantiate(wallPrefab, new Vector3(startPos.x + cellSize / 2, startPos.y, (teamOneSouthSpawnWall * cellSize) + cellSize / 2), Quaternion.Euler(0, 90f, 0f), parentObj);
//        //        //Transform teamOneEastWall = Instantiate(prefab, new Vector3(startPos.x, startPos.y, (leftWall2Start * cellSize)), Quaternion.identity, parentObj);
//        //        //teamOneNorthWall.name = ";
//        //        //teamOneEastWall = "T1_E"
//        //    }
//        //    else //East Door - make 
//        //    {
//        //        //Debug.Log("Creating East Door");
//        //        Transform teamOneNorthWall = Instantiate(wallPrefab, new Vector3(startPos.x, startPos.y, (teamOneNorthSpawnWall * cellSize)), Quaternion.identity, parentObj);
//        //        Transform teamOneEastWall = Instantiate(doorPrefab, new Vector3(startPos.x + cellSize / 2, startPos.y, (teamOneSouthSpawnWall * cellSize) + cellSize / 2), Quaternion.Euler(0, 90f, 0f), parentObj);
//        //    }
//        //    if (entranceDirection == 0)
//        //    {
//        //        //Debug.Log("Destroying West Door");
//        //        string str = "W_(0, " + teamOneSouthSpawnWall + ")";
//        //        GameObject teamOneWestSpawnWall = GameObject.Find(str);
//        //        Destroy(teamOneWestSpawnWall);
//        //    }
//        //    else //south
//        //    {
//        //        //Debug.Log("Destroying South Door");
//        //        string str = "N_(0, 0)";
//        //        GameObject clearSpawnWall = GameObject.Find(str);
//        //        Destroy(clearSpawnWall);
//        //    }
//        //}
////--------------------//--------------------//--------------------//--------------------//--------------------//--------------------
//        //if (teamOneSouthSpawnWall != 0 || teamOneSouthSpawnWall != height - 1)
//        //{
//        //    leftWall2Start = teamOneSouthSpawnWall + 1;
//        //}

//        //Transform leftWall1 = Instantiate(prefab, new Vector3(startPos.x, startPos.y, (teamOneSouthSpawnWall * cellSize)), Quaternion.identity, parentObj);
//        //Transform leftWall2 = Instantiate(prefab, new Vector3(startPos.x, startPos.y, (leftWall2Start * cellSize)), Quaternion.identity, parentObj);
//        //Transform leftWall3 = Instantiate(prefab, new Vector3(startPos.x+cellSize/2, startPos.y, (teamOneSouthSpawnWall * cellSize) + cellSize/2), Quaternion.Euler(0, 90f, 0f), parentObj);
//        //Transform leftWall3 = Instantiate(prefab, new Vector3(mazeGrid[leftSpawn * cellSize, , startPos.y, startPos.z), Quaternion.identity, parentObj);
//        //int rightSpawnStart = UnityEngine.Random.Range(1, height - 1);

//    }

//    public void InitializePerimeter(string[,] mazeGrid)
//    {
//        //initialize the top row to the correct letters
//        //t = top, tl = top-left, tr = top-right
//        for (int i = 0; i < length; i++)
//        {
//            string topStr = "t";
//            if (i == 0)
//            {
//                topStr = "tl";
//            }
//            if (i == length - 1)
//            {
//                topStr = "tr";
//            }
//            mazeGrid[0, i] = topStr;
//        }

//        //initialize the sides of the matrix
//        for (int i = 1; i < height - 1; i++)
//        {
//            string leftStr = "l";
//            string rightStr = "r";
//            mazeGrid[i, 0] = leftStr;
//            mazeGrid[i, length - 1] = rightStr;
//        }

//        //initialize the middle of the matrix
//        for (int i = 1; i < height - 1; i++)
//        {
//            for (int j = 1; j < length - 1; j++)
//            {
//                mazeGrid[i, j] = "e";
//            }
//        }

//        //initialize the bottom of the array
//        for (int i = 0; i < length; i++)
//        {
//            string bottomStr = "b";
//            if (i == 0)
//            {
//                bottomStr = "bl";
//            }
//            if (i == length - 1)
//            {
//                bottomStr = "br";
//            }
//            mazeGrid[height - 1, i] = bottomStr;
//        }

//        for (int i = 0; i < length; i++)
//        {
//            for (int j = 0; j < height; j++)
//            {
//                //Debug.Log("J is: " + j);
//                string gridVal = mazeGrid[i, j];
//                //Debug.Log(gridVal);
//                if (gridVal.Contains("t"))
//                {
//                    Transform newWall = Instantiate(wallPrefab, new Vector3(j * cellSize, startPos.y, startPos.z), Quaternion.identity, parentObj);
//                    string wallCoordinates = "N_(" + i + ", " + j + ")";
//                    newWall.name = wallCoordinates;
//                }
//                if (gridVal.Contains("l"))
//                {
//                    Transform newWall = Instantiate(wallPrefab, new Vector3(startPos.x - (cellSize / 2), startPos.y, (cellSize / 2) + i * cellSize), Quaternion.Euler(0, 90f, 0f), parentObj);
//                    string wallCoordinates = "W_(" + i + ", " + j + ")";
//                    newWall.name = wallCoordinates;
//                }
//                if (gridVal.Contains("r"))
//                {
//                    //float rightSideStart = startPos.x + cellSize * length;
//                    Transform newWall = Instantiate(wallPrefab, new Vector3(startPos.x - (cellSize / 2) + (cellSize * length), startPos.y, (cellSize / 2) + cellSize * i), Quaternion.Euler(0f, 90f, 0f), parentObj);
//                    string wallCoordinates = "E_(" + i + ", " + j + ")";
//                    newWall.name = wallCoordinates;
//                }
//                if (gridVal.Contains("b"))
//                {
//                    Transform newWall = Instantiate(wallPrefab, new Vector3(j * cellSize, startPos.y, startPos.z + (cellSize * height)), Quaternion.identity, parentObj);
//                    string wallCoordinates = "S_(" + i + ", " + j + ")";
//                    newWall.name = wallCoordinates;
//                }
//            }
//        }

//    }

//    private static void ToString(string[,] mazeGrid)
//    {
//        for (int i = 0; i < length; i++)
//        {
//            string rowString = "";
//            for (int j = 0; j < height; j++)
//            {
//                if (string.IsNullOrEmpty(mazeGrid[i, j]))
//                    rowString += "e";
//                else
//                    rowString += mazeGrid[i, j];
//                rowString += ", ";
//            }
//            Debug.Log(rowString);
//        }
//    }
//}
