using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingItems : MonoBehaviour
{
    public GameObject[] items;
    int activeItem;

    void Start()
    {
        activeItem = Random.Range(0, items.Length);
    }

    public void ItemsDropped()
    {
        Instantiate(items[activeItem], transform.position, Quaternion.identity);
    }
}
