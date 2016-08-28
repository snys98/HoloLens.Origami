using UnityEngine;

public class SphereSounds : MonoBehaviour
{
    AudioSource audioSource = null;
    AudioClip impactClip = null;
    AudioClip rollingClip = null;


    private float _forceVolumeFactor = 1.2f;

    void Start()
    {
        // Add an AudioSource component and set up some defaults
        audioSource = gameObject.GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialize = true;
        audioSource.spatialBlend = 1.0f;
        audioSource.dopplerLevel = 0.0f;
        audioSource.volume = 0.5f;
        audioSource.rolloffMode = AudioRolloffMode.Custom;

        // Load the Sphere sounds from the Resources folder
        impactClip = Resources.Load<AudioClip>("Impact");
        rollingClip = Resources.Load<AudioClip>("Rolling");
    }

    // Occurs when this object starts colliding with another object
    void OnCollisionEnter(Collision collision)
    {
        // Play an impact sound if the sphere impacts strongly enough.
        if (collision.relativeVelocity.magnitude >= 0.1f)
        {
            audioSource.clip = impactClip;
            audioSource.volume = collision.relativeVelocity.magnitude * _forceVolumeFactor;
            audioSource.Play();
        }
    }

    // Occurs each frame that this object continues to collide with another object
    void OnCollisionStay(Collision collision)
    {
        //Rigidbody rigid = this.gameObject.GetComponent<Rigidbody>();

        //// Play a rolling sound if the sphere is rolling fast enough.
        //if (rigid.velocity.magnitude >= 0.01f)
        //{
        //    audioSource.volume = collision.relativeVelocity.magnitude * _forceVolumeFactor / 2;
        //    if (audioSource.isPlaying)
        //    {
        //        return;
        //    }
        //    audioSource.clip = rollingClip;
        //    audioSource.Play();
        //}
        //// Stop the rolling sound if rolling slows down.
        //else if (rigid.velocity.magnitude < 0.01f)
        //{
        //    audioSource.Stop();
        //}
    }

    // Occurs when this object stops colliding with another object
    void OnCollisionExit(Collision collision)
    {
        // Stop the rolling sound if the object falls off and stops colliding.
        audioSource.Stop();
        
    }
}