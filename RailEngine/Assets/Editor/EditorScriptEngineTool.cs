using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

public class EditorScriptEngineTool : EditorWindow {

    static ScriptEngine engine;

    int curWaypoint;
    int curFacing;

    static int numPaths;

    string author;

    [MenuItem("Rail Tools/Open Rail Engine")]
	public static void OpenEngineTool()
    {
        if (Selection.activeGameObject != null)
        {
            ScriptEngine engineScript = Selection.activeGameObject.GetComponent<ScriptEngine>();
            if (engineScript != null)
            {
                engine = engineScript;
                if (engine.waypoints == null)
                {
                    engine.waypoints = new List<ScriptWaypoint>();
                    AddWaypoint();
                }

                if (engine.facings == null)
                {
                    engine.facings = new List<ScriptFacings>();
                    AddFacing();
                }



                EditorWindow.GetWindow(typeof(EditorScriptEngineTool), false, "Rail Pathing");
            }
            else
            {
                System.Media.SystemSounds.Exclamation.Play();
                EditorUtility.DisplayDialog("No ScriptEngine detected on selected object.",
                    "Please select a single game object with ScriptEngine and try again.", "Acknowledge");
            }
        }
        else
        {
            System.Media.SystemSounds.Exclamation.Play();
            EditorUtility.DisplayDialog("No game object selected.",
                "Please select a single game object with ScriptEngine and try again.", "Acknowledge");
        }
    }

    void OnGUI()
    {
        //Debug.Log("Comment this line to recompile.");
        if (engine != null)
        {
            if (curWaypoint >= engine.waypoints.Count || curWaypoint < 0)
            {
                if (engine.waypoints.Count == 0)
                {
                    AddWaypoint();
                }
                curWaypoint = 0;
            }

            if (curFacing >= engine.facings.Count || curFacing < 0)
            {
                if (engine.facings.Count == 0)
                {
                    AddFacing();
                }
                curFacing = 0;
            }

            //Display the current waypoint data

            //Display the current facing data

            //Display the waypoint timeline

            //Display the facing timeline

            //Textbox for author name

            //Save button
        }
        else
        {
            Close();
        }
    }

    static void AddWaypoint()
    {
        ScriptWaypoint temp = new ScriptWaypoint();
        temp.moveType = MoveType.WAIT;
        temp.moveTime = 0f;
        engine.waypoints.Add(temp);
    }

    static void AddFacing()
    {
        ScriptFacings temp = new ScriptFacings();
        temp.facingType = FacingType.FREE;
        temp.facingTime = 0f;
        engine.facings.Add(temp);
    }

    void SavePath(string author, string timestamp)
    {
        FileInfo file;
        int fileIndex = 1;
        file = new FileInfo(Application.dataPath + "Resources/Pathing/path" + fileIndex + ".csv");
        while (file.Exists)
        {
            fileIndex++;
            file = new FileInfo(Application.dataPath + "Resources/Pathing/path" + fileIndex + ".csv");
        }

        using (StreamWriter save = new StreamWriter(Application.dataPath + "Resources/Pathing/path" + fileIndex + ".csv"))
        {
            save.WriteLine("/Author : " + author);
            save.WriteLine("/Created : " + timestamp);

            foreach (ScriptWaypoint waypoint in engine.waypoints)
            {
                switch (waypoint.moveType)
                {
                    case MoveType.WAIT:
                        //Create Wait movement file line
                        break;
                    case MoveType.STRAIGHT:
                        //Create Straight movement file line
                        break;
                    case MoveType.BEZIER:
                        //Create Bezier movement file line
                        break;
                    case MoveType.BEZIER2:
                        //Create Bezier2 movement file line
                        break;
                }
            }
        }
    }
}
