using TMPro;
using UnityEngine;
public class WeaponAmmo : MonoBehaviour
{
    // Numbers
    [SerializeField] private int magSize;
    [SerializeField] private int ammoCount;
    private int currentMag;
    private int currentAmmo;
    // UI
    [SerializeField] private TextMeshProUGUI magText;
    [SerializeField] private TextMeshProUGUI ammoText;
    // Bool
    [HideInInspector] public bool hasAmmo;

    private void Start()
    {
        currentMag = magSize;
        currentAmmo = ammoCount;
        UpdateUI();
    }
    private void Update() => hasAmmo = currentMag > 0;
    public void UpdateAmmo()
    {
        currentMag--;
        UpdateUI();
    }
    public void Reload()
    {
        currentAmmo -= magSize - currentMag;
        currentMag = magSize;
        UpdateUI();
    }
    private void UpdateUI()
    {
        magText.text = currentMag.ToString();
        ammoText.text = currentAmmo.ToString();
    }
}
