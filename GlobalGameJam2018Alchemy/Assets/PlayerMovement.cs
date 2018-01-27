using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 6.0F;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 moveDirection = Vector3.zero;
        CharacterController controller = GetComponent<CharacterController>();
        SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();

        moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        if(moveDirection.sqrMagnitude > 0)
        {
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= Speed;
            controller.Move(moveDirection * Time.deltaTime);

            int lookingDir = Mathf.RoundToInt(Vector3.SignedAngle(Vector3.down, moveDirection, Vector3.forward) / 90);
            renderer.transform.localRotation = Quaternion.AngleAxis(lookingDir * 90, Vector3.forward);
        }
    }
}
