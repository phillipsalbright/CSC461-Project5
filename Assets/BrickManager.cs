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

    private static GameObject[,,] bricks;

    // Start is called before the first frame update
    void Start()
    {
        bricks = new GameObject[NUM_BRICKS_X,NUM_BRICKS_Y,NUM_BRICKS_Z];

        float xSpacing = brick.transform.localScale.x + SPACING;
        float ySpacing = brick.transform.localScale.y + SPACING;
        float zSpacing = brick.transform.localScale.z + SPACING;

        for (int x = 0; x < NUM_BRICKS_X; x++)
        {
            for (int y = 0; y < NUM_BRICKS_Y; y++)
            {
                for (int z = 0; z < NUM_BRICKS_Z; z++)
                {
                    bricks[x, y, z] = Instantiate(brick, new Vector3(BRICK_START_X + (x * xSpacing), BRICK_START_Y + (y * ySpacing), BRICK_START_Z - (z * zSpacing)), Quaternion.identity);
                    bricks[x, y, z].GetComponent<Brick>().gridPosition = new Vector3Int(x, y, z);
                }
            }
        }
    }


    public static void BreakBrick(GameObject brickObj, bool bigBrickBlast)
    {
        Vector3Int brickPos = brickObj.GetComponent<Brick>().gridPosition;
        if (bigBrickBlast)
        {
            for (int x = brickPos.x - 1; x <= brickPos.x + 1; x++)
            {
                for (int y = brickPos.y - 1; y <= brickPos.y + 1; y++)
                {
                    for (int z = brickPos.z - 1; z <= brickPos.z + 1; z++)
                    {
                        if ((x == brickPos.x - 1 || x == brickPos.x + 1) && ( y != brickPos.y || z != brickPos.z)
                            || (y == brickPos.y - 1 || y == brickPos.y + 1) && z != brickPos.z)
                        {
                            continue;
                        }

                        try
                        {
                            GameObject brick = bricks[x, y, z];
                            if (brick != null)
                            {
                                Destroy(brick);
                                bricks[x, y, z] = null;
                            }
                        }
                        catch (System.IndexOutOfRangeException)
                        {
                            // skip brick
                        }
                    }
                }
            }
        }
        else
        {
            Destroy(bricks[brickPos.x, brickPos.y, brickPos.z]);
            bricks[brickPos.x, brickPos.y, brickPos.z] = null;
        }
    }
}
