namespace Tasko.General.Extensions
{
    public static class BuilderConfigurationExtension
    {
        public static void SetSettingFile(this WebApplicationBuilder builder, string relativePath, string fileName)
        {
            var absolutePath = Path.GetFullPath(relativePath);
            builder.Configuration.SetBasePath(absolutePath)
                                 .AddJsonFile(fileName);
        }
    }
}
