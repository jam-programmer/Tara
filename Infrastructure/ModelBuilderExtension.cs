namespace Infrastructure
{
    public static class ModelBuilderExtension
    {
        public static void AppendDbSetOfEntity(this ModelBuilder builder)
        {
            Assembly? assembly = Assembly.GetAssembly(typeof(Domain.Entities.BaseEntity));
            if (assembly is not null)
            {
                List<Type> entities = assembly.GetTypes()
                   .Where(w => w.IsClass
                   && w.IsPublic
                   && w.BaseType != null && w.BaseType == typeof(BaseEntity))
                   .ToList();
                entities.ForEach(entity => builder.Entity(entity));
            }
        }
    }
}
