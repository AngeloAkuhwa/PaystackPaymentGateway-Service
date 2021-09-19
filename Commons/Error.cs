namespace EcommerceApi_dotNetFramework.Commons
{
    public class Error
    {
        public string PropertyName { get; set; }

        public string PropertyValue { get; set; }

        public bool HasValidationErrors { get; set; }
    }
}