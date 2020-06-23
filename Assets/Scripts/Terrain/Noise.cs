using UnityEngine;
using System.Collections;

public static class Noise {

	public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale, float offsetX, float offsetY) {
		float[,] noiseMap = new float[mapWidth,mapHeight];
        const int geralOffset = 1000000;
		if (scale <= 0) {
			scale = 0.0001f;
		}

		for (int y = 0; y < mapHeight; y++) {
			for (int x = 0; x < mapWidth; x++) {
				float sampleX = (x + offsetX - geralOffset) / scale;
				float sampleY = (y + offsetY - geralOffset) / scale;

				float perlinValue = Mathf.PerlinNoise (sampleX, sampleY);
				noiseMap [x, y] = perlinValue;
			}
		}

		return noiseMap;
	}

}