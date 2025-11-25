
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrushTransitionManager : MonoBehaviour
{
    public static BrushTransitionManager Instance;

    [Header("Particle Prefabs")]
    public GameObject TransitionInFX;
    public GameObject TransitionOutFX;

    [Header("Timings")]
    public float TransitionInTime = 2f;
    public float TransitionOutTime = 2f;

    private void Awake()
    {
        // Singleton so it persists between scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    // -----------------------------
    // PLAY TRANSITION IN (Scene Start)
    // -----------------------------
    public void PlayTransitionIn()
    {
        StartCoroutine(DoTransitionIn());
    }

    IEnumerator DoTransitionIn()
    {
        InstantiateFX(TransitionInFX);
        yield return new WaitForSeconds(TransitionInTime);
    }

    // -----------------------------
    // PLAY TRANSITION OUT (Before Scene Change)
    // -----------------------------
    public void TransitionOutAndLoad(string sceneName)
    {
        StartCoroutine(DoTransitionOut(sceneName));
    }

    private IEnumerator DoTransitionOut(string sceneName)
    {
        InstantiateFX(TransitionOutFX);
        yield return new WaitForSeconds(TransitionOutTime);
        SceneManager.LoadScene(sceneName);
    }

    // -----------------------------
    // Helper: Spawn FX At Camera
    // -----------------------------
    private void InstantiateFX(GameObject fxPrefab)
    {
        if (fxPrefab == null) return;

        Camera cam = Camera.main;
        if (cam == null) return;

        Instantiate(fxPrefab, cam.transform.position, Quaternion.identity);
    }
}
