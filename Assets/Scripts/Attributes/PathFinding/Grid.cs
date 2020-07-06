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

    private void Awake() {
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
                grid[x, y] = new Node(walkable, worldPosition, x, y);
            }
        }
    }

    public List<Node> GetNeighbours(Node node){
        List<Node> neighbours = new List<Node>();

        if(node.gridX - 1 >= 0)
            neighbours.Add(grid[node.gridX - 1, node.gridY]);
        if(node.gridY - 1 >= 0)
            neighbours.Add(grid[node.gridX, node.gridY - 1]);
        if(node.gridX + 1 < gridSizeX)
            neighbours.Add(grid[node.gridX + 1, node.gridY]);
        if(node.gridY + 1 < gridSizeY)
            neighbours.Add(grid[node.gridX, node.gridY + 1]);
        return neighbours;
    }

    public Node NodeFromWorldPoint(Vector2 worldPosition){
        int x = Mathf.FloorToInt((worldPosition.x - transform.position.x) / nodeDiameter);
        int y = Mathf.FloorToInt((worldPosition.y - transform.position.y) / nodeDiameter);
        x = Mathf.Max(x, 0);
        x = Mathf.Min(x, gridSizeX - 1);
        y = Mathf.Max(y, 0);
        y = Mathf.Min(y, gridSizeY - 1);
        return grid[x, y];
    }
    public List<Node> path;
    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireCube(transform.position + new Vector3(gridWorldSize.x / 2, gridWorldSize.y / 2, 0), new Vector3(gridWorldSize.x, gridWorldSize.y, 1));
        if(grid != null) {
            Node playerNode = NodeFromWorldPoint(player.transform.position);
            foreach (Node node in grid)
            {
                if(node.walkable && playerNode != node && path != null && !path.Contains(node)) continue;
                Gizmos.color = Color.red;
                if(path != null && path.Contains(node)) Gizmos.color = Color.yellow;
                if(playerNode == node) Gizmos.color = Color.cyan;
                Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}
