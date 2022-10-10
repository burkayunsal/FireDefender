using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [HideInInspector] public float Speed;
    [HideInInspector] public float dragSpeed = 0f;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private SkinnedMeshRenderer boundarySkinMesh, harvesterSkinMesh;
    [SerializeField] public CapsuleCollider _collider;
    private float initialRadius, initialHeight;
    public ParticleSystem[] plyayerDustParticles;

    public void OnGameStarted()
    {
    }

    public Vector3 GetPosition() => transform.position;
    public void ForceStop()
    {
        dragSpeed = 0f;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
    }
    
    private void Start()
    {
        initialRadius = _collider.radius;
        initialHeight = _collider.height;
        InitPlayer();
    }

    void InitPlayer()
    {
        Speed = Configs.Player.speed[SaveLoadManager.GetSpeedLevel()];
        SetSize();
    }

    private void Update()
    {
        if (GameManager.isRunning)
        {
            Move();
        }
    }

    public void Move()
    {
        rb.velocity = rb.transform.forward * Speed * dragSpeed;
    }

    public void Stop()
    {
        rb.velocity = Vector3.zero;
    }

    public void Rotate(Quaternion rot)
    {
        rb.rotation = rot;
    }

    public void SetSize()
    {
        float sizeValue = Configs.Player.size[SaveLoadManager.GetSize()]; 
        harvesterSkinMesh.SetBlendShapeWeight(0,sizeValue);
        boundarySkinMesh.SetBlendShapeWeight(0,sizeValue);

        var sizeMultiplier = (sizeValue / 100f) + 1;
        _collider.radius = initialRadius * sizeMultiplier;
        _collider.height = initialHeight * sizeMultiplier;
    } 
}
