using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling.LL1.MSTest
{
    public class TINYNodeConvertor : JsonConverter<GrammarNode>
    {
        public override GrammarNode ReadJson(JsonReader reader, Type objectType, GrammarNode existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, GrammarNode value, JsonSerializer serializer)
        {
            if (!value.IsNotEmpty())
            {
                return;
            }

            writer.WriteStartObject();

            if (!value.Data.Equals(Symbols.Epsilon))
            {
                writer.WritePropertyName(nameof(value.Data));
                writer.WriteValue(value.Data.Value);
            }

            if (value.Children != null && value.Children.Count > 0 && value.Children.Any(v => v.IsNotEmpty()))
            {
                writer.WritePropertyName(nameof(value.Children));
                serializer.Serialize(writer, value.Children);
            }

            writer.WriteEndObject();
        }
    }
}
