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

    [Tooltip("Animator to play animations like 'attack'")]
    public Animator animator;

    [Tooltip("Amount of seconds that the attack animation has to play before the monster deals damage.")]
    public float secondsOfAnimationUntilHit = 0f;

    [Tooltip("Cursor of the curve that enemies travel along.")]
    public BGCcCursor cursor;

    [Tooltip("Time at which the enemy should arrive. (Together with the spawn time this also determines the speed of the enemy.)")]
    public float arrivalTime;

    [Tooltip("Sound player that is used for the charging part of the attack.")]
    public SoundPlayer charge;

    [Tooltip("Sound player that is used for the hitting part of the attack.")]
    public SoundPlayer hit;

    private float travelDuration;
    private BGCcCursorObjectRotate rotator;
    private Door door;
    private float lastAttack = 0f;
    private bool isHitting = false;

    private void Start()
    {
        travelDuration = arrivalTime - Time.time;
        rotator = cursor.GetComponent<BGCcCursorObjectRotate>();
        door = FindObjectOfType<Door>();
        if (door == null) { Debug.LogError("Enemy spawned and didn't find a door. Enemy became confused. It hurt itself in its confusion!"); }
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
                isHitting = false;
                hit?.PlaySound();
            }
            if (!isHitting && Time.time - lastAttack + secondsOfAnimationUntilHit >= attackCooldown)
            {
                animator.SetTrigger("Attack");
                isHitting = true;
                charge?.PlaySound();
            }
        }
    }
}
