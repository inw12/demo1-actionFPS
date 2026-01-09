using UnityEngine;
public class Interactable : MonoBehaviour
{
    public enum InteractionType {
        Proximity, Vision
    }
    public InteractionType interactionType;
    public string promptMessage = "Interact (E)";

    public void Interact() => Interaction();
    protected virtual void Interaction() {}
}