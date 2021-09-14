using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostumeChoice : MonoBehaviour
{

    public Sprite[] costumeList;

    private int currentCostume;
    private Image robotImage;

    // Start is called before the first frame update
    void OnEnable()
    {
        currentCostume = 0;
        robotImage = this.GetComponent<Image>();
    }

    public int GetCurrentCostumeIndex()
    {
        return currentCostume;
    }

    public Sprite GetCurrentCostume()
    {
        return costumeList[GetCurrentCostumeIndex()];
    }

    public void ShowNextCostume()
    {
        if (currentCostume+1 < costumeList.Length)
        {
            currentCostume++;
            robotImage.sprite = costumeList[currentCostume];
        }
    }

    public void ShowPreviousCostume()
    {
        if (currentCostume > 0)
        {
            currentCostume--;
            robotImage.sprite = costumeList[currentCostume];
        }
    }
}
