using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float acceleration;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0;
        acceleration = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        float windAngle = WindScript.getWindAngle(transform.position)-270;
        if (windAngle < 0) windAngle += 360;
        float windForce = WindScript.getWindForce(transform.position);
        Vector3 speedVector = transform.up * Time.deltaTime * speed;
        Vector3 wind = createWindVector(windAngle, windForce);
        Vector3 relativeWind = wind; //- speedVector;


        float angle = transform.rotation.eulerAngles.z - windAngle;
        print(windAngle+"wa");
        print(transform.rotation.eulerAngles.z+"z");
        print(angle+"a");
        if (angle < 45)
        {
            acceleration = -Mathf.Cos(angle*2);
        }
        else if (angle<90)
        {
            acceleration = Mathf.Sin((angle-45)*2);
        }
        else
        {
            acceleration = 1f;
        }
        //CODER L'INFLUENCE DE LA VITESSE SUR LA FORCE DU VENT
        acceleration *= relativeWind.magnitude;
        acceleration *= 0.001f;
        speed += acceleration;
        if (speed < 0)
        {
            speed = 0;
        }
        transform.position += transform.up * Time.deltaTime * speed;

        //transform.Rotate(new Vector3(0, 0, -Input.GetAxis("Horizontal")* Mathf.Max(speed / 5, 0.04f)));
        transform.Rotate(new Vector3(0, 0, -Input.GetAxis("Horizontal")));
    }

    Vector3 createWindVector(float angle, float force)
    {
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.right);
        Vector3 vec = rotation * new Vector3(force, 0, 0);
        return vec;
    }
}
