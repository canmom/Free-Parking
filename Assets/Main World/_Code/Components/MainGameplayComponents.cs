using Latios;
using Latios.Psyshock;
using Latios.Transforms;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace FreeParking.MainWorld.MainGameplay
{
    public struct PlayerMotionDesiredActions : IComponentData
    {
        public float2 cameraRelativeMovement;
    }

    public struct PlayerMotionStats : IComponentData
    {
        public CapsuleCollider collider;
        public float           targetHoverHeight;
        public float           skinWidth;
        public float           cosMaxSlope;

        public float maxSpeed;
        public float maxTurnSpeed;
        public float gravity;
        public float acceleration;
        public float hoverKpM;  // k/mass

        public EntityWith<TwoAgoTransform>        cameraEntity;
        public EntityWith<PlayerInteractionState> interactionEntity;
    }

    public struct PlayerMotionState : IComponentData
    {
        public float speed;
        public float verticalSpeed;
        public bool  isSafeToInteract;  // Safe for interacting and such.
    }

    public struct PlayerInteractionDesiredActions : IComponentData
    {
        public bool interact;
        public bool cancel;
    }

    public struct PlayerInteractionStats : IComponentData
    {
        public float maxDistance;
        public float cosFov;
    }

    public struct PlayerInteractionState : IComponentData
    {
        public bool interacting;
    }

    public struct NpcCollisionTag : IComponentData { }

    public struct InteractableTargetTag : IComponentData { }

    public struct QuestBlob
    {
        public struct Subquest
        {
            public GameFlagHandle            completionFlag;
            public BlobArray<byte>           prelistText;
            public BlobArray<GameFlagHandle> checklistFlags;
            public BlobArray<byte>           postlistText;
        }

        public FixedString128Bytes questName;
        public BlobArray<Subquest> subquests;
    }

    // Lives on the worldBlackboardEntity.
    public struct ActiveQuest : IBufferElementData
    {
        public BlobAssetReference<QuestBlob> quest;
    }

    // Lives on the worldBlackboardEntity.
    public struct FinishedQuest : IBufferElementData
    {
        public BlobAssetReference<QuestBlob> quest;
    }
}

