using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusDomage : MonoBehaviour
{
    private RectTransform transformP1;
    private RectTransform transformP2;
    private RectTransform transformP3;
    private RectTransform transformP4;

    private float lifeTime;
    private float bonusDuration;

    private bool isUsed;

    // Start is called before the first frame update
    void Start()
    {
        transformP1 = GameObject.FindGameObjectWithTag("EmplacementBonus1").GetComponent<RectTransform>();
        transformP2 = GameObject.FindGameObjectWithTag("EmplacementBonus2").GetComponent<RectTransform>();
        transformP3 = GameObject.FindGameObjectWithTag("EmplacementBonus3").GetComponent<RectTransform>();
        transformP4 = GameObject.FindGameObjectWithTag("EmplacementBonus4").GetComponent<RectTransform>();
        isUsed = false;
        lifeTime = 0;
        bonusDuration = 5;
    }

    public void SendToPlayer(int number)
    {
        switch (number)
        {
            case 1:
                this.transform.SetParent(transformP1);
                break;
            case 2:
                this.transform.SetParent(transformP2);
                break;
            case 3:
                this.transform.SetParent(transformP3);
                break;
            case 4:
                this.transform.SetParent(transformP4);
                break;
        }
        this.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
        lifeTime = Time.time + bonusDuration;
        isUsed = true;
    }

    private void Update()
    {
        if(isUsed && Time.time > lifeTime)
        {
            Destroy(this.gameObject);
        }
    }
}
