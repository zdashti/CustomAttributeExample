using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomAttributeExample
{
    public static class AttributeHelper
    {
        public static Dictionary<string, object?> FindByAttribute<TRequest, TAttribute>(TRequest request) where TAttribute : Attribute
        {
            if (request == null)
                return new Dictionary<string, object?>();

            var props = request.GetType().GetProperties().Where(
                prop => Attribute.IsDefined(prop, typeof(TAttribute)));

            var dict = new Dictionary<string, object?>();
            foreach (var property in props)
                dict[property.Name] = property.GetValue(request);

            return dict;
        }
    }
}
