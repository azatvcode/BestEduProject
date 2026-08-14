using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    public void OnFootstep()
    {
        _audioSource.Play();
    }
}