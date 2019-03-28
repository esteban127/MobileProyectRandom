using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] GameObject testSprite;

    public void OnClick()
    {
        testSprite.GetComponent<SpriteRenderer>().color = GetComponent<Image>().color;
    }
}
