using BansheeGz.BGSpline.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("Damage that this enemy does per hit")]
    public int attackStrength;

    [Tooltip("How many hits per second this enemy can do.")]
    public float attackSpeed;

    [Tooltip("Range of the enemies attack.")]
    public float hitRange;

    [Tooltip("Cursor of the curve that enemies travel along.")]
    public BGCcCursor cursor;

    [Tooltip("Time at which the enemy should arrive. (Together with the spawn time this also determines the speed of the enemy.)")]
    public float arrivalTime;

    private float travelDuration;
    private BGCcCursorObjectRotate rotator;
    private Door door;
    private float lastAttack = 0f;

    private void Start()
    {
        travelDuration = arrivalTime - Time.time;
        rotator = cursor.GetComponent<BGCcCursorObjectRotate>();
        door = FindObjectOfType<Door>();
        if(door == null) { Debug.LogError("Enemy spawned and didn't find a door. Enemy became confused. It hurt itself in its confusion!"); }
    }

    private void LateUpdate()
    {
        // Update enemy position and rotation
        cursor.DistanceRatio = Mathf.Lerp(1f, 0f, (arrivalTime - Time.time) / travelDuration);
        transform.position = cursor.CalculatePosition();
        Quaternion rotateTo = Quaternion.identity;
        if (rotator.TryToCalculateRotation(ref rotateTo))
        {
            transform.rotation = rotateTo;
        }

        // Hit door if close enough
        if (door != null && Vector3.Distance(transform.position, door.transform.position) <= hitRange)
        {
            var attackCooldown = 1f / attackSpeed;
            if (Time.time - lastAttack >= attackCooldown)
            {
                door.Attack(attackStrength);
                lastAttack = Time.time;
            }
        }
    }
}
