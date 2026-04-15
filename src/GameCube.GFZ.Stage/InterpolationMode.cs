// Super Monkey Ball (SEGA AV developed) had info on keys 0, 1.
// https://craftedcart.github.io/SMBLevelWorkshop/documentation/index.html?page=stagedefFormat2#spec-stagedefFormat2-section-animationKeyframe

// Review types
// http://www.john-player.com/maya/interface/maya-animation-tangent-types-explained/

namespace GameCube.GFZ.Stage;

/// <summary>
///     Animation key interpolation methods.
/// </summary>
public enum InterpolationMode : int
{
    /// <summary>
    ///     No interpolation between keys. Values are kept until next key is hit. Maya "step" tangent.
    /// </summary>
    Constant = 0,

    /// <summary>
    ///     Linear interpolation between keys.
    /// </summary>
    Linear = 1,

    /// <summary>
    ///     Cubic interpolation between keys.
    /// </summary>
    Cubic = 2,

    /// <summary>
    ///     Cubic interpolation between keys.
    /// </summary>
    /// <remarks>
    ///     Alternate value for <see cref="Cubic"/>.<br/>
    ///     The game interprets anything that isn't <see cref="Constant"/> 
    ///     or <see cref="Linear"/> as <see cref="Cubic"/> interpolation.
    ///     Some game data has keys of value 3 instead of 2. ¯\_(ツ)_/¯
    /// </remarks>
    CubicAlt = 3,
}