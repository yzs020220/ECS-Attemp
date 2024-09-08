using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
public partial struct CubeSystem: ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        //state.EntityManager.CreateEntity();
        var entityManager = state.EntityManager;
        var entities = entityManager.GetAllEntities(Allocator.Temp);

        foreach(var e in entities)
        {
            if (entityManager.HasComponent<CubeComponent>(e))
            {
                var cube = entityManager.GetComponentData<CubeComponent>(e);
                var localTransform = entityManager.GetComponentData<LocalTransform>(e);
                var moveDist = cube.moveDir * cube.moveSpd * SystemAPI.Time.DeltaTime;
                localTransform.Position += moveDist;
                entityManager.SetComponentData<LocalTransform>(e, localTransform);
                if (cube.moveSpd > 0) cube.moveSpd -= 2f * SystemAPI.Time.DeltaTime;
                else cube.moveSpd = 0;
                entityManager.SetComponentData<CubeComponent>(e, cube);
            }
        }
    }
}
