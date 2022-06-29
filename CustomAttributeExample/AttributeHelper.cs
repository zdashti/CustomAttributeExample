using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomAttributeExample
{
    public static class AttributeHelper
    {
        public static Dictionary<string, object?> FindByAttribute<TRequest, TAttribute>(TRequest request) where TAttribute : Attribute
        {
            var dict = new Dictionary<string, object?>();

            if (request != null)
            {
                dict["Type"] = request.GetType();
                var props = request.GetType().GetProperties().Where(
                    prop => Attribute.IsDefined(prop, typeof(TAttribute)));

                foreach (var property in props)
                    dict[property.Name] = property.GetValue(request);
            }

            return dict;
        }
    }
}
