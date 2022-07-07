using Manifold;
using Manifold.IO;
using System;
using System.IO;

namespace GameCube.GFZ.GMA
{
    /// <summary>
    /// A model comprised of a mesh (many display lists) and materials.
    /// </summary>
    public class Model
    {
        // FIELDS
        private ShiftJisCString name;
        private Gcmf gcmf;

        // PROPERTIES
        public Pointer GcmfPtr { get; set; }
        public Pointer NamePtr { get; set; }
        public ShiftJisCString Name { get => name; set => name = value; }
        public Gcmf Gcmf { get => gcmf; set => gcmf = value; }

        // METADATA
        /// <summary>
        /// Indicates the deserialized index from it's original GMA. Useful for
        /// debug printing model index.
        /// </summary>
        public string DebugIndex { get; set; }


        public Model() { }
        public Model(string name, Gcmf gcmf)
        {
            this.name = name;
            this.gcmf = gcmf;
        }



        // METHODS
        public void Deserialize(EndianBinaryReader reader)
        {
            // Pointers are assigned from outside the class.
            // If not set, Deserialize() is not expected to be called.
            Assert.IsTrue(GcmfPtr.IsNotNull);
            Assert.IsTrue(NamePtr.IsNotNull);

            reader.JumpToAddress(NamePtr);
            reader.Read(ref name);

            reader.JumpToAddress(GcmfPtr);
            reader.Read(ref gcmf);
        }

    }
}
