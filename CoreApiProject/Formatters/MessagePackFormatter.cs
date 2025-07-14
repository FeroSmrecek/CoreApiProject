namespace CoreApiProject.Formatters
{
    public class MessagePackFormatter : Microsoft.AspNetCore.Mvc.Formatters.OutputFormatter
    {
        public MessagePackFormatter()
        {
            SupportedMediaTypes.Add("application/x-msgpack");
        }

        protected override bool CanWriteType(Type type)
        {
            return Attribute.IsDefined(type, typeof(MessagePack.MessagePackObjectAttribute)) ||
                   (type.IsGenericType && typeof(IEnumerable<>).IsAssignableFrom(type.GetGenericTypeDefinition())
                   && Attribute.IsDefined(type.GetGenericArguments()[0], typeof(MessagePack.MessagePackObjectAttribute)));
        }

        public override async Task WriteResponseBodyAsync(Microsoft.AspNetCore.Mvc.Formatters.OutputFormatterWriteContext context)
        {
            var response = context.HttpContext.Response;

            if (context.Object != null)
            {
                await MessagePack.MessagePackSerializer.SerializeAsync(response.Body, context.Object, cancellationToken: response.HttpContext.RequestAborted);
            }
        }
    }
}
