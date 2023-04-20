using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectInteractivity : MonoBehaviour
{
    RaycastHit hit;
    TextMeshProUGUI textBox;
    GameObject prevSelectedObj;
    [SerializeField] Material defaultMat, highlighter;

    void Start()
    {
        textBox = GetComponentInChildren<TextMeshProUGUI>(true);
    }

    void Update()
    {
        if(Input.touchCount>0||Input.GetMouseButtonDown(0))
        {
            if(prevSelectedObj!=null)
                prevSelectedObj.GetComponent<MeshRenderer>().material = defaultMat;
            Vector3 vec = new Vector3();
            if (Input.touchCount > 0)
                vec = Input.GetTouch(0).position;
            else if (Input.GetMouseButtonDown(0))
                vec = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(vec);
            if (Physics.Raycast(ray, out hit))
            {
                transform.GetChild(0).gameObject.SetActive(true);
                textBox.text = hit.collider.gameObject.name;
                switch(hit.collider.gameObject.name)
                {
                    case "SOLID.010":
                        textBox.text = "cooling fan";
                        break;
                    case "SOLID.023":
                        textBox.text = "end bell";
                        break;
                    case "SOLID.003":
                        textBox.text = "bearing";
                        break;
                    case "SOLID.014":
                    case "SOLID.015":
                        textBox.text = "fan cover";
                        break;
                    case "SOLID.021":
                        textBox.text = "cast iron frame";
                        break;
                    case "SOLID.018":
                        textBox.text = "squirrel cage rotor";
                        break;
                }
                prevSelectedObj = hit.collider.gameObject;
                prevSelectedObj.GetComponent<MeshRenderer>().material = highlighter;
            }
        }
    }
}
