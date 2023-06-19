using Assets.Dev.Scripts.Tiles;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public GemTypeSO gemType;
    public Tile tile;

    bool isCollectable = false;

    float scale = 0;

    //for performance increasement;
    public bool isUpdateEnable = true;

    Collider gemCollider;

    void Awake()
    {
        gemCollider = gameObject.GetComponent<BoxCollider>();
        gemCollider.enabled = false;
    }

    void Update()
    {
        if (!isUpdateEnable) return;

        scale += (Time.deltaTime / 5);

        if (scale >= 0.25f && !isCollectable)
        {
            isCollectable = true;
            gemCollider.enabled = true;
        }

        if (scale >= 1)
        {
            scale = 1;
            isUpdateEnable = false;
        }

        transform.localScale = new Vector3(scale, scale, scale);

    }
}
