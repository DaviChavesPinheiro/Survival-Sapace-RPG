﻿using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour, ISaveable
{
    public static int maxViewDst = 50;
    Transform player;

    public static Vector2 viewerPosition;
    int chunkSize;
    int chunkVisibleInViewDst;
    
    Dictionary<string, int[]> data = new Dictionary<string, int[]>();
    Dictionary<Vector2, Chunk> terrainChunkDictionary = new Dictionary<Vector2, Chunk>();
    List<Chunk> terrainChunksVisibleLastUpdate = new List<Chunk>();

    [SerializeField] GameObject chunkObject;
    [SerializeField] float noiseScale = 20f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        chunkSize = Chunk.height;
        chunkVisibleInViewDst = Mathf.RoundToInt(maxViewDst / chunkSize);
    }

    void Update() {
		viewerPosition = new Vector2 (player.position.x, player.position.y);
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
                } else
                {
                    Chunk chunk = SpawnChunk(viewedChunkCoord);
                    if (data.ContainsKey(viewedChunkCoord.ToString()))
                    {
                        chunk.SetMap(data[viewedChunkCoord.ToString()]);
                    }
                    else
                    {
                        chunk.GenereteMap();
                    }
                    terrainChunkDictionary.Add(viewedChunkCoord, chunk);
                }
            }
        }
    }

    private Chunk SpawnChunk(Vector2 viewedChunkCoord)
    {
        Chunk chunk = Instantiate(chunkObject, new Vector3(viewedChunkCoord.x * chunkSize, viewedChunkCoord.y * chunkSize, 0), Quaternion.identity).GetComponent<Chunk>();
        chunk.transform.SetParent(transform);
        chunk.SetNoiseScale(noiseScale + 0.123f);
        return chunk;
    }

    public object CaptureState()
    {
        foreach (Vector2 chunkCoord in terrainChunkDictionary.Keys)
        {
            data[chunkCoord.ToString()] = Chunk.MapToOneDimensionalMap(terrainChunkDictionary[chunkCoord].GetMap());
        }
        return data;
    }

    public void RestoreState(object state)
    {
        data = (Dictionary<string, int[]>)state;
    }

    public static Vector2 StringToVector2(string sVector)
     {
         if (sVector.StartsWith ("(") && sVector.EndsWith (")")) {
             sVector = sVector.Substring(1, sVector.Length-2);
         }

         string[] sArray = sVector.Split(',');
 
         Vector2 result = new Vector2(
             int.Parse(sArray[0]),
             int.Parse(sArray[1]));
 
         return result;
     }

}
