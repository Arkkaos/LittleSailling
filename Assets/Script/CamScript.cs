using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{

    [SerializeField]
    public GameObject playerObject;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = playerObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerObject.transform.position + new Vector3(0,0,-10);
    }
}
