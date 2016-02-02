using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// @author Marshall Mason
/// @description Core movement engine with data import and timeline tracking
/// </summary>
public class ScriptEngine : MonoBehaviour {

    public List<ScriptWaypoint> waypoints;

	void Start ()
    {
	    //Import Waypoint List
        //StartCoroutine(MoveEngine);
	}
	
    IEnumerator MoveEngine()
    {
        int currentWaypoint = 0;
        while (currentWaypoint < waypoints.Count)
        {
            switch (waypoints[currentWaypoint].moveType)
            {
                case MoveType.WAIT:
                    break;
                case MoveType.STRAIGHT:
                    //StartCoroutine(StraightMove);
                    break;
                case MoveType.BEZIER:
                    //StartCoroutine(BezierMove);
                    break;
            }
            yield return new WaitForSeconds(waypoints[currentWaypoint].moveTime);
            currentWaypoint++;
        }
    }
    
}
