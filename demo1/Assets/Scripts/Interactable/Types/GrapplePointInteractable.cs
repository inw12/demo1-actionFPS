using UnityEngine;
public class GrapplePointInteractable : Interactable
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private float timeToTarget;
    [SerializeField] private float yOffset;

    protected override void Interaction()
    {
        Vector3 offset = new(0, yOffset, 0);
        playerMovement.Launch(transform.position + offset, timeToTarget);
    } 
}
