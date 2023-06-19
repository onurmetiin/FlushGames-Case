using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StackManager : MonoBehaviour
{
    public List<Gem> gems = new List<Gem>();
    [SerializeField] Transform AddingPos, RemovingPos;

    bool isSold = false;
    bool isStacked = false;

    public UnityEvent UpdateMoneyText;

    public void AddGem(Gem gem)
    {
        //gem listeye ekleme i�lemi
        gems.Add(gem);

        //gem parenti stickmanBag ayarland�
        gem.gameObject.transform.SetParent(AddingPos.transform);

        //gem ta��ma i�lemi
        gem.gameObject.transform
            .DOLocalJump(new Vector3(0, gems.Count, 0), 5, 1, .3f)
            .OnComplete(() => 
            {
                //toplan�lan gem database de art�r�ld�
                DataManager.IncreaseCollectedGem(gem.gemType.Name);
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
                    //kazan�lan para database e eklendi
                    float moneyToEarn = (removedGem.gemType.InitialPrice + (removedGem.transform.localScale.x * 100));
                    DataManager.EarningMoney(moneyToEarn);

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
            Gem gemToCarry = other.GetComponent<Gem>();
            gemToCarry.isUpdateEnable = false;

            //Gem i �antaya ekle
            AddGem(gemToCarry);

            //Topland�ktan sonra yeni gem olu�tur                        
            gemToCarry.tile.CreateGem();
            
            isStacked = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("SellingArea")) return;

        if (gems.Count > 0 && !isSold)
        {
            isSold = true;

            //Gem i s�rttan indirme
            RemoveGem();                        
        }
    }
}
