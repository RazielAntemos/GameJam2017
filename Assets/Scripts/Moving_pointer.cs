using Boo.Lang;
using UnityEngine;
using XInputDotNetPure;

public class Moving_pointer : MonoBehaviour
{
    private bool _playerIndexSet;
    public bool m_UseMouse;
    [SerializeField]
    private float moveSpeed = 1f;

    [SerializeField]
    private PlayerIndex _playerIndex;

   

    private GamePadState _state;
    private GamePadState _prevState;

    // Use this for initialization
    private void Start()
    {
        // No need to initialize anything for the plugin
    }

    // Update is called once per frame
    private void Update()
    {
        //here we move the player/object
        DoMovement();
        DoKeyboardMovement(); //use arrows
        if (m_UseMouse)
        {
            DoMouseMovement();
        }
    }

    private void OnGUI()
    {
        //  DrawHelp();
    }

    private void DoMovement()
    {
        _prevState = _state;
        _state = GamePad.GetState(_playerIndex);

        //  checking the borders of screen
        var dist = (transform.position.y - Camera.main.transform.position.y);
        var leftLimitation = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var rightLimitation = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;

        var upLimitation = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).z;
        var downLimitation = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).z;

        var tempx = Mathf.Clamp(transform.position.x, rightLimitation, leftLimitation);
        var tempz = Mathf.Clamp(transform.position.z, downLimitation, upLimitation);

        //moving using left thumbstick
        transform.position = Vector3.Slerp(transform.position,
            new Vector3(tempx + _state.ThumbSticks.Left.X * moveSpeed,
                        transform.position.y,
                        tempz + _state.ThumbSticks.Left.Y * moveSpeed), Time.deltaTime * 10);
    }

    private void DrawHelp()
    {
        var text = "Use left stick to turn the cube, hold A to change color\n";
        text += string.Format("IsConnected {0} Packet #{1}\n", _state.IsConnected, _state.PacketNumber);
        text += string.Format("\tTriggers {0} {1}\n", _state.Triggers.Left, _state.Triggers.Right);
        text += string.Format("\tD-Pad {0} {1} {2} {3}\n", _state.DPad.Up, _state.DPad.Right, _state.DPad.Down, _state.DPad.Left);
        text += string.Format("\tButtons Start {0} Back {1} Guide {2}\n", _state.Buttons.Start, _state.Buttons.Back, _state.Buttons.Guide);
        text += string.Format("\tButtons LeftStick {0} RightStick {1} LeftShoulder {2} RightShoulder {3}\n", _state.Buttons.LeftStick, _state.Buttons.RightStick, _state.Buttons.LeftShoulder, _state.Buttons.RightShoulder);
        text += string.Format("\tButtons A {0} B {1} X {2} Y {3}\n", _state.Buttons.A, _state.Buttons.B, _state.Buttons.X, _state.Buttons.Y);
        text += string.Format("\tSticks Left {0} {1} Right {2} {3}\n", _state.ThumbSticks.Left.X, _state.ThumbSticks.Left.Y, _state.ThumbSticks.Right.X, _state.ThumbSticks.Right.Y);
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), text);
    }

    private void DoKeyboardMovement() //for debug purposes
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime * 10;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime * 10;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.forward * moveSpeed * Time.deltaTime * 10;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.back * moveSpeed * Time.deltaTime * 10;
        }
    }

    /// <summary>
    /// Allow controlling the pointer with the mouse
    /// </summary>
    void DoMouseMovement()
    {
        var mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Plane groundPlane=new Plane(transform.up,0);


        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            transform.position = ray.GetPoint(rayDistance);
        }

        if (Input.GetMouseButtonUp(0))
        {
           
    
        
        }
        

    }





}