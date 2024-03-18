using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{

    public Renderer rendArmorDo;
    public Renderer rendArmorBelt;
    public GameObject armorDo;
    public GameObject armorBelt;

    public Renderer rendHelmet;
    public GameObject helmet;

    //[SerializeField] private Color newColor;
    [SerializeField] private Color[] colors;
    private int colorValue;

    // Start is called before the first frame update
    void Start()
    {
        rendArmorDo = armorDo.GetComponent<Renderer>();
        rendArmorBelt = armorBelt.GetComponent<Renderer>();
        rendHelmet = helmet.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMaterial()
    {
        //rendArmorDo.material.color = newColor;
        //rendArmorBelt.material.color = newColor;
        //rendHelmet.material.color = newColor;

        colorValue++;
        if (colorValue > 3)
        {
            colorValue = 0;
        }

        rendArmorDo.material.color = colors[colorValue];
        rendArmorBelt.material.color = colors[colorValue];
        rendHelmet.material.color = colors[colorValue];
    }
}
