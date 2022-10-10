
using DG.Tweening;
using MyBox;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    [SerializeField] Transform target;
    [SerializeField] bool useEditorOffset;
    [ConditionalField(nameof(useEditorOffset), false, false)]
    [SerializeField] Vector3 offset;
    bool isInitialized = false;
    [SerializeField] private float lerpTime;

    public bool hasSwitched = false;
    private Transform PlayerTransform;
    public void SetTarget(Transform t) => target = t;

    public void SetOffset(Vector3 endVal) => offset = endVal;
    public void SetOffset(Vector3 endVal, float time)
    {
        DOTween.To(() => offset, (x) => offset = x, endVal, time).SetEase(Ease.Linear);
    }

    delegate void OnUpdate();
    OnUpdate onUpdate;

    private void Start()
    {
        PlayerTransform = target;
        Initialize();
    }

    void Initialize()
    {
        if (target == null) return;

        if (useEditorOffset)
        {
            SetOffset(transform.position - target.position);
        }

        isInitialized = true;
    }

    public void LateUpdate()
    {
        if (isInitialized)
        {
            onUpdate?.Invoke();
        }
    }

    public void OnGameStarted()
    {
        onUpdate = FollowTarget;
    }

    void FollowTarget()
    {
        transform.position = target.position + offset;
    }

    void FollowWithLerp()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, lerpTime);
    }

    public void SetPlayerAsTarget()
    {
        target = PlayerTransform;
    }
}
