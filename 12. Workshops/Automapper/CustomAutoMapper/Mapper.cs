namespace CustomAutoMapper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class Mapper
    {
        public T Map<T>(object source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source), "can't be null");
            }

            var dest = Activator.CreateInstance<T>();

            return this.DoMapping(source, dest);
        }

        private T DoMapping<T>(object source, T dest)
        {
            var destProps = dest
                .GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanWrite);

            var srcProps = source
                .GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var destProp in destProps)
            {
                var srcProp = srcProps
                    .SingleOrDefault(p => p.Name == destProp.Name);

                if (srcProp == null)
                {
                    continue;
                }

                try
                {
                    TrySetValue(source, dest, srcProp, destProp);
                }
                catch (Exception e)
                {
                    throw;
                }
            }

            return dest;
        }

        private void TrySetValue<T>(object source, T dest, PropertyInfo srcProp, PropertyInfo destProp)
        {
            var destType = destProp.PropertyType;
            var sourceValue = srcProp.GetMethod.Invoke(source, new object[0]);

            if (ReflectionUtils.IsPrimitive(destType))
            {
                destProp.SetMethod.Invoke(dest, new[]{sourceValue});
            }
            else if(ReflectionUtils.IsGenericCollection(destType))
            {
                var elementsType = sourceValue.GetType().GetGenericArguments()[0];

                if (ReflectionUtils.IsPrimitive(elementsType))
                {
                    var destinationCollection = sourceValue;
                    destProp.SetMethod.Invoke(dest, new[] { destinationCollection });
                }
                else
                {
                    var destColl = destProp.GetMethod.Invoke(dest, new object[0]);
                    var destCollType = destColl.GetType().GetGenericArguments()[0];

                    foreach (var destP in (IEnumerable)sourceValue)
                    {
                        ((IList)destColl).Add(this.DoMapping(destP, destCollType));
                    }
                }
            }
            else if (ReflectionUtils.IsNonGenericCollection(destType))
            {
                var destColl = (IList)Activator.CreateInstance(destProp.PropertyType,
                    new object[] { ((object[])sourceValue).Length });

                for (int i = 0; i < ((object[])sourceValue).Length; i++)
                {
                    destColl[i] = this.DoMapping(((object[])sourceValue)[i],
                        destProp.PropertyType.GetElementType());
                }

                destProp.SetValue(dest, destColl);
            }
            else
            {
                var propertyInstance = Activator.CreateInstance(srcProp.GetValue(source).GetType());
                destProp.SetValue(dest, this.DoMapping(srcProp.GetValue(source), propertyInstance));
            }
        }
    }
}
