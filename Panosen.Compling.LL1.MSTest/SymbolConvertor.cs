using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling.LL1.MSTest
{
    public class SymbolConvertor : JsonConverter<Symbol>
    {
        public override Symbol ReadJson(JsonReader reader, Type objectType, Symbol existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, Symbol value, JsonSerializer serializer)
        {
            writer.WriteValue(value.Value);
        }
    }
}
