using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    float mainSpeed = 100.0f;
    float cameraSensitivity = 0.25f;
    private Vector3 lastMouse = new Vector3(255, 255, 255); 

    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        lastMouse = Input.mousePosition - lastMouse ;
        lastMouse = new Vector3(-lastMouse.y * cameraSensitivity, lastMouse.x * cameraSensitivity, 0 );
        lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x , transform.eulerAngles.y + lastMouse.y, 0);
        transform.eulerAngles = lastMouse;
        lastMouse =  Input.mousePosition;

        Vector3 p = GetMovementVector();
        p *= mainSpeed * Time.deltaTime;
        
        transform.Translate(p);  
    }

    private Vector3 GetMovementVector() {
            Vector3 movementVector = new Vector3(0, 0, 0);
            if (Input.GetKey (KeyCode.W))
                movementVector += new Vector3(0, 0 , 1);
            if (Input.GetKey (KeyCode.S))
                movementVector += new Vector3(0, 0, -1);
            if (Input.GetKey (KeyCode.A))
                movementVector += new Vector3(-1, 0, 0);
            if (Input.GetKey (KeyCode.D))
                movementVector += new Vector3(1, 0, 0);
            if (Input.GetKey (KeyCode.LeftControl))
                movementVector += new Vector3(0, -1, 0);
            if (Input.GetKey (KeyCode.LeftShift))
                movementVector += new Vector3(0, 1, 0);
                
            return movementVector;
        }
}
