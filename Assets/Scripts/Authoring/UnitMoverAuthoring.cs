using Unity.Entities;
using UnityEngine;

public class UnitMoverAuthoring : MonoBehaviour
{
    public float moveSpeed;

    public class Baker : Baker<UnitMoverAuthoring>
    {
        public override void Bake(UnitMoverAuthoring authoring) //Function runs during the bake process
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic); //Creates entity that has dots transform components
            AddComponent(entity, new UnitMover //Adds component to entity, component to add is custom MoveSpeed
            {
                moveSpeed = authoring.moveSpeed
            });
        }
    }
}

public struct UnitMover : IComponentData
{
    public float moveSpeed;
}
