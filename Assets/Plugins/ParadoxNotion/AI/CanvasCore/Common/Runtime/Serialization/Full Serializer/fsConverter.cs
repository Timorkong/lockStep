using System;

namespace AIParadoxNotion.Serialization.FullSerializer
{

    /// The serialization converter allows for customization of the serialization process.
    public abstract class fsConverter : fsBaseConverter
    {

        /// Can this converter serialize and deserialize the given object type?
        public abstract bool CanProcess(Type type);
    }
}