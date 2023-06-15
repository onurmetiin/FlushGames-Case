using Assets.Dev.Scripts.Tiles;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    public List<Gem> gems = new List<Gem>();
    [SerializeField] Transform AddingPos, RemovingPos;

    public StackManager()
    {
        gems = new List<Gem>();
    }

    public void AddGem(Gem gem)
    {
        //gem listeye ekledik
        gems.Add(gem);

        //gem taþýma iþlemi
        Tween addingTween = gem.gameObject.transform
            .DOJump(new Vector3(AddingPos.position.x, AddingPos.position.y + (gems.Count), AddingPos.position.z), 5, 1, .3f)
            .OnComplete(() => 
            { 
                //gem parenti stickmanBag ayarlandý
                gem.gameObject.transform.SetParent(AddingPos.transform);
                gem.gameObject.transform.localPosition = new Vector3(0, AddingPos.position.y + (gems.Count),0);                
            });          
    }

    public void RemoveGem()
    {
        if (gems.Count > 0)
        {
            //gem listeden silindi
            Gem removedGem = gems[gems.Count - 1];
            gems.RemoveAt(gems.Count - 1);

            //gem satma animasyonu
            Tween removeTween = removedGem.gameObject.transform
                .DOJump(RemovingPos.position, 5, 1, 2f)
                .OnUpdate(() => 
                    {
                        removedGem.gameObject.transform.DOScale(Vector3.zero, 3f);  
                    })
                .OnComplete(() => 
                    { 
                        //gem parenti null a çekildi
                        removedGem.transform.localPosition= RemovingPos.position;
                        removedGem.transform.parent = null;
                        Destroy(removedGem.gameObject);
                    });
            //removeTween.Kill();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gem"))
        {
            Debug.Log("Çarptim");
            Gem gemToCarry = other.GetComponent<Gem>();
            //AddGem(gemToCarry);
            Debug.Log("Gem toplandý");
            gemToCarry.tile.CreateGem();
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("SellingArea")) return;        
            
        float counter = 5f;
        counter -= Time.time;
        if (counter<=0 && gems.Count>0)
        {
            RemoveGem();
            counter = 5f;
            Debug.Log("Gem Satýldý");
        }
        
    }
}
