using GlobalGameJam2018Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 6.0F;
    public Sprite[] directions;

    public GameObject test;

    private int lookingDir = 0;

    private const float EPSILON = 1e-3f;
    private IItem currentItem;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
    }

    private void Move()
    {
        Vector3 moveDirection = Vector3.zero;
        CharacterController controller = GetComponent<CharacterController>();
        SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();

        moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        if (moveDirection.sqrMagnitude > EPSILON)
        {
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= Speed;
            controller.Move(moveDirection * Time.deltaTime);

            lookingDir = Mathf.RoundToInt(Vector3.SignedAngle(Vector3.down, moveDirection, Vector3.forward) / 90);
            if (lookingDir < 0)
            {
                lookingDir += 4;
            }
            //renderer.transform.localRotation = Quaternion.AngleAxis(lookingDir * 90, Vector3.forward);

            if(directions.Length >= 4)
            {
                renderer.sprite = directions[lookingDir];
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(Input.GetButtonDown("Jump"))
        {
            Workbench bench = other.GetComponentInParent<Workbench>();

            if(currentItem == null)
            {
                currentItem = bench.GetItem();
            }
            else if(bench.CanInteract(currentItem))
            {
                if(bench.PutItem(currentItem))
                {
                    currentItem = null;
                }
            }

            foreach (SpriteRenderer renderer in bench.GetComponentsInChildren<SpriteRenderer>())
            {
                renderer.enabled = true;
            }
        }
    }
}
