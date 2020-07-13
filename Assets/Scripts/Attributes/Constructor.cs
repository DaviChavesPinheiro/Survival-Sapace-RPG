using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constructor : MonoBehaviour
{
    [SerializeField] LayerMask mask;
    ChunksController chunkGenerator;
    Inventory inventory;
    int combinedMask;
    private void Awake() {
        chunkGenerator = GameObject.FindObjectOfType(typeof(ChunksController)) as ChunksController;
        inventory = GetComponent<Inventory>();
    }
    void Update()
    {
        if (Input.touchCount > 0) {
			Touch toque = Input.touches[0];
            Vector2 virtualBlockPosition = BlockController.BlockPositionFromWorld(Camera.main.ScreenToWorldPoint(toque.position));
            Collider2D collider = Physics2D.OverlapBox(virtualBlockPosition + (Vector2.one/2f), Vector2.one * 0.95f, 0, mask);
            if(collider == null){
                PutBlock(virtualBlockPosition);
            }
		} else if (Input.GetMouseButton(0)){
            Vector2 virtualBlockPosition = BlockController.BlockPositionFromWorld(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Collider2D collider = Physics2D.OverlapBox(virtualBlockPosition + (Vector2.one/2f), Vector2.one * 0.95f, 0, mask);
            if(collider == null){
                PutBlock(virtualBlockPosition);
            }
        }
    }

    private void PutBlock(Vector2 position)
    {
        Item item = inventory.GetActiveSlot() != null ? inventory.GetActiveSlot().item : null;
        if(item == null) return;
        if(item.itemType != ItemType.block) return;
        if(inventory.GetActiveSlot().RemoveAmount(1) == 0){
            inventory.InventoryHasUpdated();
            ChunkController chunk = chunkGenerator.GetChunk(position);
            if(!chunk) return;
            chunk.SetBlock(position, item.id);
            chunk.RefreshBlock(position);
        }
        
    }
}
