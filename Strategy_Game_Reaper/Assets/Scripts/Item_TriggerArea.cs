using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_TriggerArea : MonoBehaviour
{
    [SerializeField] PlayerMovement _ref_P;
    [SerializeField] GameObject _itemList;

    private void Update()
    {
        DropItem();
    }
    private void OnTriggerEnter(Collider other)
    {
       if(other.tag=="Player") GetPickedUp();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player") GetPickedUp();
    }
    void GetPickedUp()
    {
        if (transform.parent != null)
        {
            if (_ref_P.PickItem)
            {
                transform.SetParent(_ref_P.transform);
            }
        }

    }
    void DropItem()
    {
        if (!_ref_P.PickItem&&transform.parent!=null)
        {
            transform.SetParent(_itemList.transform);
        }
    }
}
