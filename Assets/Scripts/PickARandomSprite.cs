using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickARandomSprite : MonoBehaviour {

    public List<Sprite> sprites;

	private void Start()
	{
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count)];
	}
}
