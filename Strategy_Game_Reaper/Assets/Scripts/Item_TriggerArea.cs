using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_TriggerArea : MonoBehaviour
{
    [SerializeField] PlayerMovement _ref_P;
    [SerializeField] GameObject _itemList;

    private void Update()
    {
        GetPickedUp();
    }
    private void OnTriggerEnter(Collider other)
    {
       if(other.tag=="Player") GetPickedUp();
    }
    void GetPickedUp()
    {
        if (transform.parent != null)
        {
            if (_ref_P.PickItem)
            {
                transform.SetParent(_ref_P.transform);
            }
            else
            {
                transform.SetParent(_itemList.transform);
            }
        }

    }
}
