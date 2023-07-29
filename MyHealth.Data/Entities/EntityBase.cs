using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHealth.Data.Entities
{
    /// <summary>
    /// Базовый класс сущности
    /// </summary>
    public class EntityBase
    {
        /// <summary>
        /// Код сущности
        /// </summary>
        [Key]
        public virtual int ID { get; set; }
    }

    public class EntityBase<TEntity> : EntityBase, IEntityTypeConfiguration<TEntity> where TEntity : class
    {
        public void Configure(EntityTypeBuilder<TEntity> pBuilder)
        {
            pBuilder.HasKey(nameof(ID));
            CustomConfigure(pBuilder);
        }

        /// <summary>
        /// Рассширяет логику настройки модели в БД
        /// </summary>
        /// <param name="pBuilder"></param>
        public virtual void CustomConfigure(EntityTypeBuilder<TEntity> pBuilder) { }
    }
}
