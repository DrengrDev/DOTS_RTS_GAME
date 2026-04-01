using Unity.Entities;
using UnityEngine;

public class MoveSpeedAuthoring : MonoBehaviour
{
    public float value;

    public class Baker : Baker<MoveSpeedAuthoring>
    {
        public override void Bake(MoveSpeedAuthoring authoring) //Function runs during the bake process
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic); //Creates entity that has dots transform components
            AddComponent(entity, new MoveSpeed //Adds component to entity, component to add is custom MoveSpeed
            {
                value = authoring.value
            });
        }
    }
}

public struct MoveSpeed : IComponentData
{
    public float value;
}
