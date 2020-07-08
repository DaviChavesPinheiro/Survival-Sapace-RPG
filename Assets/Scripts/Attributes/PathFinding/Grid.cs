using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] LayerMask unwalkableMask;
    [SerializeField] Vector2 gridWorldSize;
    [SerializeField] int chunksViewRange = 1;
    [SerializeField] float nodeRadius;
    Node[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    private void Awake() {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = ChunkController.width * (chunksViewRange * 2 + 1);
        gridSizeY = ChunkController.height * (chunksViewRange * 2 + 1);
        transform.position = new Vector2(Mathf.FloorToInt(player.position.x / ChunkController.width) * ChunkController.width - ChunkController.width * chunksViewRange, Mathf.FloorToInt(player.position.y / ChunkController.height) * ChunkController.height - ChunkController.height * chunksViewRange);
        CreateGrid();
        
    }

    private void Update() {
        transform.position = new Vector2(Mathf.FloorToInt(player.position.x / ChunkController.width) * ChunkController.width - ChunkController.width * chunksViewRange, Mathf.FloorToInt(player.position.y / ChunkController.height) * ChunkController.height - ChunkController.height * chunksViewRange);
        MapGrid();
    }

    public int MaxSize {
		get {
			return gridSizeX * gridSizeY;
		}
	}

    private void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector2 worldPosition = (Vector2)transform.position + new Vector2(x, y);
                grid[x, y] = new Node(false, worldPosition + new Vector2(nodeRadius, nodeRadius), x, y);
            }
        }
    }

    private void MapGrid()
    {
        for (int x = 0; x < gridSizeX; x += ChunkController.width)
        {
            for (int y = 0; y < gridSizeY; y += ChunkController.height)
            {
                Vector2 worldChunkPosition = (Vector2)transform.position + new Vector2(x, y);
                ChunkController chunk = ChunksController.instance.GetChunk(worldChunkPosition);
                int[,] map = chunk?.GetMap();
                for (int xx = 0; xx < ChunkController.width; xx++)
                {
                    for (int yy = 0; yy < ChunkController.height; yy++)
                    {
                        grid[x + xx, y + yy] = new Node(map?[xx, yy] == 0, worldChunkPosition + new Vector2(xx + nodeRadius, yy + nodeRadius), x + xx, y + yy);
                    }
                }
                
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
    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireCube(transform.position + new Vector3((ChunkController.width * (chunksViewRange * 2 + 1)) / 2, (ChunkController.height * (chunksViewRange * 2 + 1)) / 2, 0), new Vector3(ChunkController.width * (chunksViewRange * 2 + 1), ChunkController.height * (chunksViewRange * 2 + 1), 1));
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
