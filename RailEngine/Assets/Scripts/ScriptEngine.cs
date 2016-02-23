using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// @author Marshall Mason
/// @description Core movement engine with data import and timeline tracking
/// </summary>
public class ScriptEngine : MonoBehaviour {


    public ScriptCharacterData playerCharacter;
    public List<ScriptWaypoint> waypoints;
    public List<ScriptFacings> facings;
    public float trackingSpeed = 5f;
    int currentWaypoint = 0;
    int currentFacing = 0;

    bool lookChange = true;
    bool freeLook = true;
    Transform mainCamera;


	void Start ()
    {
        mainCamera = Camera.main.transform;
        //Simple test index for demo purposes until menus are implemented
        ScriptFileImport.LoadPath(1, out waypoints, out facings);

        //Actual production methodology
        waypoints = ScriptFileImport.Waypoints;
        facings = ScriptFileImport.Facings;

        //Gentlemen, Start your Engines!
        StartCoroutine(MoveEngine());
        StartCoroutine(CameraEngine());
	}

    void Update ()
    {
        if (freeLook)
    }

    #region Movement
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
                case MoveType.BEZIER2:
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
        if (curMove.moveType == MoveType.BEZIER)
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
        Quaternion camRotation = mainCamera.rotation;
        mainCamera.LookAt(facing.facingTarget);
        Quaternion lookDirection = mainCamera.rotation;

        while (timeElapsed < facing.facingTime)
        {
            timeElapsed += Time.deltaTime;
            mainCamera.rotation = Quaternion.Lerp(camRotation, lookDirection, timeElapsed / facing.facingTime * trackingSpeed);
            yield return null;
        }
    }

    IEnumerator LocationLock(ScriptFacings facing)
    {
        float timeElapsed = 0;
        Quaternion camRotation = mainCamera.rotation;

        while (timeElapsed < facing.facingTime)
        {
            mainCamera.LookAt(facing.facingTarget);
            Quaternion lookDirection = mainCamera.rotation;
            timeElapsed += Time.deltaTime;
            mainCamera.rotation = Quaternion.Lerp(camRotation, lookDirection, timeElapsed / facing.facingTime * trackingSpeed);
            yield return null;
        }
    }
    #endregion
}