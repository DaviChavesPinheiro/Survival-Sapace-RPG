using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    [SerializeField] Item drop;
    [SerializeField] int amount = 1;
    private void Awake() {
        GetComponent<SpriteRenderer>().sprite = drop ? drop.icon : null;
    }
    public Item GetItem(){
        return drop;
    }
    public int GetAmout(){
        return amount;
    }
}
