using GameCube.GFZ.GCI;

namespace GameCube.GFZ.Emblem
{
    /// <summary>
    /// 
    /// </summary>
    /// <example>
    ///     Primary source of information:
    ///     https://docs.google.com/document/d/1c4a7d6xZ-rnK-E5p7d__6V1qhLxcdOHwAmv-FR8K50s/edit
    /// </example>
    public class EmblemGCI : GfzGci<Emblem>
    {
        public Emblem Emblem { get => FileData; set => FileData = value; }
    }
}
