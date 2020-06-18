using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    GameObject player;
    [Header("Chunk")]
    [SerializeField] GameObject chunk;
    [SerializeField] int viewDistance = 50;
    [Range(0,1000)]
    [SerializeField] float noiseScale;
    public static List<Chunk> chunks = new List<Chunk>();

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    // Update is called once per frame
    void Update()
    {
        for (float x = player.transform.position.x - viewDistance; x < player.transform.position.x + viewDistance; x += Chunk.width){
            for (float y = player.transform.position.y - viewDistance; y < player.transform.position.y + viewDistance; y += Chunk.height){
                
                Chunk chunck = GetChunk(new Vector2(x, y));
                if (chunck == null)
                {   
                    Vector2Int chunkPosition = Chunk.ToChunkScale(new Vector2(x, y));
                    SpawnChunk(chunkPosition);
                }
            }
        }
    }

    private void SpawnChunk(Vector2Int position)
    {
        Chunk chunkIntance = Instantiate(chunk, new Vector3(position.x, position.y, 0), Quaternion.identity).GetComponent<Chunk>();
        chunkIntance.transform.SetParent(transform);
        chunkIntance.SetNoiseScale(noiseScale);
    }

    public static Chunk GetChunk(Vector2 position){
        int x = Mathf.FloorToInt(position.x / Chunk.width) * Chunk.width;
        int y = Mathf.FloorToInt(position.y / Chunk.height) * Chunk.height;
        
        foreach (Chunk chunk in chunks)
        {
            if (chunk.transform.position == new Vector3(x, y, 0))
            {
                return chunk;
            }
        }
        return null;
    }


}
