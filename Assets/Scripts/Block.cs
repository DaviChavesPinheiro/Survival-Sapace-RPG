using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    void OnDestroy()
    {
        transform.GetComponentInParent<Chunk>().SetBlock(new Vector2(transform.position.x, transform.position.y));
    }
}
