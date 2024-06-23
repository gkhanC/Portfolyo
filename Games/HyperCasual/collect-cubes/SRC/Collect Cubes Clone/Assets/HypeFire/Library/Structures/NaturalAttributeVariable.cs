using System;
using System.Globalization;

namespace HypeFire.Library.Structures
{
    [Serializable]
    public class NaturalAttributeVariable : AttributeVariable
    {
        public new float current
        {
            get => _current;

            set
            {
                if (value < 0f)
                {
                    _current = 0f;
                    _fill = current / max;
                    return;
                }

                if (value > max)
                {
                    _current = max;
                    _fill = current / max;
                    return;
                }

                _current = value;
               
            }
        }

        public new float max
        {
            get => _max;
            set
            {
                if (value < 0f)
                {
                    _max = 0f;
                }
                else
                {
                    _max = value;
                }

                if (current > _max)
                {
                    current = _max;
                }

            }
        }
        
        
        public static implicit operator float(NaturalAttributeVariable naField)
        {
            return naField.current;
        }

        public static implicit operator int(NaturalAttributeVariable naField)
        {
            return (int)naField.current;
        }

        public static implicit operator NaturalAttributeVariable(float Val)
        {
            var hf = new NaturalAttributeVariable();
            hf.current = Val;
            return hf;
        }

        public static implicit operator NaturalAttributeVariable(int Val)
        {
            var hf = new NaturalAttributeVariable();
            hf.current = (float)Val;
            return hf;
        }

        public static NaturalAttributeVariable operator +(NaturalAttributeVariable A, NaturalAttributeVariable B)
        {
            var hf = new NaturalAttributeVariable();
            hf.current = (A.current + B.current);
            return hf;
        }
        
        public static NaturalAttributeVariable operator +(NaturalAttributeVariable A, AttributeVariable B)
        {
            var hf = new NaturalAttributeVariable();
            hf.current = (A.current + B.current);
            return hf;
        }
        
        public static NaturalAttributeVariable operator +(NaturalAttributeVariable A, ProgressAttributeVariable B)
        {
            var hf = new NaturalAttributeVariable();
            hf.current = (A.current + B.current);
            return hf;
        }

        public static NaturalAttributeVariable operator +(NaturalAttributeVariable A, float B)
        {
            return (A.current + B);
        }

        public static NaturalAttributeVariable operator +(NaturalAttributeVariable A, int B)
        {
            return (A.current + (float)B);
        }

        public static float operator +(float A, NaturalAttributeVariable B)
        {
            return (A + B.current);
        }

        public static int operator +(int A, NaturalAttributeVariable B)
        {
            return (A + (int)B.current);
        }

        public override string ToString()
        {
            return current.ToString(CultureInfo.CurrentCulture);
        }
        
        
    }
}