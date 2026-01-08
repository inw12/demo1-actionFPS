// *** Implementation for triggering VFX + SFX for weapons ***
using UnityEngine;
public class WeaponEffects : MonoBehaviour
{
    [SerializeField] private GameObject[] muzzleFlash;
    [SerializeField] private AudioClip shootAudio;
    private AudioSource sfx;
    
    private void Start() => sfx = GetComponent<AudioSource>();
    
    public void PlayMuzzleFlash(Transform spawn) => _ = Instantiate(muzzleFlash[Random.Range(0, muzzleFlash.Length - 1)], spawn.position, transform.rotation);
    public void PlayAudio_Shoot() => sfx.PlayOneShot(shootAudio);
}