using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

partial struct UnitMoverSystem : ISystem
{

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach((
            RefRW<LocalTransform> localTransform, 
            RefRO<MoveSpeed> MoveSpeed)
            in SystemAPI.Query<
                RefRW<LocalTransform>,
                RefRO<MoveSpeed>>()) //Query everything that has a LocalTransform and MoveSpeed
        {
            //Run on every single entity that has a localTransform and moveSpeed component
            //Grabs localTransform, moveSpeed moving right by 1 unit
            localTransform.ValueRW.Position = localTransform.ValueRO.Position + new float3(MoveSpeed.ValueRO.value, 0, 0) * SystemAPI.Time.DeltaTime;
        }
    }

}
