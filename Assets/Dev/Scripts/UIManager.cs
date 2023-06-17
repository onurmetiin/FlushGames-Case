using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject StatisticsPanel;
    private void Start()
    {
        CloseStatisticPanel();
    }
    public void OpenStatisticsPanel()
    {
        StatisticsPanel.transform
            .DOScale(Vector3.one, .3f)
            .SetEase(Ease.OutBounce);
    }

    public void CloseStatisticPanel()
    {
        StatisticsPanel.transform
            .DOScale(Vector3.zero, .3f);
    }
}
