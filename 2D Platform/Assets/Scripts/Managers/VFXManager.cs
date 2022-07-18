using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Core.Singleton;

public class VFXManager : Singleton<VFXManager>
{
    public List<VFXSetup> setupList;

    public void PlayVFXByType(VFXType type, Vector3 position, Transform parent = null)
    {
        foreach(var setup in setupList)
        {
            if (setup.type == type)
            {
                if (parent == null)
                    parent = transform;

                var pfb = Instantiate(setup.prefab, parent);

                if(pfb != null)
                    pfb.transform.position = position;

                break;
            }
        }
    }
}

public enum VFXType
{
    JUMP,
    GUN
}

[System.Serializable]
public class VFXSetup
{
    public VFXType type;
    public GameObject prefab;
}


