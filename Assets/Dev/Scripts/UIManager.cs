using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using Assets.Dev.Scripts.Persistence;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject statisticsPanel;
    [SerializeField] GameObject statisticItemPrefab;
    [SerializeField] Transform statisticItemHolder;

    [SerializeField] GameObject EarnedAmount;

    private void Start()
    {
        CloseStatisticPanel();
        DatabaseCheck();
    }

    void DatabaseCheck() 
    {
        GameManager.Instance.availableGemTypes.ForEach(gemType => 
        {
            Debug.Log("Gem Name: " + gemType.Name);
            // (Clone) Gem adýndan atýlacak
            if (!PlayerDatabase.Instance.gemStatistics.ContainsKey(gemType.Name))
            {
                PlayerDatabase.Instance.gemStatistics.Add(gemType.Name, 0);
            }

            PlayerDatabase.SaveData();
        });
    }

    public void EarningMoneyEffect()
    {

        TextMeshPro amountText = EarnedAmount.GetComponentInChildren<TextMeshPro>();
        amountText.color = Color.white;

        Color targetColor = amountText.color;
        targetColor.a = 0f;

        amountText.gameObject.transform.DOLocalMoveY(2, 1f);

        amountText.gameObject.transform.localPosition = Vector3.zero;
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

    public void ShowUIStatistics()
    {
        GameManager.Instance.availableGemTypes.ForEach(gemType =>
        {
            GameObject go= Instantiate(statisticItemPrefab, statisticItemHolder);
            go.transform.Find("Icon").GetComponent<Image>().sprite = gemType.Icon;
            go.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = $"Gem Type: {gemType.Name}";
            go.transform.Find("CollectedCount").GetComponent<TextMeshProUGUI>().text = "Collected Count:" + PlayerDatabase.Instance.gemStatistics[gemType.Name].ToString();
            //$"Collected Count: {PlayerPrefs.GetInt(gemType.Name)}";
            Debug.Log(gemType.Name + " in toplanýlan gem sayýsý : " + PlayerPrefs.GetInt(gemType.Name));
        });
    }
}
