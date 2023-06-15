using Assets.Dev.Scripts.Tiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        CreateGem();
    }
   
    public void CreateGem()
    {
        GemTypeSO gemType = gameManager.availableGemTypes[Random.Range(0, gameManager.availableGemTypes.Count)];
        GameObject gemObject = Instantiate(gemType.Model, transform,false);
        Gem gemScript= gemObject.AddComponent<Gem>();
        gemScript.gemType = gemType;
        gemScript.tile = this;
    }
}
