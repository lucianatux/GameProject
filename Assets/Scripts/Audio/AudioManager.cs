using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundEffect
{
    Jump,
    Push,
    LifePoint,
    Mosquito,
}

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource; // Fuente de audio
    public AudioClip[] soundEffects; // Array de efectos de sonido

    // Reproduce un sonido basado en su tipo
    public void PlaySound(SoundEffect sound)
    {
        switch (sound)
        {
            case SoundEffect.Jump:
                audioSource.PlayOneShot(soundEffects[0]); // Asumiendo que Jump es el primer sonido en el array
                break;
            case SoundEffect.Push:
                audioSource.PlayOneShot(soundEffects[1]);
                break;
            case SoundEffect.LifePoint:
                audioSource.PlayOneShot(soundEffects[2]);
                break;
            case SoundEffect.Mosquito:
                audioSource.PlayOneShot(soundEffects[3]);
                break;
            // Agrega más casos según los sonidos que quieras gestionar
        }
    }
}
