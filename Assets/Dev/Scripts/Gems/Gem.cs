using Assets.Dev.Scripts.Tiles;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] List<GemScriptable> gemScriptables = new List<GemScriptable>();
    public string _gemName;
    public float  _initSellingPrice;
    public Sprite _gemSprite;
    public GameObject _gemPrefab;

    public void Start()
    {
        CreateRandomGem();
    }

    void CreateRandomGem()
    { 
        int randomGem = Random.Range(0, gemScriptables.Count);
        Debug.Log("random gem: " + randomGem);
        _gemName = gemScriptables[randomGem].GemName;
        _initSellingPrice = gemScriptables[randomGem].InitSellingPrice;
        _gemSprite = gemScriptables[randomGem].GemIcon;
        _gemPrefab = gemScriptables[randomGem].GemPrefab;       
    }
}
