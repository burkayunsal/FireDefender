using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [HideInInspector] public float Speed;
    [HideInInspector] public float dragSpeed = 0f;

    [SerializeField] private Animator anim;
    [SerializeField] public Rigidbody rb;
    [SerializeField] private SkinnedMeshRenderer boundarySkinMesh;
    [SerializeField] private MeshRenderer[] harvesterSkinMeshes;
    [SerializeField] private GameObject[] speedUpgrades;
    
    //[SerializeField] public CapsuleCollider _collider;
    //private float initialRadius, initialHeight;
    public ParticleSystem[] plyayerDustParticles;
    [SerializeField] private GameObject FullText;

    public bool isInGround;
    public void OnGameStarted()
    {
        anim.SetFloat("Speed", 0.7f);
        rb.isKinematic = false;
    }
    
    public void ForceStop()
    {
        dragSpeed = 0f;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
    }
    
    private void Start()
    {
       // initialRadius = _collider.radius;
        //initialHeight = _collider.height;
        InitPlayer();
    }

    void InitPlayer()
    {
        Speed = Configs.Player.speed[SaveLoadManager.GetSpeedLevel()];
        SetSize();
        SetSpeedUpgrades();
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

    public void Rotate(Quaternion rot)
    {
        rb.rotation = rot;
    }

    public void SetSize()
    {
        float sizeValue = Configs.Player.size[SaveLoadManager.GetSize()]; 
        for (int i = 0; i < harvesterSkinMeshes.Length; i++)
        {
            harvesterSkinMeshes[i].gameObject.SetActive(i == SaveLoadManager.GetSize());
        }
        boundarySkinMesh.SetBlendShapeWeight(0,sizeValue);
    }

    public void SetSpeedUpgrades()
    {
        for (int i = 0; i < SaveLoadManager.GetSpeedLevel(); i++)
        {
            if (!speedUpgrades[i].activeSelf)
            {
                speedUpgrades[i].SetActive(true);
            }
        }
    }
    [SerializeField] private LineController lc;

    public void playerCapacityisFull(bool isFull)
    {
        if (isFull && !FullText.gameObject.activeSelf)
        {
            FullText.gameObject.SetActive(true);

            if (!SaveLoadManager.HasSellTutShown())
            {
                SaveLoadManager.SetSellTutDone();
                EnableLine(true);
            }
        }
        else if (!isFull && FullText.gameObject.activeSelf)
        {
            FullText.gameObject.SetActive(false);
        }
    }

    public void EnableLine(bool b)
    {
        lc.gameObject.SetActive(b);
    }
}
