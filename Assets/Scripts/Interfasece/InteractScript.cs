using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour, IInteractable
{
    private Droped _drop;
    private void Awake()
    {

        _drop = GetComponent<Droped>();

    }

    public void Interact()
    {
        if (_drop == null)
            return;

        _drop.DropThis();
    }

}
