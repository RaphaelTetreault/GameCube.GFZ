namespace GameCube.GFZ.LineREL
{
    public enum RelocationType : byte
    {
        /// <summary>
        /// Do nothing. Skip this entry.
        /// </summary>
        R_PPC_NONE = 0,

        /// <summary>
        /// Write the 32-bit address of the symbol.
        /// </summary>
        R_PPC_ADDR32 = 1,

        /// <summary>
        /// Write the 24-bit address of the symbol divided by four shifted up 2 bits to the 32-bit value (for relocating branch instructions). Fail if the address won't fit.
        /// </summary>
        R_PPC_ADDR24 = 2,

        /// <summary>
        /// Write the 16-bit address of the symbol. Fail if the address is more than 16 bits.
        /// </summary>
        R_PPC_ADDR16 = 3,

        /// <summary>
        /// Write the low 16 bits of the address of the symbol.
        /// </summary>
        R_PPC_ADDR16_LO = 4,

        /// <summary>
        /// Write the high 16 bits of the address of the symbol.
        /// </summary>
        R_PPC_ADDR16_HI = 5,

        /// <summary>
        /// Write the high 16 bits of the address of the symbol plus 0x8000.
        /// </summary>
        R_PPC_ADDR16_HA = 6,

        /// <summary>
        /// Write the 14 bits of the address of the symbol divided by four shifted up 2 bits to the 32-bit value (for relocating conditional branch instructions). Fail if the address won't fit.
        /// </summary>
        R_PPC_ADDR14 = 7,

        /// <summary>
        /// Write the 24-bit address of the symbol minus the address of the relocation divided by four shifted up 2 bits to the 32-bit value (for relocating relative branch instructions). Fail if the address won't fit.
        /// </summary>
        R_PPC_REL24 = 10,

        /// <summary>
        /// Write the 14-bit address of the symbol minus the address of the relocation divided by four shifted up 2 bits to the 32-bit value (for relocating conditional relative branch instructions). Fail if the address won't fit.
        /// </summary>
        R_PPC_REL14 = 11,

        /// <summary>
        /// Represents relocation type with value 201. Do not relocate anything, but accumulate the offset field for the next relocation offset calculation. These types are used for referring to relocations that are more than 0xffff apart from each other.
        /// </summary>
        R_DOLPHIN_NOP = 201,

        /// <summary>
        /// Represents relocation type with value 202. Change which section relocations are being applied to. Set the offset into the section to 0.
        /// </summary>
        R_DOLPHIN_SECTION = 202,

        /// <summary>
        /// Represents relocation type with value 203. Stop parsing the relocation list.
        /// </summary>
        R_DOLPHIN_END = 203,

        /// <summary>
        /// Represents relocation type with value 204.
        /// </summary>
        R_DOLPHIN_MRKREF = 204
    }

}
