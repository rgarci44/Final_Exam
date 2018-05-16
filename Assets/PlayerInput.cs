using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public float moveSpeed = 5.0f;
    public float radiusOfSatisfaction = 0.5f;
    public float radiusOfApproach = 1.00f;
    public float approach = 0.0f;
    private Vector3 target = Vector3.zero;
    private bool isSeekTargetSet;
    private Vector3 player;

    // Use this for initialization
    void Start()
    {
        player = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {


        if (isSeekTargetSet)
        {
            transform.LookAt(target);
            Vector3 moveDirection = target - transform.position;
            transform.position += moveDirection.normalized * Time.deltaTime * moveSpeed;
            if (Vector3.Distance(target, transform.position) <= radiusOfApproach)
            {
                approach = Vector3.Distance(target, transform.position) - radiusOfSatisfaction + 1;
                moveSpeed = approach;
                if (Vector3.Distance(target, transform.position) <= radiusOfSatisfaction)
                {
                    isSeekTargetSet = false;
                    moveSpeed = 5.0f;
                }

            }
        }

        TargetSet();
        player = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
    public void Seek(Vector3 position)
    {
        target = position;
        target.y = transform.position.y;
        isSeekTargetSet = true;
    }

    public void TargetSet()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 1000, 1 << LayerMask.NameToLayer("Floor")))
            {
                Seek(hitInfo.point);
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject otherObject = collision.gameObject;

        if (otherObject.tag == "Obstacle")
        {
            isSeekTargetSet = false;
        }

        if (otherObject.tag == "Pickup")
        {
            Destroy(otherObject);
            SimulateInput.pickupCounter += 1;
        }

    }
}


