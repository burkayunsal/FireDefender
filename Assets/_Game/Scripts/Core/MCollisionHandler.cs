using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class MCollisionHandler : MonoBehaviour
{
    Dictionary<string, UnityEvent> dicEvts = new Dictionary<string, UnityEvent>();

    [SerializeField] CollisionRules[] rules;

    private void Start()
    {
        foreach(CollisionRules cr in rules)
        {
            dicEvts.Add(cr._tag, cr.evt);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(dicEvts.ContainsKey(collision.collider.gameObject.tag))
        {
            if(dicEvts.TryGetValue(collision.collider.tag,out UnityEvent evt))
            {
                evt?.Invoke();
            }
        }
    }

    [System.Serializable]
    public class CollisionRules
    {
        public string _tag;
        public UnityEvent evt;
    }
}
