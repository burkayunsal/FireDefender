using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : Singleton<FireController>
{
    public List<Mob> lsMobs = new List<Mob>();
    public List<Meteor> lsMeteors = new List<Meteor>();

    public void AddMob(Mob m)
    {
        if (!lsMobs.Contains(m))
            lsMobs.Add(m);
    }
    
    public void RemoveMob(Mob m)
    {
        if (lsMobs.Contains(m))
            lsMobs.Remove(m);
    }
    
    public void AddMeteor(Meteor m)
    {
        if (!lsMeteors.Contains(m))
            lsMeteors.Add(m);
    }
    
    public void RemoveMeteor(Meteor m)
    {
        if (lsMeteors.Contains(m))
            lsMeteors.Remove(m);
    }
}
