using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// @author Marshall Mason
/// @description Core movement engine with data import and timeline tracking
/// </summary>
public class ScriptEngine : MonoBehaviour {

    

    public List<ScriptWaypoint> waypoints;

    int currentWaypoint = 0;

	void Start ()
    {
	    //Import Waypoint List
        StartCoroutine(MoveEngine());
	}
	
    IEnumerator MoveEngine()
    {
        while (currentWaypoint < waypoints.Count)
        {
            switch (waypoints[currentWaypoint].moveType)
            {
                case MoveType.WAIT:
                    yield return new WaitForSeconds(waypoints[currentWaypoint].moveTime);
                    break;
                case MoveType.STRAIGHT:
                    StartCoroutine(StraightMove(waypoints[currentWaypoint]));
                    yield return new WaitForSeconds(waypoints[currentWaypoint].moveTime);
                    break;
                case MoveType.BEZIER:
                    StartCoroutine(BezierMove(waypoints[currentWaypoint]));
                    yield return new WaitForSeconds(waypoints[currentWaypoint].moveTime);
                    break;
            }
            currentWaypoint++;
        }
    }
    
    IEnumerator StraightMove(ScriptWaypoint curMove)
    {
        float elapsedTime = 0;
        Vector3 startPos = transform.position;
        float moveSpeed = 1 / curMove.moveTime;
        transform.LookAt(curMove.moveTarget);
        while (elapsedTime < curMove.moveTime)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, curMove.moveTarget.position, elapsedTime * moveSpeed);
            yield return null;
        }
    }

    IEnumerator BezierMove(ScriptWaypoint curMove)
    {
        float elapsedTime = 0;
        Vector3 startPos = transform.position;
        float moveSpeed = 1 / curMove.moveTime;
        Vector3 nextPos;
        if (curMove.curvePoint2 == null)
        {
            while (elapsedTime < curMove.moveTime)
            {
                float moveTime = moveSpeed * elapsedTime;
                //Quadratic Bezier Curve
                nextPos = BezierCurve(startPos, curMove.curvePoint.position, curMove.moveTarget.position, moveTime);
                transform.LookAt(nextPos);
                transform.position = nextPos;
                yield return null;
            }
        }
        else
        {
            while (elapsedTime < curMove.moveTime)
            {
                float moveTime = moveSpeed * elapsedTime;
                //Cubic Bezier Curve
                nextPos = (1 - moveTime) * BezierCurve(startPos, curMove.curvePoint.position, curMove.curvePoint2.position, moveTime) +
                               moveTime * BezierCurve(curMove.curvePoint.position, curMove.curvePoint2.position, curMove.moveTarget.position, moveTime);
                transform.LookAt(nextPos);
                transform.position = nextPos;
                yield return null;
            }
        }
    }


    /// <summary>
    /// Returns the value of a quadratic bezier curve
    /// </summary>
    /// <param name="start">the start point for the curve</param>
    /// <param name="control">the control point for the curve</param>
    /// <param name="end">the end point for the curve</param>
    /// <param name="progress">the % along the curve</param>
    /// <returns>The point along the curve at the specified time</returns>
    Vector3 BezierCurve(Vector3 start, Vector3 control, Vector3 end, float progress)
    {
        return ((1 - progress) * (1 - progress) * start + 2 * (1 - progress) * progress * control + progress * progress * end);
    }
}
