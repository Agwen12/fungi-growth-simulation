using UnityEngine;

public class Camera : MonoBehaviour
{
    private float _mainSpeed = 15.0f;
    private float _cameraSensitivity = 0.15f;
    private Vector3 _lastMouse = new Vector3(255, 255, 255); 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _lastMouse = Input.mousePosition - _lastMouse ;
        _lastMouse = new Vector3(-_lastMouse.y * _cameraSensitivity, _lastMouse.x * _cameraSensitivity, 0 );
        _lastMouse = new Vector3(transform.eulerAngles.x + _lastMouse.x , transform.eulerAngles.y + _lastMouse.y, 0);
        transform.eulerAngles = _lastMouse;
        _lastMouse =  Input.mousePosition;

        Vector3 p = GetMovementVector();
        p *= _mainSpeed * Time.deltaTime;
        
        transform.Translate(p);  
    }

    private Vector3 GetMovementVector() 
    {
        Vector3 movementVector = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
            movementVector += new Vector3(0, 0, 1);
        if (Input.GetKey(KeyCode.S))
            movementVector += new Vector3(0, 0, -1);
        if (Input.GetKey(KeyCode.A))
            movementVector += new Vector3(-1, 0, 0);
        if (Input.GetKey(KeyCode.D))
            movementVector += new Vector3(1, 0, 0);
        if (Input.GetKey(KeyCode.LeftControl))
            movementVector += new Vector3(0, -1, 0);
        if (Input.GetKey(KeyCode.LeftShift))
            movementVector += new Vector3(0, 1, 0);

        return movementVector;
    }
}
