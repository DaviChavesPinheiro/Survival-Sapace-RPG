using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    void OnDestroy()
    {
        Chunk chunk = transform.GetComponentInParent<Chunk>();
        if(chunk != null){
            chunk.SetBlock(new Vector2(transform.position.x, transform.position.y), 0);
        }
    }
}
