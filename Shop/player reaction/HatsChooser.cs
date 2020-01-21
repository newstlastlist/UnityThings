using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HatsChooser : MonoBehaviour
{
    public GameObject[] hats;
    private GameObject _currentHat;
    [SerializeField] private Mediator _mediator;


    void Start()
    {

        
        if(gameObject.tag == "player")
        {
            _mediator.Subscribe<ChooseHatCommand>(ApplyHat);
            GameObject tempPrefab = new GameObject("EmptyHat");
            for (int i = 0; i < Shop.Instance.ShopItem.Length; i++)
            {
                if(Shop.Instance.ShopItem[i].hatIDinLocalStorage == system.LocalStorage.LastChoosedHatID)
                {
                    tempPrefab = Shop.Instance.ShopItem[i].hatPrefab;
                    break;
                }
            }
            _currentHat = Instantiate(tempPrefab, transform.position, Quaternion.identity);
        }
        else 
            _currentHat = Instantiate(hats[UnityEngine.Random.Range(0, hats.Length)], transform.position, Quaternion.identity);
        
        if(gameObject.tag == "SideSnowMan" && _currentHat.name == "Witch Hat(Clone)") // 999999 vertex
        {
            Destroy(_currentHat);
        }
        else
        {
            _currentHat.transform.SetParent(transform);
            _currentHat.transform.localScale = Vector3.one;
            _currentHat.transform.localEulerAngles = Vector3.zero;
            _currentHat.SetActive(true);

            
        }

    }

   

    private void ApplyHat(ChooseHatCommand hat)
    {
        if(gameObject.tag == "player")
        {
            if(_currentHat != null)
                Destroy(_currentHat);
            _currentHat = Instantiate(hat.ShopHat.hatPrefab, transform.position, Quaternion.identity);
            _currentHat.transform.SetParent(transform);
            _currentHat.transform.localScale = Vector3.one;
            _currentHat.transform.localEulerAngles = Vector3.zero;
            _currentHat.SetActive(true);


            system.LocalStorage.LastChoosedHatID = hat.ShopHat.hatIDinLocalStorage;

        }
    }

  
}
