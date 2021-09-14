using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeShotFeedback : MonoBehaviour
{

    private bool isCharging;
    private float subValue;

    // Start is called before the first frame update
    void Start()
    {
        isCharging = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCharging && this.GetComponent<Slider>().value < 1)
        {
            this.GetComponent<Slider>().value = Time.time - subValue;
        }
    }

    public void BeginCharge()
    {
        isCharging = true;
        subValue = Time.time;
    }

    public void EndCharge()
    {
        isCharging = false;
        this.GetComponent<Slider>().value = 0;
    }
}
