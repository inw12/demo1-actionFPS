using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string promptMessage = "Interact (E)";

    public void Interact() => Interaction();
    protected virtual void Interaction() {}
}
