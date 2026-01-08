// *** Handles player input for WEAPON-based actions ***
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerWeaponInput : MonoBehaviour
{
    private PlayerInput input;
    public PlayerInput.CombatActions combatActions;
    private PlayerWeapon weaponController;

    private void Awake()
    {
        input = new PlayerInput();
        combatActions = input.Combat;
        weaponController = GetComponent<PlayerWeapon>();
    }
    private void OnEnable()
    {
        combatActions.Attack.performed += OnAttackPerformed;
        combatActions.Attack.canceled += OnAttackPerformed;
        input.Enable();
    }
    private void OnDisable()
    {
        combatActions.Attack.performed -= OnAttackPerformed;
        combatActions.Attack.canceled -= OnAttackPerformed;
        input.Disable();
    }

    private void OnAttackPerformed(InputAction.CallbackContext ctx) => weaponController.Attack(ctx.performed);
    private void OnReloadPerformed(InputAction.CallbackContext ctx) => weaponController.Reload();
}
