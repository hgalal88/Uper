﻿using System.Collections.Generic;
using System.Linq;
using WebApp.Data.Specifications;
namespace WebApp.Data.Repositories
{
    /// <summary>
    /// Base class for every repository. Implements shared functionality such as adding, removing, finding by id etc.
    /// </summary>
    /// <typeparam name="EntityType">Type of entity</typeparam>
    public abstract class BaseRepository<EntityType,IdType> : IRepository<EntityType,IdType> where EntityType : class
    {
        protected ApplicationContext context;
        
        public BaseRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public void Add(EntityType toAdd)
        {
            context.Set<EntityType>().Add(toAdd);
            context.SaveChanges();
        }

        public IEnumerable<EntityType> GetAll()
        {
            return context.Set<EntityType>().ToList();
        }

        public EntityType GetById(IdType id)
        {
            return context.Set<EntityType>().Find(id);
        }

        public IEnumerable<EntityType> GetList(ITravelListSpecification<EntityType> specification)
        {
            return SpecificationEvaluator<EntityType>.EvaluateSpecification(
                context.Set<EntityType>(),
                specification);
        }

        public void Remove(EntityType toRemove)
        {
            context.Set<EntityType>().Remove(toRemove);
            context.SaveChanges();
        }
    }
}
