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

    static string fileSavePath;
    static string fileLoadPath;
    static string fileName;
    static string fileType;
    
    public static void Initialize()
    {
        fileName = "path";
        fileType = ".csv";
        fileSavePath = (Application.dataPath + "/Resources/Pathing/");
        fileLoadPath = "Pathing/";
    }

    public static bool CheckPath(int fileIndex)
    {
        return File.Exists(fileSavePath + fileName + fileIndex + fileType);
    }

    #region Parse File
    public static void LoadPath(int fileIndex, out List<ScriptWaypoint> waypoints, out List<ScriptFacings> facings)
    {
        Waypoints = new List<ScriptWaypoint>();
        Facings = new List<ScriptFacings>();

        TextAsset file = Resources.Load(fileLoadPath + fileName + fileIndex + fileType) as TextAsset;
        if (file != null)
        {
            using (StreamReader reader = new StreamReader(file.text))
            {
                reader.ReadLine();
                reader.ReadLine();
                string line = reader.ReadLine();
                while (line != null)
                {
                    string[] parts = line.Split(',');
                    switch (parts[MOVE_OR_FACE].ToUpper())
                    {
                        case "MOVE":
                            Waypoints.Add(ParseMove(parts));
                            break;
                        case "FACE":
                            Facings.Add(ParseFacing(parts));
                            break;
                    }
                }
            }
        }

        waypoints = Waypoints;
        facings = Facings;
    }

    static ScriptWaypoint ParseMove(string[] parts)
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
                toReturn.moveTarget.position = new Vector3(tempX, tempY, tempZ);
                goto case MoveType.WAIT;
            case MoveType.BEZIER:
                tempX = Convert.ToSingle(parts[(int)MoveParsing.CURVE_ONE_X]);
                tempY = Convert.ToSingle(parts[(int)MoveParsing.CURVE_ONE_Y]);
                tempZ = Convert.ToSingle(parts[(int)MoveParsing.CURVE_ONE_Z]);
                toReturn.curvePoint.position = new Vector3(tempX, tempY, tempZ);
                goto case MoveType.STRAIGHT;
            case MoveType.BEZIER2:
                tempX = Convert.ToSingle(parts[(int)MoveParsing.CURVE_TWO_X]);
                tempY = Convert.ToSingle(parts[(int)MoveParsing.CURVE_TWO_Y]);
                tempZ = Convert.ToSingle(parts[(int)MoveParsing.CURVE_TWO_Z]);
                toReturn.curvePoint2.position = new Vector3(tempX, tempY, tempZ);
                goto case MoveType.BEZIER;
        }
        return toReturn;
    }

    static ScriptFacings ParseFacing(string[] parts)
    {
        float tempX;
        float tempY;
        float tempZ;
        ScriptFacings toReturn = new ScriptFacings();
        toReturn.facingType = (FacingType)Enum.Parse(typeof(FacingType), parts[(int)FaceParsing.LOOK_TYPE]);
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
                toReturn.facingTarget.position = new Vector3(tempX, tempY, tempZ);
                goto case FacingType.FREE;
        }
        return toReturn;
    }
    #endregion

}
