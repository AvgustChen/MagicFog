using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource walk;
    [SerializeField] private AudioClip[] attack;

    private void Start()
    {
        PlayerControl.Instance.AttackEvent += PlayerControl_AttackEvent;
        PlayerControl.Instance.WalkSoundEvent += PlayerControl_WalkSoundEvent;
        PlayerControl.Instance.IdleSoundEvent += PlayerControl_IdleSoundEvent;   
    }

    private void PlayerControl_IdleSoundEvent(object sender, EventArgs e)
    {
        if(walk.isPlaying)
        {
            walk.Stop();
        }
    }

    private void PlayerControl_WalkSoundEvent(object sender, EventArgs e)
    {
        if(!walk.isPlaying)
        {
            walk.Play();
        }
    }

    private void PlayerControl_AttackEvent(object sender, EventArgs e)
    {
        int rand = UnityEngine.Random.Range(0, attack.Length);
        AudioSource.PlayClipAtPoint(attack[rand], PlayerControl.Instance.transform.position, 1f);
    }
}
