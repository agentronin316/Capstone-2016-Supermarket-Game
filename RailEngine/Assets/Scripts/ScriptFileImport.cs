using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public enum MoveParsing
{
    MOVE_TYPE = 1,
    MOVE_TIME = 2,
    END_X = 3,
    END_Y = 4,
    END_Z = 5,
    CURVE_ONE_X = 6,
    CURVE_ONE_Y = 7,
    CURVE_ONE_Z = 8,
    CURVE_TWO_X = 9,
    CURVE_TWO_Y = 10,
    CURVE_TWO_Z = 11
}

public enum FaceParsing
{
    LOOK_TYPE = 1,
    LOOK_TIME = 2,
    TARGET_X = 3,
    TARGET_Y = 4,
    TARGET_Z = 5
}

public static class ScriptFileImport
{
    const int MOVE_OR_FACE = 0;
    public static List<ScriptWaypoint> Waypoints { get; private set;}
    public static List<ScriptFacings> Facings { get; private set;}

    static string pathingFileSavePath = (Application.dataPath + "/Resources/Pathing/");
    //static string pathingFileLoadPath = "/Pathing/";
    static string pathingFileName = "path";
    static string fileType = ".csv";

    //static string characterFileSavePath = (Application.dataPath + "/Resources/Character/");
    //static string characterFileLoadPath = "Character/";
    //static string characterFileName = "character";

    //static string itemFileSavePath = (Application.dataPath + "/Resources/Character/");
    //static string itemFileLoadPath = "Item/";
    //static string itemFileName = "item";


    

    public static bool CheckPath(int fileIndex)
    {
        return File.Exists(pathingFileSavePath + pathingFileName + fileIndex + fileType);
    }

    #region Parse Pathing File
    public static void LoadPath(int fileIndex, GameObject waypointPrefab, out List<ScriptWaypoint> waypoints, out List<ScriptFacings> facings)
    {
        Waypoints = new List<ScriptWaypoint>();
        Facings = new List<ScriptFacings>();

        //Debug.Log(pathingFileLoadPath + pathingFileName + fileIndex + fileType);
        //TextAsset file = Resources.Load(pathingFileName + fileIndex + fileType) as TextAsset;
        //if (file != null)
        //{
            using (StreamReader reader = new StreamReader(pathingFileSavePath + pathingFileName + fileIndex + fileType))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    string[] parts = line.Split(',');
                    switch (parts[MOVE_OR_FACE].ToUpper())
                    {
                        case "MOVE":
                            Waypoints.Add(ParseMove(waypointPrefab, parts));
                            break;
                        case "FACE":
                            Facings.Add(ParseFacing(waypointPrefab, parts));
                            break;
                    }
                line = reader.ReadLine();
                }
        }
        //}
        //else
        //{
        //    Debug.Log("File Not Found.");
        //}

        waypoints = Waypoints;
        facings = Facings;
    }

    static ScriptWaypoint ParseMove(GameObject waypointPrefab, string[] parts)
    {
        float tempX;
        float tempY;
        float tempZ;
        ScriptWaypoint toReturn = new ScriptWaypoint();
        toReturn.moveType = (MoveType)Enum.Parse(typeof(MoveType), parts[(int)MoveParsing.MOVE_TYPE].ToUpper());
        switch (toReturn.moveType)
        {
            case MoveType.WAIT:
                toReturn.moveTime = Convert.ToSingle(parts[(int)MoveParsing.MOVE_TIME]);
                break;
            case MoveType.STRAIGHT:
                tempX = Convert.ToSingle(parts[(int)MoveParsing.END_X]);
                tempY = Convert.ToSingle(parts[(int)MoveParsing.END_Y]);
                tempZ = Convert.ToSingle(parts[(int)MoveParsing.END_Z]);
                toReturn.moveTarget = (GameObject.Instantiate(waypointPrefab, new Vector3(tempX, tempY, tempZ), Quaternion.identity) as GameObject).transform;
                goto case MoveType.WAIT;
            case MoveType.BEZIER:
                tempX = Convert.ToSingle(parts[(int)MoveParsing.CURVE_ONE_X]);
                tempY = Convert.ToSingle(parts[(int)MoveParsing.CURVE_ONE_Y]);
                tempZ = Convert.ToSingle(parts[(int)MoveParsing.CURVE_ONE_Z]);
                toReturn.curvePoint = (GameObject.Instantiate(waypointPrefab, new Vector3(tempX, tempY, tempZ), Quaternion.identity) as GameObject).transform;
                goto case MoveType.STRAIGHT;
            case MoveType.BEZIER2:
                tempX = Convert.ToSingle(parts[(int)MoveParsing.CURVE_TWO_X]);
                tempY = Convert.ToSingle(parts[(int)MoveParsing.CURVE_TWO_Y]);
                tempZ = Convert.ToSingle(parts[(int)MoveParsing.CURVE_TWO_Z]);
                toReturn.curvePoint2 = (GameObject.Instantiate(waypointPrefab, new Vector3(tempX, tempY, tempZ), Quaternion.identity) as GameObject).transform;
                goto case MoveType.BEZIER;
        }
        return toReturn;
    }

    static ScriptFacings ParseFacing(GameObject waypointPrefab, string[] parts)
    {
        float tempX;
        float tempY;
        float tempZ;
        ScriptFacings toReturn = new ScriptFacings();
        toReturn.facingType = (FacingType)Enum.Parse(typeof(FacingType), parts[(int)FaceParsing.LOOK_TYPE].ToUpper());
        switch (toReturn.facingType)
        {
            case FacingType.FREE:
                toReturn.facingTime = Convert.ToSingle(parts[(int)FaceParsing.LOOK_TIME]);
                break;
            case FacingType.DIRECTION_LOCK:
            case FacingType.LOCATION_LOCK:
                tempX = Convert.ToSingle(parts[(int)FaceParsing.TARGET_X]);
                tempY = Convert.ToSingle(parts[(int)FaceParsing.TARGET_Y]);
                tempZ = Convert.ToSingle(parts[(int)FaceParsing.TARGET_Z]);
                toReturn.facingTarget = (GameObject.Instantiate(waypointPrefab, new Vector3(tempX, tempY, tempZ), Quaternion.identity) as GameObject).transform;
                goto case FacingType.FREE;
        }
        return toReturn;
    }
    #endregion

}
