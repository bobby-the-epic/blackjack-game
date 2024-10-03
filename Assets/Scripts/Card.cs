using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public GameObject cardFace;
    public Texture2D[] cardTextures;

    Material cardMat;
    // use SetTexture method to set the texture
    void Start()
    {
        cardMat = cardFace.GetComponent<Renderer>().material;
    }
    void Update()
    {

    }
}
