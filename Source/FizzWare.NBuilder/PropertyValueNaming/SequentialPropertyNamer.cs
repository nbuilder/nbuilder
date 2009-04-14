using System;
using System.Collections.Generic;
using FizzWare.NBuilder;
using System.Reflection;
using System.Diagnostics;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;

namespace FizzWare.NBuilder.PropertyNaming
{
    public class SequentialPropertyNamer<T> : IPropertyNamer<T>
    {
        private readonly IReflectionUtil reflectionUtil;

        public SequentialPropertyNamer(IReflectionUtil reflectionUtil)
        {
            this.reflectionUtil = reflectionUtil;
        }

        public void SetValuesOfAllIn(IList<T> objects)
        {
            const BindingFlags FLAGS = (BindingFlags.Public | BindingFlags.Instance);

            var type = typeof(T);

            for (int i = 0; i < objects.Count; i++)
            {
                foreach (var propertyInfo in type.GetProperties(FLAGS))
                {
                    SetPropertyValue(propertyInfo, objects[i], i + 1);
                }
            }
        }

        private void SetPropertyValue(PropertyInfo propertyInfo, T obj, int sequenceNumber)
        {
            SetPropertyValue(propertyInfo, obj, sequenceNumber, sequenceNumber.ToString());
        }

        private void SetPropertyValue(PropertyInfo propertyInfo, T obj, int sequenceNumber, string sequenceIdentifier)
        {
            Type propertyType = propertyInfo.PropertyType;

            if (propertyInfo.GetSetMethod() == null)
                return;

            // Check to see if the property has already been assigned a value

            object currentValue = propertyInfo.GetValue(obj, null);
            
            if (!reflectionUtil.IsDefaultValue(currentValue))
                return;

            object value = null;

            if (propertyType == typeof(short))
            {
                int newSequenceNumber = GetNewSequenceNumber(sequenceNumber, short.MaxValue);
                value = Convert.ToInt16(newSequenceNumber);
                goto set_property;
            }

            if (propertyType == typeof(int))
            {
                value = sequenceNumber;
                goto set_property;
            }

            if (propertyType == typeof(long))
            {
                value = Convert.ToInt64(sequenceNumber);
                goto set_property;
            }

            if (propertyType == typeof(decimal))
            {
                value = Convert.ToDecimal(sequenceNumber);
                goto set_property;
            }

            if (propertyType == typeof(float))
            {
                value = Convert.ToSingle(sequenceNumber);
                goto set_property;
            }

            if (propertyType == typeof(double))
            {
                value = Convert.ToDouble(sequenceNumber);
                goto set_property;
            }

            if (propertyType == typeof(ushort))
            {
                value = Convert.ToUInt16(sequenceNumber);
                goto set_property;
            }

            if (propertyType == typeof(uint))
            {
                value = Convert.ToUInt32(sequenceNumber);
                goto set_property;
            }

            if (propertyType == typeof(ulong))
            {
                value = Convert.ToUInt64(sequenceNumber);
                goto set_property;
            }

            if (propertyType == typeof(char))
            {
                int newSequenceNumber = GetNewSequenceNumber(sequenceNumber, 26);
                newSequenceNumber += 64;

                value = Convert.ToChar(newSequenceNumber);
                goto set_property;
            }

            if (propertyType == typeof(byte))
            {
                int newSequenceNumber = GetNewSequenceNumber(sequenceNumber, byte.MaxValue);

                value = Convert.ToByte(newSequenceNumber);
                goto set_property;
            }

            if (propertyType == typeof(DateTime))
            {
                value = DateTime.Now.AddDays(sequenceNumber);
                goto set_property;
            }

            if (propertyType == typeof(string))
            {
                value = propertyInfo.Name + sequenceIdentifier;
                goto set_property;
            }

            set_property:
                if (value != null && propertyInfo.CanWrite)
                    propertyInfo.SetValue(obj, value, null);
        }

        /// <summary>
        /// Gets the new sequence number taking into account a maximum value.
        /// 
        /// If the current sequence number is above the maximum value it will 
        /// reset it to zero, and continue the sequence from there until the maximum 
        /// value is reached again.
        /// </summary>
        /// <param name="sequenceNumber">The sequence number.</param>
        /// <param name="maxValue">The max value.</param>
        /// <returns></returns>
        private static int GetNewSequenceNumber(int sequenceNumber, int maxValue)
        {
            int newSequenceNumber;
            if (sequenceNumber > maxValue)
            {
                int divisor = sequenceNumber / maxValue;
                newSequenceNumber = sequenceNumber - (divisor * maxValue);
            }
            else
                newSequenceNumber = sequenceNumber;

            return newSequenceNumber;
        }

        public void SetValuesOf(T obj)
        {
            var type = typeof(T);

            foreach (var propertyInfo in type.GetProperties())
                SetPropertyValue(propertyInfo, obj, 1);
        }

        public void SetValuesOf(T obj, int sequenceNumber, string sequenceIdentifier)
        {
            var type = typeof(T);

            foreach (var propertyInfo in type.GetProperties())
                SetPropertyValue(propertyInfo, obj, sequenceNumber, sequenceIdentifier);
        }
    }
}