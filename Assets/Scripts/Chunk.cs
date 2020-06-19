using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    GameObject chunkObject;
    public static int height = 25, width = 25;
    Vector2 position;
	Bounds bounds;

    [SerializeField] GameObject block;

    float[,] map;
    float noiseScale;
    
    public void UpdateTerrainChunk() {
        float viewerDstFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance (ChunkGenerator.viewerPosition));
        bool visible = viewerDstFromNearestEdge <= ChunkGenerator.maxViewDst;
        SetVisible (visible);
    }

    public void SetVisible(bool visible) {
        gameObject.SetActive (visible);
    }

    public bool IsVisible() {
        return gameObject.activeSelf;
    }


    void Start()
    {   
        bounds = new Bounds(transform.position, Vector2.one * height);
        GenereteMap();
        SetVisible(false);

    }

    public void SetNoiseScale(float noiseScale){
        this.noiseScale = noiseScale;
    }

    public void SetBlock(Vector2 position){
        Vector2 localPosition = position - new Vector2(transform.position.x, transform.position.y);
        map[Mathf.FloorToInt(localPosition.x), Mathf.FloorToInt(localPosition.y)] = 0;
    }
    
    private void GenereteMap(){

        map = Noise.GenerateNoiseMap (width, height, noiseScale, transform.position.x, transform.position.y);
        GenereteBlocks();
    }


    private void GenereteBlocks()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(map[x,y] >= 0.75){
                    GameObject blockInstance = Instantiate(block, new Vector3(x + transform.position.x, y + transform.position.y, transform.position.z), transform.rotation) as GameObject;
                    blockInstance.transform.SetParent(transform);
                }
            }
        }
    }

    public static Vector2Int ToChunkScale(Vector2 position){
        int x = Mathf.FloorToInt(position.x / Chunk.width) * Chunk.width;
        int y = Mathf.FloorToInt(position.y / Chunk.height) * Chunk.height;
        return new Vector2Int(x, y);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position + new Vector3(width / 2, height / 2, 0), new Vector3(width, height, 1));        
    }



}
