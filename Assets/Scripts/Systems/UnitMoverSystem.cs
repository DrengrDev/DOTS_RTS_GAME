using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Physics;

partial struct UnitMoverSystem : ISystem
{

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach((
            RefRW<LocalTransform> localTransform, //RW = Read/Write
            RefRO<MoveSpeed> moveSpeed, //RO = Read Only
            RefRW<PhysicsVelocity> physicsVelocity)
            in SystemAPI.Query<
                RefRW<LocalTransform>,
                RefRO<MoveSpeed>,
                RefRW<PhysicsVelocity>>()) //Query everything that has these components
        {
            //Run on every single entity that has a localTransform, moveSpeed, and PhysicsVelocity component
            float3 targetPosition = localTransform.ValueRO.Position + new float3(10, 0, 0); //float3 is essentially equivalent to Vector3
            float3 moveDirection = targetPosition - localTransform.ValueRO.Position;
            moveDirection = math.normalize(moveDirection);

            localTransform.ValueRW.Rotation = quaternion.LookRotation(moveDirection, math.up());//Think of it as vector3.up

            physicsVelocity.ValueRW.Linear = moveDirection * moveSpeed.ValueRO.value;
            physicsVelocity.ValueRW.Angular = float3.zero;
            //localTransform.ValueRW.Position += moveDirection * moveSpeed.ValueRO.value * SystemAPI.Time.DeltaTime;
        }
    }

}
