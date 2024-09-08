using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[BurstCompile]
public partial struct CubeSpawnerSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        if (!SystemAPI.TryGetSingletonEntity<CubeSpawnerComponent>(out Entity spawnerEntity))
        {
            return;
        }

        RefRW<CubeSpawnerComponent> spawner = SystemAPI.GetComponentRW<CubeSpawnerComponent>(spawnerEntity);

        //state.EntityManager.CreateEntity();
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);

        if (spawner.ValueRO.nextSpawnTime < SystemAPI.Time.ElapsedTime)
        {
            Entity cubeEntity = ecb.Instantiate(spawner.ValueRO.prefab);
            ecb.AddComponent(cubeEntity, new CubeComponent
            {
                moveDir = Random.CreateFromIndex((uint)(SystemAPI.Time.ElapsedTime / SystemAPI.Time.DeltaTime)).NextFloat3(),
                moveSpd = 10f
            });

            spawner.ValueRW.nextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.spawnRate;
            ecb.Playback(state.EntityManager);
        }
    }
}
