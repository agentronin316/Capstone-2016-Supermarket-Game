using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// @author Marshall Mason
/// @description Core movement engine with data import and timeline tracking
/// </summary>
public class ScriptEngine : MonoBehaviour {

    public GameObject waypointPrefab;
    public ScriptCharacterData playerCharacter;
    public List<ScriptWaypoint> waypoints = new List<ScriptWaypoint>();
    public List<ScriptFacings> facings = new List<ScriptFacings>();
    public float trackingSpeed = 5f;
    int currentWaypoint = 0;
    int currentFacing = 0;

    public bool lookChange = true;
    public bool freeLook = true;
    Transform mainCamera;


	void Start ()
    {
        mainCamera = Camera.main.transform;
        //Simple test index for demo purposes until menus are implemented
        ScriptFileImport.LoadPath(1, waypointPrefab, out waypoints, out facings);

        //Actual production methodology
        waypoints = ScriptFileImport.Waypoints;
        facings = ScriptFileImport.Facings;

        //Gentlemen, Start your Engines!
        StartCoroutine(MoveEngine());
        StartCoroutine(CameraEngine());
	}

    

    #region Movement
    IEnumerator MoveEngine()
    {
        while (currentWaypoint < waypoints.Count)
        {
            switch (waypoints[currentWaypoint].moveType)
            {
                case MoveType.WAIT:
                    Debug.Log("waiting for " + waypoints[currentWaypoint].moveTime);
                    yield return new WaitForSeconds(waypoints[currentWaypoint].moveTime);
                    break;
                case MoveType.STRAIGHT:
                    Debug.Log("Moving straight for " + waypoints[currentWaypoint].moveTime);
                    StartCoroutine(StraightMove(waypoints[currentWaypoint]));
                    yield return new WaitForSeconds(waypoints[currentWaypoint].moveTime);
                    break;
                case MoveType.BEZIER:
                case MoveType.BEZIER2:
                    Debug.Log("Bezier move for " + waypoints[currentWaypoint].moveTime);
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
        //Debug.Log(curMove.moveTarget);
        //Debug.Log(curMove.curvePoint);
        //Debug.Log(curMove.curvePoint2);

        float elapsedTime = 0;
        Vector3 startPos = transform.position;
        float moveSpeed = 1 / curMove.moveTime;
        Vector3 nextPos;
        if (curMove.moveType == MoveType.BEZIER)
        {
            while (elapsedTime < curMove.moveTime)
            {
                float moveTime = moveSpeed * elapsedTime;
                //Quadratic Bezier Curve
                nextPos = BezierCurve(startPos, curMove.curvePoint.position, curMove.moveTarget.position, moveTime);
                transform.LookAt(nextPos);
                transform.position = nextPos;
                elapsedTime += Time.deltaTime;
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
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }


    /// <summary>
    /// Returns a position on a quadratic bezier curve
    /// </summary>
    /// <param name="start">the start point for the curve</param>
    /// <param name="control">the control point for the curve</param>
    /// <param name="end">the end point for the curve</param>
    /// <param name="progress">the % along the curve</param>
    /// <returns>The point along the curve at the specified time</returns>
    Vector3 BezierCurve(Vector3 start, Vector3 control, Vector3 end, float progress)
    {
        return ((1 - progress) * (1 - progress) * start + 2 * (1 - progress) * progress * control + progress * progress * end);
        //return Vector3.Lerp(Vector3.Lerp(start, control, progress), Vector3.Lerp(control, end, progress), progress);
    }
    #endregion


    #region Facing
    IEnumerator CameraEngine()
    {
        while (currentFacing < facings.Count)
        {
            switch (facings[currentFacing].facingType)
            {
                case FacingType.FREE:
                    if (facings[currentFacing].facingTime > 0)
                    {
                        if (!freeLook)
                        {
                            lookChange = true;
                        }
                        freeLook = true;
                        yield return new WaitForSeconds(facings[currentFacing].facingTime);
                    }
                    break;
                case FacingType.DIRECTION_LOCK:
                    if (facings[currentFacing].facingTarget != null && facings[currentFacing].facingTime > 0)
                    {
                        if (freeLook)
                        {
                            lookChange = true;
                        }
                        freeLook = false;
                        StartCoroutine(DirectionLock(facings[currentFacing]));
                        yield return new WaitForSeconds(facings[currentFacing].facingTime);
                    }
                    break;
                case FacingType.LOCATION_LOCK:
                    if (facings[currentFacing].facingTarget != null && facings[currentFacing].facingTime > 0)
                    {
                        if (freeLook)
                        {
                            lookChange = true;
                        }
                        freeLook = false;
                        StartCoroutine(LocationLock(facings[currentFacing]));
                        yield return new WaitForSeconds(facings[currentFacing].facingTime);
                    }
                    break;
            }
            currentFacing++;
        }
        freeLook = true;
    }

    IEnumerator DirectionLock(ScriptFacings facing)
    {
        float timeElapsed = 0;
        float lookSpeed = trackingSpeed / facing.facingTime;
        Quaternion camRotation = mainCamera.rotation;
        mainCamera.LookAt(facing.facingTarget);
        Quaternion lookDirection = mainCamera.rotation;

        while (timeElapsed < facing.facingTime)
        {
            timeElapsed += Time.deltaTime;
            mainCamera.rotation = Quaternion.Lerp(camRotation, lookDirection, timeElapsed * lookSpeed);
            yield return null;
        }
    }

    IEnumerator LocationLock(ScriptFacings facing)
    {
        Debug.Log("Locking camera to track " + facing.facingTarget.position);
        float timeElapsed = 0;
        float lookSpeed = trackingSpeed / facing.facingTime;
        Quaternion camRotation = mainCamera.rotation;
        
        while (timeElapsed < facing.facingTime)
        {
            mainCamera.LookAt(facing.facingTarget);
            Quaternion lookDirection = mainCamera.rotation;
            timeElapsed += Time.deltaTime;
            mainCamera.rotation = Quaternion.Lerp(camRotation, lookDirection, timeElapsed * lookSpeed);
            yield return null;
        }
    }
    #endregion
}