using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] LayerMask unwalkableMask;
    [SerializeField] Vector2 gridWorldSize;
    [SerializeField] float nodeRadius;
    Node[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    private void Start() {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.FloorToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.FloorToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    private void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector2 worldPosition = (Vector2)transform.position + new Vector2(x + nodeRadius, y + nodeRadius);
                bool walkable = !(Physics2D.OverlapCircle(worldPosition, nodeRadius-.1f, unwalkableMask));
                grid[x, y] = new Node(walkable, worldPosition);
            }
        }
    }

    public Node NodeFromWorldPoint(Vector2 worldPosition){
        int x = Mathf.FloorToInt((worldPosition.x - transform.position.x) / nodeDiameter);
        int y = Mathf.FloorToInt((worldPosition.y - transform.position.y) / nodeDiameter);
        Mathf.Max(x, 0);
        Mathf.Min(x, gridSizeX - 1);
        Mathf.Max(y, 0);
        Mathf.Min(y, gridSizeY - 1);
        return grid[x, y];
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireCube(transform.position + new Vector3(gridWorldSize.x / 2, gridWorldSize.y / 2, 0), new Vector3(gridWorldSize.x, gridWorldSize.y, 1));
        if(grid != null) {
            Node playerNode = NodeFromWorldPoint(player.transform.position);
            foreach (Node node in grid)
            {
                if(node.walkable && playerNode != node) continue;
                Gizmos.color = Color.red;
                if(playerNode == node) Gizmos.color = Color.cyan;
                Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}
