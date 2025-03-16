using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource walk;
    [SerializeField] private AudioClip[] attack;

    private void Start()
    {
        Player.Instance.AttackEvent += Player_AttackEvent;
        Player.Instance.WalkSoundEvent += Player_WalkSoundEvent;
        Player.Instance.IdleSoundEvent += Player_IdleSoundEvent;   
    }

    private void Player_IdleSoundEvent(object sender, EventArgs e)
    {
        if(walk.isPlaying)
        {
            walk.Stop();
        }
    }

    private void Player_WalkSoundEvent(object sender, EventArgs e)
    {
        if(!walk.isPlaying)
        {
            walk.Play();
        }
    }

    private void Player_AttackEvent(object sender, EventArgs e)
    {
        int rand = UnityEngine.Random.Range(0, attack.Length);
        AudioSource.PlayClipAtPoint(attack[rand], Player.Instance.transform.position, 1f);
    }
}
