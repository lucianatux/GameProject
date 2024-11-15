using UnityEngine;

public class SpeakerOnOff : MonoBehaviour, IInteractable
{
    private bool isMuted = false;

    public void Interact()
    {
        // Alternar el estado de mute
        isMuted = !isMuted;

        // Mutea o desmutea el AudioSource de BgMusic
        var bgMusic = GameObject.Find("BgMusic")?.GetComponent<AudioSource>();
        if (bgMusic != null)
        {
            bgMusic.mute = isMuted;
        }

        // Acceder al Animator del padre y ajustar el parámetro "On"
        var parentAnimator = transform.parent?.GetComponent<Animator>();
        if (parentAnimator != null)
        {
            parentAnimator.SetBool("On", !isMuted);
        }
    }
}

