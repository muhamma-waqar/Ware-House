namespace WebAPI.Swagger.Filters
{
    /// <summary>
    /// Specifies a custom name that overrides Swagger's default group name for the actions in the given controller.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SwaggerGroupAttribute
    {
        public string GroupName { get; set; }

        public SwaggerGroupAttribute(string groupName)
        {
            GroupName = groupName;
        }
    }
}
