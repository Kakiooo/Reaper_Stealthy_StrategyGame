
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrushTransitionManager : MonoBehaviour
{
    public static BrushTransitionManager Instance;

    [Header("Particle Prefabs")]
    public ParticleSystem TransitionInFX;
    public ParticleSystem TransitionOutFX;

    public void PlayOutTro()
    {
        TransitionOutFX.Play(); 
    }

}
