using SBF.Extentions.Numbers;
using SBF.Extentions.Vector;

using UnityEngine;
using UnityEngine.UI;

public class FireFollower : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    [SerializeField] Image Pointer;
    [HideInInspector] public Transform target;
    float minX,maxX;
    float minY, maxY;
    Camera cam;


    private void Start()
    { 
        cam = Camera.main;
        minX = Pointer.rectTransform.rect.size.x / 2f;
        maxX = Screen.width - minX;
        minY = (Pointer.rectTransform.rect.size.y / 2f) + 100f;
        maxY = Screen.height- (Pointer.rectTransform.rect.size.y / 2f) - 200f;
    }

    private void Update()
    {
        if(target != null && cam != null)
            ShowPointer();
    }

    void ShowPointer()
    {
        Vector3 pointerPos = cam.WorldToScreenPoint(target.position + offset).WithZ(0);

        HandleRotation(pointerPos.x.Between(minX, maxX) && pointerPos.y.Between(minY, maxY));

        pointerPos.x = Mathf.Clamp(pointerPos.x, minX, maxX);
        pointerPos.y = Mathf.Clamp(pointerPos.y, minY, maxY);
        Pointer.transform.position = pointerPos;
        
    }

    public void HidePointer()
    {
        target = null;
        Pointer.gameObject.SetActive(false);

    }
    
    void HandleRotation(bool isInViewport)
    {
        if (isInViewport)
        {
            Pointer.transform.localRotation = Quaternion.identity;
            Pointer.gameObject.SetActive(false);
        }
        else
        {
            Pointer.gameObject.SetActive(true);
            //Vector3 dif = target.position - PlayerController.I.GetPosition();
            //Vector3 normPos = dif.normalized;
            //float rot = (Mathf.Atan2(normPos.z, normPos.x) * Mathf.Rad2Deg) + 90f;
           // Pointer.transform.localRotation = Quaternion.Euler(Vector3.forward * rot);
        }
    }

}