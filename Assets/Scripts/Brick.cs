using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public Vector3Int gridPosition;
    public string type = "default";
    public bool approaching = false;

    private Vector3 moveVector = new Vector3(0, 0, -0.0005f);

    private void Update()
    {
        if (approaching)
        {
            gameObject.transform.position += moveVector;
        }
    }
}
