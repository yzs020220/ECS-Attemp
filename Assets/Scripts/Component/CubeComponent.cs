using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct CubeComponent : IComponentData
{
    public float3 moveDir;
    public float moveSpd;
}
