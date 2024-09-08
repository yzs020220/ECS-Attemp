using Unity.Entities;
using UnityEngine;

public class CubeSpanwerAuthoring : MonoBehaviour
{
    public GameObject prefab;
    public float spawnRate;
}

public class CubeSpawnBaker: Baker<CubeSpanwerAuthoring>
{
    public override void Bake(CubeSpanwerAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.None);

        AddComponent(entity, new CubeSpawnerComponent
        {
            prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic),
            spawnPos = authoring.transform.position,
            spawnRate = authoring.spawnRate,
            nextSpawnTime = 0f,
        });
    }
}
