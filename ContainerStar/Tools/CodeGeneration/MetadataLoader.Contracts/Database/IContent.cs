namespace MetadataLoader.Contracts.Database
{
    public interface IContent
    {
        /// <summary>
        ///     CodeName of object in code
        /// </summary>
        string CodeName { get; set; }
        /// <summary>
        ///     <see cref="CodeName" /> start with lower char
        /// </summary>
        string CodeNameCamelCase { get; }
        /// <summary>
        ///     Plural name of object
        /// </summary>
        string CodeNamePlural { get; set; }
        /// <summary>
        ///     <see cref="CodeNamePlural" /> start with lower char
        /// </summary>
        string CodeNamePluralCamelCase { get; }
    }
}