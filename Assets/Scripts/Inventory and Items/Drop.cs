using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    [SerializeField] Item drop;
    public Item GetItem(){
        return drop;
    }
}
