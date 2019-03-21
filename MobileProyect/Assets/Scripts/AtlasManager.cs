using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class AtlasManager : MonoBehaviour
{
    enum Textures
    {
        P,
        U,
        T,
        O
    }
    [SerializeField] Textures textureToUse;
    [SerializeField] SpriteAtlas atlas;
    SpriteRenderer spRenderer;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(textureToUse.ToString());
        spRenderer = GetComponent<SpriteRenderer>();
        spRenderer.sprite = atlas.GetSprite(textureToUse.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwapToNextTexture();
        }
    }
    private void SwapToNextTexture()
    {
        textureToUse =(Textures)(((int)textureToUse + 1 ) % (Enum.GetValues(typeof(AtlasManager.Textures)).Length)) ;
        Debug.Log(textureToUse);
        spRenderer.sprite = atlas.GetSprite(textureToUse.ToString());
    }
}
