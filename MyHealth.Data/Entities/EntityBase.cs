using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public virtual Guid ID { get; set; }

        /// <summary>
        /// Время создания
        /// </summary>
        [Column(TypeName = "timestamp with time zone")]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Время изменения
        /// </summary>
        [Column(TypeName = "timestamp with time zone")]
        public DateTime? UpdatedAt { get; set; }
    }

    public class EntityBase<TEntity> : EntityBase, IEntityTypeConfiguration<TEntity> where TEntity : class
    {
        public void Configure(EntityTypeBuilder<TEntity> pBuilder)
        {
            pBuilder.HasKey(nameof(ID));
            pBuilder.Property(nameof(CreatedAt)).HasDefaultValueSql("now()");
            pBuilder.Property(nameof(UpdatedAt)).HasDefaultValueSql("now()");
            CustomConfigure(pBuilder);
        }

        /// <summary>
        /// Рассширяет логику настройки модели в БД
        /// </summary>
        /// <param name="pBuilder"></param>
        private protected virtual void CustomConfigure(EntityTypeBuilder<TEntity> pBuilder) { }
    }
}
