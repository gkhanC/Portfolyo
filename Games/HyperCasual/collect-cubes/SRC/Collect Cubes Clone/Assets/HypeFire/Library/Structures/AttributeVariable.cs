using System;
using System.Globalization;
using UnityEngine;

namespace HypeFire.Library.Structures
{
    [Serializable]
    public class AttributeVariable
    {
        [field: SerializeField, InspectorName("Current Value")]
        protected float _current = 0f;

        [field: SerializeField, InspectorName("Maximum Value")]
        protected float _max = 0f;

        protected float _fill = 0f;

        public float fill => _current / _max;

        public float current
        {
            get => _current;

            set
            {
                if (value > max)
                {
                    _current = max;
                    _fill = current / max;
                    return;
                }

                _current = value;
                _fill = current / max;
            }
        }

        public float max
        {
            get => _max;
            set
            {
                _max = value;

                if (current > _max)
                {
                    current = _max;
                }

                _fill = current / max;
            }
        }

        public static implicit operator float(AttributeVariable aField)
        {
            return aField.current;
        }

        public static implicit operator int(AttributeVariable aField)
        {
            return (int)aField.current;
        }

        public static implicit operator AttributeVariable(float Val)
        {
            var hf = new AttributeVariable();
            hf.current = Val;
            return hf;
        }

        public static implicit operator AttributeVariable(int Val)
        {
            var hf = new AttributeVariable();
            hf.current = (float)Val;
            return hf;
        }

        public static AttributeVariable operator +(AttributeVariable A, AttributeVariable B)
        {
            var hf = new AttributeVariable();
            hf.current = (A.current + B.current);
            return hf;
        }

        public static AttributeVariable operator +(AttributeVariable A, NaturalAttributeVariable B)
        {
            var hf = new AttributeVariable();
            hf.current = (A.current + B.current);
            return hf;
        }

        public static AttributeVariable operator +(AttributeVariable A, ProgressAttributeVariable B)
        {
            var hf = new AttributeVariable();
            hf.current = (A.current + B.current);
            return hf;
        }


        public static AttributeVariable operator +(AttributeVariable A, float B)
        {
            return (A.current + B);
        }

        public static AttributeVariable operator +(AttributeVariable A, int B)
        {
            return (A.current + (float)B);
        }

        public static float operator +(float A, AttributeVariable B)
        {
            return (A + B.current);
        }

        public static int operator +(int A, AttributeVariable B)
        {
            return (A + (int)B.current);
        }

        public override string ToString()
        {
            return current.ToString(CultureInfo.CurrentCulture);
        }
    }
}