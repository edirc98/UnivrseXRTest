using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("Kendo Armor")]
    public GameObject KendoArmor;

    public void ToggleArmor()
    {
        if (KendoArmor.activeSelf)
        {
            KendoArmor.SetActive(false); 
        }
        else
        {
            KendoArmor.SetActive(true);
        }
    }
}
