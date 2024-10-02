using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public GameObject cardFront;
    public Texture2D[] cardTextures;

    Material cardMat;
    // use SetTexture method to set the texture
    void Start()
    {
        cardMat = cardFront.GetComponent<Renderer>().material;
    }
    void Update()
    {

    }
}
