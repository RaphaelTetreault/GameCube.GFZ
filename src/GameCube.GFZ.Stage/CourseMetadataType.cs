namespace GameCube.GFZ.Stage;

/// <summary>
///     Flag used to denote the purpose of the associated transform values.
/// </summary>
[System.Flags]
public enum CourseMetadataType : int
{
    /// <summary>
    ///     Use position and scale to denote position start and end of lightning.
    ///     TODO: assert assumption.
    /// </summary>
    Lightning_Lightning = 1 << 0,
    
    /// <summary>
    ///     Trigger volumes exclusive to BBO's forked jump. Unknown purpose.
    /// </summary>
    BigBlueOrdeal_Fork = 1 << 2,

    /// <summary>
    ///     Use position and scale to denote position start and end of meteors.
    ///     TODO: assert assumption.
    /// </summary>
    OuterSpace_Meteor = 1 << 4,
    
    /// <summary>
    ///     Trigger volumes for capsule in Story 1. Used only in F-Zero AX.
    ///     GX capsule data was moved to <see cref="StoryObjectTrigger"/>. 
    /// </summary>
    Story1_CapsuleAX = 1 << 30,
    
    /// <summary>
    ///     Trigger volumes for capsules in Story 5.
    /// </summary>
    Story5_Capsule = 1 << 31,
}
