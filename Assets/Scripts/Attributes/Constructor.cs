using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constructor : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    ChunkGenerator chunkGenerator;
    Inventory inventory;
    private void Awake() {
        chunkGenerator = GameObject.FindObjectOfType(typeof(ChunkGenerator)) as ChunkGenerator;
        inventory = GetComponent<Inventory>();
    }
    void Update()
    {
        if (Input.touchCount > 0) {
			Touch toque = Input.touches[0];
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(toque.position), Vector3.forward);
            if(hit.collider == null){
                PutBlock(Camera.main.ScreenToWorldPoint(toque.position));
            }
		} else if (Input.GetMouseButton(0)){
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
            if(hit.collider == null){
                PutBlock(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }
    }

    private void PutBlock(Vector2 position)
    {
        Item item = inventory.GetActiveItem();
        if(item == null) return;
        if(item.itemType != ItemType.block) return;
        if(inventory.Remove(item, 1)){
            Chunk chunk = chunkGenerator.GetChunk(position);
            if(!chunk) return;
            chunk.SetBlock(position, item.id);
            chunk.RefreshBlock(position);
        }
        
    }
}
