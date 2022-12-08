using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    public bool approachingBricks = false;

    [SerializeField] GameObject brick;

    [SerializeField] int NUM_BRICKS_X = 8;
    [SerializeField] int NUM_BRICKS_Y = 5;
    [SerializeField] int NUM_BRICKS_Z = 4;

    [SerializeField] float BRICK_START_X = 0.5f;
    [SerializeField] float BRICK_START_Y = 7.6f;
    [SerializeField] float BRICK_START_Z = 27f;

    [SerializeField] float SPACING = 0.2f;

    private static GameObject[,,] bricks;
    static int brickTotal = 0;
    static int bricksBroken = 0;

    static bool bigBrickBlast = false;
    static int bricksBlasted = 0;

    private static int score = 0;
    [SerializeField] private TextMeshProUGUI scoreLabel;
    public GameObject winMenu;
    public GameObject ball;

    public Material orangeBrick;
    public Material blueBrick;

    // Start is called before the first frame update
    void Start()
    {
        brickTotal = 0;
        bricksBroken = 0;
        bigBrickBlast = false;
        bricksBlasted = 0;
        score = 0;
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
                    bricks[x, y, z].GetComponent<MeshRenderer>().material = orangeBrick;

                    if (approachingBricks)
                        bricks[x, y, z].GetComponent<Brick>().approaching = true;
                    else
                        bricks[x, y, z].GetComponent<Brick>().approaching = false;

                    int rand = Random.Range(0, 11);

                    if (rand < 3)
                    {
                        bricks[x, y, z].GetComponent<MeshRenderer>().material = blueBrick;
                        bricks[x, y, z].GetComponent<Brick>().type = "blast";
                    }
                    brickTotal++;
                }
            }
        }
    }

    private void Update()
    {
        scoreLabel.text = "Score: " + score;

        if (bricksBroken == brickTotal)
        {
            winMenu.SetActive(true);
            ball.GetComponent<Ball>().PauseBall();
            bricksBroken = 0;
        }
    }

    public static void BreakBrick(GameObject brickObj)
    {
        if (bricksBlasted < 3 && bigBrickBlast)
        {
            bricksBlasted++;
        }
        else
        {
            bigBrickBlast = false;
        }

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
                                score += 100;
                                bricksBroken++;
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
            score += 100;
            bricksBroken++;
        }

        if (brickObj.GetComponent<Brick>().type == "blast")
        {
            bricksBlasted = 0;
            bigBrickBlast = true;
        }
    }
}
