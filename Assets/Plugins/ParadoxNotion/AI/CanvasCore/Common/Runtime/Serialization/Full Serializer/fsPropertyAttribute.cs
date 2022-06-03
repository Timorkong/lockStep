using System;

namespace AIParadoxNotion.Serialization.FullSerializer
{

    /// Explicitly mark a property to be serialized with optional name
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class fsSerializeAsAttribute : Attribute
    {
        readonly public string Name;

        public fsSerializeAsAttribute(string name) {
            this.Name = name;
        }
    }
}