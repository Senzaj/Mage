using System.Collections.Generic;
using UnityEngine;

namespace Sources.Modules.Pools.Scripts
{
    public abstract class Pool<T> : MonoBehaviour
    {
        [SerializeField] protected List<T> Prefabs;
        [SerializeField] protected int StartCapacity;
    }
}