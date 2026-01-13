// *** Implementation for triggering VFX + SFX for weapons ***
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class WeaponEffects : MonoBehaviour
{
    [SerializeField] private AudioClip shootAudio;
    [SerializeField] private GameObject[] muzzleFlashes;
    [SerializeField] private ParticleSystem smokeEffect;
    private AudioSource sfx;
    
    private void Start() => sfx = GetComponent<AudioSource>();
    public void PlayAudio_Shoot() => sfx.PlayOneShot(shootAudio);
    public void MuzzleFlash(Vector3 pos, Quaternion rot) {
        _ = Instantiate(muzzleFlashes[Random.Range(0, muzzleFlashes.Length - 1)], pos, rot);
        _ = Instantiate(smokeEffect, pos, rot);
    }
}