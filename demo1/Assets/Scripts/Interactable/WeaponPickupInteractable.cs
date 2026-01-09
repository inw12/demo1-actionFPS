using UnityEngine;
public class WeaponPickupInteractable : Interactable
{
    protected override void Interaction()
    {
        Debug.Log("Proximity Interaction Works!");
    }
}
