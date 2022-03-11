using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EFDataAccess.Configuration
{
    public class EntityConfigurator<TEntity> where TEntity : class
    {
        private readonly EntityTypeBuilder<TEntity> _builder;

        public EntityConfigurator(EntityTypeBuilder<TEntity> entityBuilder)
        {
            this._builder = entityBuilder;
        }

        public EntityConfigurator<TEntity> Has(Action<EntityTypeBuilder<TEntity>> builder)
        {
            builder(_builder);
            return this;
        }
    }
}