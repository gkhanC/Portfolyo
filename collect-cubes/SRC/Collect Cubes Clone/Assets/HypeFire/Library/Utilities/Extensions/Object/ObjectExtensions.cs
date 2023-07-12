using UnityEngine;

namespace HypeFire.Library.Utilities.Extensions.Object
{
    public static class ObjectExtensions
    {
        public static bool IsNull(this object Value) => Value is null;

        public static bool IsNotNull(this object Value) => Value is not null;

        public static bool IsNull(this GameObject Value) =>  ReferenceEquals(Value, null) ? false : (Value ? false : true);

        public static bool IsNotNull(this GameObject Value) => (ReferenceEquals(Value, null) ? true : (Value ? true : false));
        
        public static bool IsNull(this UnityEngine.Transform Value) => ReferenceEquals(Value, null) ? false : (Value ? false : true);

        public static bool IsNotNull(this UnityEngine.Transform Value) => (ReferenceEquals(Value, null) ? true : (Value ? true : false));
        
        public static bool IsNull(this Rigidbody Value) =>  ReferenceEquals(Value, null) ? false : (Value ? false : true);

        public static bool IsNotNull(this Rigidbody Value) =>(ReferenceEquals(Value, null) ? true : (Value ? true : false));

    }
}