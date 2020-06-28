using System;
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

    int[,] map = new int[width, height];
    float noiseScale;
    
    void Start()
    {   
        bounds = new Bounds(transform.position, Vector2.one * height);
        SetVisible(false);
    }

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

    public void SetNoiseScale(float noiseScale){
        this.noiseScale = noiseScale;
    }

    public void SetBlock(Vector2 position, int id){
        Vector2 localPosition = position - new Vector2(transform.position.x, transform.position.y);
        map[Mathf.FloorToInt(localPosition.x), Mathf.FloorToInt(localPosition.y)] = id;
    }
    
    public void GenereteMap(){
        float[,] noiseMap = Noise.GenerateNoiseMap (width, height, noiseScale, transform.position.x, transform.position.y);
        map = NoiseMapToBlocksMap(noiseMap);
        GenereteBlocks();
    }

    private int[,] NoiseMapToBlocksMap(float[,] noiseMap)
    {
        int[,] mapBlock = new int[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(noiseMap[x,y] >= 0.75){
                    mapBlock[x,y] = 1;
                }
                if(noiseMap[x,y] >= 0.9){
                    mapBlock[x,y] = 2;
                }
            }
        }
        return mapBlock;
    }

    private void GenereteBlocks()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(map[x,y] == 0) continue;
                
                GameObject blockInstance = Instantiate(GM.instance.items.items[map[x,y]].prefab, new Vector3(x + transform.position.x, y + transform.position.y, transform.position.z), transform.rotation) as GameObject;
                blockInstance.transform.SetParent(transform);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position + new Vector3(width / 2, height / 2, 0), new Vector3(width, height, 1));        
    }

    public void SetMap(int[,] map){
        this.map = map;
        GenereteBlocks();
    }

    public int[,] GetMap(){
        return map;
    }
    
    public void SetMap(int[] map1d){
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map[x, y] = map1d[width * x + y];
            }
        }
        GenereteBlocks();
    }

    public static int[] MapToOneDimensionalMap(int[,] map){
        int[] map1d = new int[width * height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map1d[width * x + y] = map[x, y];
            }
        }
        return map1d;
    }

}
