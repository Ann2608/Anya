using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GirlHealthBar : MonoBehaviour
{
    [SerializeField] private AnyaHealth girlHealth;
    [SerializeField] private Image Health;
    [SerializeField] private Image HealthCurrent;

    private void Start()
    {
        Health.fillAmount = girlHealth.CurrentHealth / 20;
    }
    private void Update()
    {
        HealthCurrent.fillAmount = girlHealth.CurrentHealth / 20;
    }
}