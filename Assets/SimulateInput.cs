using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulateInput : MonoBehaviour
{

    public GameObject pickup1;
    public GameObject pickup3;
    public GameObject pickup2;
    public GameObject pickup4;
    public bool victoryCondition = false;
    public static int pickupCounter = 0;
     public float rotationSpeed = 90f;
    public Camera MainCamera;
    public GameObject Player;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        pickupCheck();
        if (victoryCondition)
        {
            victoryCamera();
            Debug.Log("Win");
        }
    }

    void pickupCheck()
    {
        if (pickupCounter == 4)
        {
            victoryCondition = true;
        }
    }

    void victoryCamera()
    {
        MainCamera.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
        MainCamera.transform.eulerAngles = new Vector3(0, MainCamera.transform.eulerAngles.y, 0);
        MainCamera.transform.Rotate(transform.up, rotationSpeed * Time.deltaTime, Space.World);
    }
}
   


