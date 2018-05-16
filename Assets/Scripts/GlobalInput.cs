using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInput : MonoBehaviour {

    private static GlobalInput instance;

    public static Vector3 mousePosition { get; private set; }
    public bool useSimulatedInput = false;

    private const int MOUSE_BUTTONS = 3;
    private bool[] mbD = new bool[MOUSE_BUTTONS];
    private bool[] mbU = new bool[MOUSE_BUTTONS];
    private bool[] mbH = new bool[MOUSE_BUTTONS];

    private Dictionary<KeyCode, bool> kD = new Dictionary<KeyCode, bool>();
    private Dictionary<KeyCode, bool> kU = new Dictionary<KeyCode, bool>();
    private Dictionary<KeyCode, bool> kH = new Dictionary<KeyCode, bool>();

    private List<KeyCode> uniqueKeyCodes = new List<KeyCode>();

    // Use this for initialization
    void Start () {
        if (instance != null)
        {
            Debug.LogError("GlobalInputCatcher may only be included on a single object. Duplicate found on " + this.gameObject.name + "; it has been automatically disabled.");
            this.enabled = false;
            return;
        }
        instance = this;
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (!uniqueKeyCodes.Contains(keyCode)) uniqueKeyCodes.Add(keyCode);
        }

        foreach (KeyCode keyCode in uniqueKeyCodes)
        {
            //print("Adding " + keyCode);
            kD.Add(keyCode, false);
            kU.Add(keyCode, false);
            kH.Add(keyCode, false);
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (!useSimulatedInput)
        {

            GlobalInput.mousePosition = Input.mousePosition;

            for (int i = 0; i < MOUSE_BUTTONS; i++) mbD[i] = mbU[i] = mbH[i] = false;
            for (int i = 0; i < MOUSE_BUTTONS; i++)
            {
                mbU[i] = Input.GetMouseButtonUp(i);
                mbD[i] = Input.GetMouseButtonDown(i);
                mbH[i] = Input.GetMouseButton(i);
            }

            foreach (KeyCode keyCode in uniqueKeyCodes) kD[keyCode] = kU[keyCode] = kH[keyCode] = false;
            foreach (KeyCode keyCode in uniqueKeyCodes)
            {
                kD[keyCode] = Input.GetKeyDown(keyCode);
                kU[keyCode] = Input.GetKeyUp(keyCode);
                kH[keyCode] = Input.GetKey(keyCode);
            }

        }
        else
        {
            // Un-down and un-up all mouse buttons and keys.
            // If they're down or up in this frame, the next script will call that.
            // And if the next script doesn't, then they should be false.
            for (int i = 0; i < MOUSE_BUTTONS; i++) mbD[i] = mbU[i] = false;
            foreach (KeyCode keyCode in uniqueKeyCodes) kD[keyCode] = kU[keyCode] = false;
        }
    }

    #region Mapping functions

    public static bool GetMouseButtonDown(int button)
    {
        if (button < 0 || button >= MOUSE_BUTTONS) return false;
        return instance.mbD[button];
    }

    public static bool GetMouseButtonUp(int button)
    {
        if (button < 0 || button >= MOUSE_BUTTONS) return false;
        return instance.mbU[button];
    }

    public static bool GetMouseButton(int button)
    {
        if (button < 0 || button >= MOUSE_BUTTONS) return false;
        return instance.mbH[button];
    }

    public static bool GetKeyDown(KeyCode keyCode)
    {
        return instance.kD[keyCode];
    }

    public static bool GetKeyDown(string keyName)
    {
        return GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), keyName));
    }

    public static bool GetKeyUp(KeyCode keyCode)
    {
        return instance.kU[keyCode];
    }

    public static bool GetKeyUp(string keyName)
    {
        return GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), keyName));
    }

    public static bool GetKey(KeyCode keyCode)
    {
        return instance.kH[keyCode];
    }

    public static bool GetKey(string keyName)
    {
        return GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), keyName));
    }

    #endregion

    #region Simulation functions

    public static void SimulateMousePosition(Vector3 mousePosition)
    {
        GlobalInput.mousePosition = mousePosition;
    }

    public static void SimulateMouseDown(int button)
    {
        instance.mbD[button] = true;
        instance.mbH[button] = true;
    }

    public static void SimulateMouseUp(int button)
    {
        instance.mbU[button] = true;
        instance.mbH[button] = false;
    }

    public static void SimulateKeyDown(KeyCode keyCode)
    {
        instance.kD[keyCode] = true;
        instance.kH[keyCode] = true;
    }

    public static void SimulateKeyUp(KeyCode keyCode)
    {
        instance.kU[keyCode] = true;
        instance.kH[keyCode] = false;
    }

    public static void SimulateKeyDown(string keyName)
    {
        SimulateKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), keyName));
    }

    public static void SimulateKeyUp(string keyName)
    {
        SimulateKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), keyName));
    }

    #endregion


}
