using UnityEngine;
using System.Collections;
using UnityEditor;

public class EditorScriptEngineTool : EditorWindow {

    static ScriptEngine engine;

    int curWaypoint;
    int curFacing;


    [MenuItem("Rail Tools/Open Rail Engine")]
	public static void OpenEngineTool()
    {
        if (Selection.activeGameObject != null)
        {
            ScriptEngine engineScript = Selection.activeGameObject.GetComponent<ScriptEngine>();
            if (engineScript != null)
            {
                engine = engineScript;
                
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

    }

    void AddWaypoint()
    {
        ScriptWaypoint temp = new ScriptWaypoint();
        temp.moveType = MoveType.WAIT;
        temp.moveTime = 0f;
        engine.waypoints.Add(temp);
    }

    void AddFacing()
    {
        ScriptFacings temp = new ScriptFacings();
        temp.facingType = FacingType.FREE;
        temp.facingTime = 0f;
        engine.facings.Add(temp);
    }
}
