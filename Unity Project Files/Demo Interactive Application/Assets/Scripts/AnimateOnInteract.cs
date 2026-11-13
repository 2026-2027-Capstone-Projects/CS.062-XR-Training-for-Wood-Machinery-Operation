using System.Collections;
using UnityEngine;

/// <summary>
/// An Interactable that plays an animation when interacted with.
///
/// Why not reuse PressButton: PressButton sets transform.position to an
/// absolute world position, so once the button is parented to a tracked
/// QR code it would jump away from the QR code instead of animating in
/// place. This version only touches local transforms, so it stays anchored.
///
/// Put this on the button prefab, alongside a Collider.
/// </summary>
[RequireComponent(typeof(Collider))]
public class AnimateOnInteract : Interactable
{
    [Header("Animation")]
    [Tooltip("Optional. If empty, the built-in press-and-pulse is used.")]
    [SerializeField] private Animator animator;

    [Tooltip("Trigger parameter name on the Animator.")]
    [SerializeField] private string animatorTrigger = "Play";

    [Header("Built-in animation")]
    [Tooltip("How far the button sinks when pressed, in meters.")]
    [SerializeField] private float pressDepth = 0.01f;

    [Tooltip("How much the button grows at the peak of the pulse.")]
    [SerializeField] private float pulseScale = 1.3f;

    [Tooltip("Length of the built-in animation in seconds.")]
    [SerializeField] private float duration = 0.45f;

    private Vector3 baseLocalPosition;
    private Vector3 baseLocalScale;
    private bool isAnimating;

    private void Awake()
    {
        baseLocalPosition = transform.localPosition;
        baseLocalScale = transform.localScale;

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    /// <summary>
    /// The callback. Called by ARTouchInteractor on mobile, and by whatever
    /// the VR side uses, since both go through Interactable.
    /// </summary>
    public override void OnInteract()
    {
        base.OnInteract();

        Debug.Log("[AnimateOnInteract] Callback fired, playing animation.");

        if (animator != null)
        {
            animator.SetTrigger(animatorTrigger);
            return;
        }

        if (!isAnimating)
        {
            StartCoroutine(PressAndPulse());
        }
    }

    private IEnumerator PressAndPulse()
    {
        isAnimating = true;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float wave = Mathf.Sin(t * Mathf.PI);

            transform.localPosition = baseLocalPosition - new Vector3(0f, pressDepth * wave, 0f);
            transform.localScale = baseLocalScale * (1f + (pulseScale - 1f) * wave);

            yield return null;
        }

        transform.localPosition = baseLocalPosition;
        transform.localScale = baseLocalScale;
        isAnimating = false;
    }
}
