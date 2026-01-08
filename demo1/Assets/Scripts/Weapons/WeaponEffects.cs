// *** Implementation for triggering VFX + SFX for weapons ***
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class WeaponEffects : MonoBehaviour
{
    [SerializeField] private AudioClip shootAudio;
    private AudioSource sfx;
    
    private void Start() => sfx = GetComponent<AudioSource>();
    public void PlayAudio_Shoot() => sfx.PlayOneShot(shootAudio);
}