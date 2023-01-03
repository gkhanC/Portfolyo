using UnityEngine;

namespace HypeFire.Library.Utilities.Extensions.Object
{
    public static class ObjectExtensions
    {
        public static bool IsNull(this object Value) => Value == null;

        public static bool IsNotNull(this object Value) => Value != null;

        public static bool IsNull(this GameObject Value) => Value == null;

        public static bool IsNotNull(this GameObject Value) => Value != null;
    }
}