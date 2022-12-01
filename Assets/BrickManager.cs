using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    [SerializeField] GameObject brick;

    [SerializeField] int NUM_BRICKS_X = 8;
    [SerializeField] int NUM_BRICKS_Y = 5;
    [SerializeField] int NUM_BRICKS_Z = 4;

    [SerializeField] float BRICK_START_X = 0.5f;
    [SerializeField] float BRICK_START_Y = 7.6f;
    [SerializeField] float BRICK_START_Z = 27f;

    [SerializeField] float SPACING = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        float xSpacing = brick.transform.localScale.x + SPACING;
        float ySpacing = brick.transform.localScale.y + SPACING;
        float zSpacing = brick.transform.localScale.z + SPACING;

        for (float x = BRICK_START_X; x < BRICK_START_X + NUM_BRICKS_X * xSpacing; x += xSpacing)
        {
            for (float y = BRICK_START_Y; y < BRICK_START_Y + NUM_BRICKS_Y * ySpacing; y += ySpacing)
            {
                for (float z = BRICK_START_Z; z > BRICK_START_Z - NUM_BRICKS_Z * zSpacing; z -= zSpacing)
                {
                    Instantiate(brick, new Vector3(x, y, z), Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
