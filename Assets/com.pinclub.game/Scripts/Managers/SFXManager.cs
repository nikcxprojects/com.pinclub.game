using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] AudioSource sfxSource;

    private void Start()
    {
        //Block.OnCollisionEnter += () => 
        //{
        //    if (sfxSource.isPlaying)
        //    {
        //        sfxSource.Stop();
        //    }

        //    sfxSource.Play();
        //};
    }
}
