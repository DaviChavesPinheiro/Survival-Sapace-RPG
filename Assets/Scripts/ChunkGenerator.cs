using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    public static int maxViewDst = 50;
    Transform viewer;

    public static Vector2 viewerPosition;
    int chunkSize;
    int chunkVisibleInViewDst;
    
    Dictionary<Vector2, Chunk> terrainChunkDictionary = new Dictionary<Vector2, Chunk>();
    List<Chunk> terrainChunksVisibleLastUpdate = new List<Chunk>();

    [SerializeField] GameObject chunkObject;
    [SerializeField] float noiseScale = 20f;

    void Start()
    {
        viewer = GameObject.FindGameObjectWithTag("Player").transform;
        chunkSize = Chunk.height;
        chunkVisibleInViewDst = Mathf.RoundToInt(maxViewDst / chunkSize);
    }

    void Update() {
		viewerPosition = new Vector2 (viewer.position.x, viewer.position.y);
		UpdateVisibleChunks ();
	}

    void UpdateVisibleChunks(){
        for (int i = 0; i < terrainChunksVisibleLastUpdate.Count; i++) {
			terrainChunksVisibleLastUpdate [i].SetVisible (false);
		}
		terrainChunksVisibleLastUpdate.Clear ();

        int currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x/chunkSize);
        int currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y/chunkSize);

        for (int yOffset = -chunkVisibleInViewDst; yOffset <= chunkVisibleInViewDst; yOffset++)
        {
            for (int xOffset = -chunkVisibleInViewDst; xOffset <= chunkVisibleInViewDst; xOffset++)
            {
                Vector2 viewedChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);

                if(terrainChunkDictionary.ContainsKey(viewedChunkCoord)){
                    terrainChunkDictionary[viewedChunkCoord].UpdateTerrainChunk ();
					if (terrainChunkDictionary[viewedChunkCoord].IsVisible()) {
						terrainChunksVisibleLastUpdate.Add(terrainChunkDictionary[viewedChunkCoord]);
					}
                } else {
                    Chunk chunk = Instantiate(chunkObject, new Vector3(viewedChunkCoord.x * chunkSize, viewedChunkCoord.y  * chunkSize, 0), Quaternion.identity).GetComponent<Chunk>();
                    chunk.transform.SetParent(transform);
                    chunk.SetNoiseScale(noiseScale + 0.123f);
                    terrainChunkDictionary.Add(viewedChunkCoord, chunk);
                }
            }
        }
    }

    // // Update is called once per frame
    // void Update()
    // {
    //     for (float x = viewer.position.x - maxViewDst; x < viewer.position.x + maxViewDst; x += Chunk.width){
    //         for (float y = viewer.position.y - maxViewDst; y < viewer.position.y + maxViewDst; y += Chunk.height){
                
    //             Chunk chunck = GetChunk(new Vector2(x, y));
    //             if (chunck == null)
    //             {   
    //                 Vector2Int chunkPosition = Chunk.ToChunkScale(new Vector2(x, y));
    //                 SpawnChunk(chunkPosition);
    //             }
    //         }
    //     }
    // }

    private void SpawnChunk(Vector2 position)
    {
        Chunk chunk = Instantiate(chunkObject, new Vector3(position.x, position.y, 0), Quaternion.identity).GetComponent<Chunk>();
        chunk.transform.SetParent(transform);
        chunk.SetNoiseScale(noiseScale + 0.123f);
    }

    // public static Chunk GetChunk(Vector2 position){
    //     int x = Mathf.FloorToInt(position.x / Chunk.width) * Chunk.width;
    //     int y = Mathf.FloorToInt(position.y / Chunk.height) * Chunk.height;
        
    //     foreach (Chunk chunk in chunks)
    //     {
    //         if (chunk.transform.position == new Vector3(x, y, 0))
    //         {
    //             return chunk;
    //         }
    //     }
    //     return null;
    // }


}
