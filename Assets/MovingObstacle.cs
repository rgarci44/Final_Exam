using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour {

    public float moveSpeed = 3.0f;
    public bool switchMovement = false;
    public bool isMoving = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        checkForInput();
        if (isMoving)
        {
            Vector3 moveDirection = Vector3.zero;
            if (switchMovement)
            {
               
                moveDirection += transform.right;
            }

            else if (!switchMovement)
            {
               
                moveDirection -= transform.right;
            }

            transform.position += moveDirection.normalized * Time.deltaTime * moveSpeed;
        }

    }


   public void checkForInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 1000, 1 << LayerMask.NameToLayer("GreenObstacle")))
            {
                isMoving = true;
                switchMovement = !switchMovement;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject otherObject = collision.gameObject;

        if (otherObject.tag == "Obstacle")
        {
            isMoving = false;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
