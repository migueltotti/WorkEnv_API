using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WorkEnv.Infrastructure.Cache;

public class PrivateResolver : DefaultContractResolver
{
    protected override JsonProperty CreateProperty(
        MemberInfo member,
        MemberSerialization memberSerialization)
    {
        JsonProperty property = base.CreateProperty(member, memberSerialization);

        if (!property.Writable)
        {
            var prop = member as PropertyInfo;
            
            bool hasPrivateSetter = prop?.GetSetMethod(true) != null;
            
            property.Writable = hasPrivateSetter;
        }
        
        return property;
    }
}