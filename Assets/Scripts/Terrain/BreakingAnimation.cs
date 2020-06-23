using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingAnimation : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    SpriteRenderer spriteRenderer;
    Health health;
    private void Awake() {
        health = GetComponentInParent<Health>();
        health.onHealthChange += UpdateSprite;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void UpdateSprite()
    {
        int index = sprites.Length - Mathf.FloorToInt(health.GetPercetage() / 100f * sprites.Length);
        index = Mathf.Min(index, sprites.Length - 1);
        spriteRenderer.sprite = sprites[index];
    }

    private void OnDestroy() {
        health.onHealthChange -= UpdateSprite;
    }
}
