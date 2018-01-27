using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable{

        bool canInteract();
        bool putItem();
        Item useWithoutItem();

}

