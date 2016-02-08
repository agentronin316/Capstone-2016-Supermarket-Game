using UnityEngine;
using System.Collections;

public enum FacingType
{
    FREE,
    DIRECTION_LOCK,
    LOCATION_LOCK
}

public class ScriptFacings
{
    public FacingType facingType = FacingType.FREE;
    public float facingTime = 0f;
    public Transform facingTarget;
}
