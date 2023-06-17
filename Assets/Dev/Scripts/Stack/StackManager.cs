using Assets.Dev.Scripts.Tiles;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    public List<Gem> gems = new List<Gem>();
    [SerializeField] Transform AddingPos, RemovingPos;

    bool isSold = false;
    bool isStacked = false;

    public StackManager()
    {
        gems = new List<Gem>();
    }

    public void AddGem(Gem gem)
    {
        //gem listeye ekleme iþlemi
        gems.Add(gem);

        //gem parenti stickmanBag ayarlandý
        gem.gameObject.transform.SetParent(AddingPos.transform);

        //gem taþýma iþlemi
        gem.gameObject.transform
            .DOLocalJump(new Vector3(0, gems.Count, 0), 5, 1, .3f)
            .OnComplete(() => 
            {
                isStacked = false;
            });    
    }

    public void RemoveGem()
    {                
        Gem removedGem = gems[gems.Count - 1];        

        //gem satma animasyonu
        Tween removeTween = removedGem.gameObject.transform
            .DOJump(RemovingPos.position, 5, 1, .1f)
            .OnComplete(() => 
                { 
                    removedGem.transform.parent = null;
                    removedGem.transform.localPosition = RemovingPos.position;
                    Destroy(removedGem.gameObject);

                    isSold = false;
                });
        
        //gem listeden silindi       
        gems.RemoveAt(gems.Count - 1);        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gem") && !isStacked)
        {
            isStacked = true;
            Gem gemToCarry = other.GetComponent<Gem>();
            gemToCarry.isUpdateEnable = false;

            //Gem i sýrtýna al
            AddGem(gemToCarry);

            //Toplandýktan sonra yeni gem oluþtur                        
            gemToCarry.tile.CreateGem();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("SellingArea")) return;

        if (gems.Count > 0 && !isSold)
        {
            isSold = true;

            //Gem i sýrttan indirme
            RemoveGem();
        }
    }
}
