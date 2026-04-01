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
            RefRO<UnitMover> unitMover, //RO = Read Only
            RefRW<PhysicsVelocity> physicsVelocity)
            in SystemAPI.Query<
                RefRW<LocalTransform>,
                RefRO<UnitMover>,
                RefRW<PhysicsVelocity>>()) //Query everything that has these components
        {
            //Run on every single entity that has a localTransform, moveSpeed, and PhysicsVelocity component
            float3 moveDirection = unitMover.ValueRO.targetPosition - localTransform.ValueRO.Position;
            moveDirection = math.normalize(moveDirection);

            localTransform.ValueRW.Rotation = 
            math.slerp(localTransform.ValueRO.Rotation, 
                quaternion.LookRotation(moveDirection, math.up()), 
                SystemAPI.Time.DeltaTime * unitMover.ValueRO.rotationSpeed);


            physicsVelocity.ValueRW.Linear = moveDirection * unitMover.ValueRO.moveSpeed;
            physicsVelocity.ValueRW.Angular = float3.zero; //float3 is essentially equivalent to Vector3
            //localTransform.ValueRW.Position += moveDirection * moveSpeed.ValueRO.value * SystemAPI.Time.DeltaTime;
        }
    }

}
