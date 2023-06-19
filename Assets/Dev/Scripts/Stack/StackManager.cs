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
        //gem listeye ekleme iþlemi
        gems.Add(gem);

        //gem parenti stickmanBag ayarlandý
        gem.gameObject.transform.SetParent(AddingPos.transform);

        //gem taþýma iþlemi
        gem.gameObject.transform
            .DOLocalJump(new Vector3(0, gems.Count, 0), 5, 1, .3f)
            .OnComplete(() => 
            {
                //toplanýlan gem database de artýrýldý
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
                    //kazanýlan para database e eklendi
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

            //Gem i çantaya ekle
            AddGem(gemToCarry);

            //Toplandýktan sonra yeni gem oluþtur                        
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

            //Gem i sýrttan indirme
            RemoveGem();                        
        }
    }
}
