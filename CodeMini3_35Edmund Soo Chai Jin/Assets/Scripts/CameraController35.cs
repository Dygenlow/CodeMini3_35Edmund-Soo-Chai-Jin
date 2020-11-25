using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController35 : MonoBehaviour
{
    public GameObject playerGO;
    Vector3 posOffSet = new Vector3(0, 2.5f, -4);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, playerGO.transform.position + posOffSet, 0.1f);
    }
}
