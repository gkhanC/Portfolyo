namespace HypeFire.Utilities.CustomStructures
{
    using System;

    [Serializable]
    public class GenericRandomAbleValues<T> where T : struct
    {
        protected ValueType m_value_type = ValueType.UseConstant;

        public void SetValueType(ValueType vType)
        {
            m_value_type = vType;
        }

        public ValueType valueType
        {
            get => m_value_type;
            set => m_value_type = value;
        }

        public enum ValueType
        {
            UseConstant = 0,
            UseRandomize = 1
        }
    }
}