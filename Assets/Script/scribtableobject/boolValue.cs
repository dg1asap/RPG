using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class boolValue : ScriptableObject,ISerializationCallbackReceiver
{

    public bool initialValue;
    //[HideInInspector]
    public bool RuntimeValue;
    public void OnAfterDeserialize()
    {
        RuntimeValue = initialValue;
    }
    public void OnBeforeSerialize()
    {

    }
}
