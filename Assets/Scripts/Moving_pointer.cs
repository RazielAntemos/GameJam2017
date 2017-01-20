using UnityEngine;
using XInputDotNetPure;

public class Moving_pointer : MonoBehaviour
{
    private bool _playerIndexSet = false;

    [SerializeField]
    private float moveSpeed = 1f;

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
        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected ans use it
        if (!_playerIndexSet || !_prevState.IsConnected)
        {
            for (var i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (!testState.IsConnected) continue;
                Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                _playerIndex = testPlayerIndex;
                _playerIndexSet = true;
            }
        }

        //here we move the player/object
        DoMovement();

        // Set vibration according to triggers
        GamePad.SetVibration(_playerIndex, _state.Triggers.Left, _state.Triggers.Right);

        // Make the current object turn
        transform.localRotation *= Quaternion.Euler(0.0f, _state.ThumbSticks.Left.X * 25.0f * Time.deltaTime, 0.0f);
    }

    private void OnGUI()
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

    private void DoMovement()
    {
        _prevState = _state;
        _state = GamePad.GetState(_playerIndex);

        // Detect if a button was pressed this frame
        if (_prevState.Buttons.A == ButtonState.Released && _state.Buttons.A == ButtonState.Pressed)
        {
            GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);
        }
        // Detect if a button was released this frame
        if (_prevState.Buttons.A == ButtonState.Pressed && _state.Buttons.A == ButtonState.Released)
        {
            GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }

        ////Applying movement to an object;
        //if (_state.DPad.Left == ButtonState.Pressed)
        //{
        //    transform.position = Vector3.Slerp(transform.position, new Vector3(transform.position.x - moveSpeed, transform.position.y, transform.position.z), Time.deltaTime * 10);
        //}

        //if (_state.DPad.Right  == ButtonState.Pressed)
        //{
        //    transform.position = Vector3.Slerp(transform.position, new Vector3(transform.position.x + moveSpeed, transform.position.y, transform.position.z), Time.deltaTime * 10);
        //}

        //if (_state.DPad.Up == ButtonState.Pressed)
        //{
        //    transform.position = Vector3.Slerp(transform.position, new Vector3(transform.position.x , transform.position.y, transform.position.z + moveSpeed), Time.deltaTime * 10);
        //}
        
        //if (_state.DPad.Down  == ButtonState.Pressed)
        //{
        //    transform.position = Vector3.Slerp(transform.position, new Vector3(transform.position.x , transform.position.y, transform.position.z - moveSpeed), Time.deltaTime * 10);
        //}

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
            new Vector3(tempx + _state.ThumbSticks.Left.X, 
                        transform.position.y,
                        tempz + _state.ThumbSticks.Left.Y), Time.deltaTime * 10);


    }

   
}