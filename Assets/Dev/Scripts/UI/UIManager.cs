using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.Extension;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject statisticsPanel;
    [SerializeField] GameObject statisticItemPrefab;
    [SerializeField] Transform statisticItemHolder;

    [SerializeField] TextMeshProUGUI moneyText;

    private void Start()
    {
        CloseStatisticPanel();
        UpdateMoneyTextUI();
    }

    public void ShowUIStatistics()
    {
        //Statistic paneli temizle
        for (int i = 0; i < statisticItemHolder.childCount; i++)
        {
            Destroy(statisticItemHolder.GetChild(i).gameObject);
        }        

        //Statics leri panele yazdýr
        int gemCount;
        GameManager.Instance.availableGemTypes.ForEach(gemType =>
        {
            GameObject statisticItem = Instantiate(statisticItemPrefab, statisticItemHolder);

            statisticItem.transform.Find("Icon").GetComponent<Image>().sprite = gemType.Icon;
            statisticItem.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = $"Gem Type: {gemType.Name}";

            DataManager.Data.GemStatistics.TryGetValue(gemType.Name, out gemCount);
            statisticItem.transform.Find("CollectedCount").GetComponent<TextMeshProUGUI>().text = "Collected Count:" + gemCount;
        });
    }

    public void UpdateMoneyTextUI()
    {
        moneyText.text = DataManager.Data.Money.ToShortFloat();
        moneyText.gameObject.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), .3f).SetLoops(2, LoopType.Yoyo);
    }

    public void OpenStatisticsPanel()
    {
        statisticsPanel.transform
            .DOScale(Vector3.one, .3f)
            .SetEase(Ease.OutBounce);
        
        ShowUIStatistics();
    }

    public void CloseStatisticPanel()
    {
        statisticsPanel.transform
            .DOScale(Vector3.zero, .3f);
    }
}
