using System;
using System.Globalization;
using UnityEngine;

namespace HypeFire.Library.Structures
{
    [Serializable]
    public class ProgressAttributeVariable
    {
        [field: SerializeField] protected ProgressionVariable progress { get; set; } = new ProgressionVariable();

        [field: SerializeField]
        protected NaturalAttributeVariable variable { get; set; } = new NaturalAttributeVariable();


        public float current
        {
            get => variable.current;
            set
            {
                variable.current = value;
                progress.fillAmount = variable.fill;
            }
        }

        public float max
        {
            get => variable.max;
            set => variable.max = value;
        }


        public static implicit operator float(ProgressAttributeVariable pField)
        {
            return pField.current;
        }

        public static implicit operator int(ProgressAttributeVariable pField)
        {
            return (int)pField.current;
        }

        public static implicit operator ProgressAttributeVariable(float Val)
        {
            var hf = new ProgressAttributeVariable();
            hf.current = Val;
            return hf;
        }

        public static implicit operator ProgressAttributeVariable(int Val)
        {
            var hf = new ProgressAttributeVariable();
            hf.current = (float)Val;
            return hf;
        }

        public static ProgressAttributeVariable operator +(ProgressAttributeVariable A, ProgressAttributeVariable B)
        {
            var hf = new ProgressAttributeVariable();
            hf.current = (A.current + B.current);
            return hf;
        }

        public static ProgressAttributeVariable operator +(ProgressAttributeVariable A, AttributeVariable B)
        {
            var hf = new ProgressAttributeVariable();
            hf.current = (A.current + B.current);
            return hf;
        }

        public static ProgressAttributeVariable operator +(ProgressAttributeVariable A, NaturalAttributeVariable B)
        {
            var hf = new ProgressAttributeVariable();
            hf.current = (A.current + B.current);
            return hf;
        }

        public static ProgressAttributeVariable operator +(ProgressAttributeVariable A, float B)
        {
            return (A.current + B);
        }

        public static ProgressAttributeVariable operator +(ProgressAttributeVariable A, int B)
        {
            return (A.current + (float)B);
        }

        public static float operator +(float A, ProgressAttributeVariable B)
        {
            return (A + B.current);
        }

        public static int operator +(int A, ProgressAttributeVariable B)
        {
            return (A + (int)B.current);
        }

        public override string ToString()
        {
            return current.ToString(CultureInfo.CurrentCulture);
        }
    }
}