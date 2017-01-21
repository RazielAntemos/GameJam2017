using UnityEngine;
//using XInputDotNetPure;

public class Moving_pointer : MonoBehaviour
{
    private bool _playerIndexSet;
    public bool m_UseMouse;

    [SerializeField]
    private float moveSpeed = 1f;

    [SerializeField]
    //private PlayerIndex _playerIndex;

    //private GamePadState _state;
    //private GamePadState _prevState;

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
        //_prevState = _state;
        //_state = GamePad.GetState(_playerIndex);

        //  checking the borders of screen
        var dist = (transform.position.y - Camera.main.transform.position.y);
        var leftLimitation = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var rightLimitation = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;

        var upLimitation = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).z;
        var downLimitation = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).z;

        var tempx = Mathf.Clamp(transform.position.x, rightLimitation, leftLimitation);
        var tempz = Mathf.Clamp(transform.position.z, downLimitation, upLimitation);

        //moving using left thumbstick
       // transform.position = Vector3.Slerp(transform.position,
            //new Vector3(tempx + _state.ThumbSticks.Left.X * moveSpeed,
            //            transform.position.y,
            //            tempz + _state.ThumbSticks.Left.Y * moveSpeed), Time.deltaTime * 10);
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
    private void DoMouseMovement()
    {
       // var mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Plane groundPlane = new Plane(transform.up, 0);

        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            transform.position = ray.GetPoint(rayDistance);
        }
    }
}