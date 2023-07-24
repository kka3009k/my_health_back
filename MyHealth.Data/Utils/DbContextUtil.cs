using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MyHealth.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MyHealth.Data.Utils
{
    public static class DbContextUtil
    {
        public static void ApplyConfigEntities(this ModelBuilder pBuilder)
        {
            pBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public static List<Type> GetEntities()
        {
            return Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(type => !type.IsAbstract && IsEntity(type) && HasConfigure(type))
                .ToList();
        }

        private static bool IsEntity(Type pType) => pType.BaseType != null && pType?.BaseType is EntityBase;

        private static bool HasConfigure(Type pType)
        {
            return pType.GetMethod("Configure") != null;
        }
    }
}
