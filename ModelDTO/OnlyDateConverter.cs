using Newtonsoft.Json.Converters;

namespace api.fernflowers.com.ModelDTO
{
    public class OnlyDateConverter : IsoDateTimeConverter
    {
        public OnlyDateConverter()
        {
            DateTimeFormat = "dd-MM-yyyy";
        }
    }
}