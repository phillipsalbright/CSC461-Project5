using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour
{
    float timePassed;

    private void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > 2)
        {
            Destroy(this.gameObject);
        }
    }
}
