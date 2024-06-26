using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;


public struct UnitMovement : IComponentData {
    public float3 Velocity; 
    public float MoveSpeed;

}
public class UnitMovementAuthoring : MonoBehaviour {
    public float3 Velocity;
    public float MoveSpeed;

    public class Baker : Baker<UnitMovementAuthoring> {
        public override void Bake(UnitMovementAuthoring authoring) {
            Entity entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
            AddComponent(entity,
                new UnitMovement()
                {
                    Velocity = authoring.Velocity,
                    MoveSpeed = authoring.MoveSpeed
                });
        }
    }
}

public partial struct UnitMovementSystem : ISystem {
    public void OnUpdate(ref SystemState state) {
        foreach (var (physicsVelocity, unitMovement) in SystemAPI.Query<RefRW<PhysicsVelocity>, RefRW<UnitMovement>>()) {
             physicsVelocity.ValueRW.Linear = unitMovement.ValueRO.Velocity * unitMovement.ValueRO.MoveSpeed;
        }
    }
}