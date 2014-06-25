using System;
using System.Collections.Generic;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;

namespace UmbCacheDependencies.Dependencies
{
    /// <summary>
    /// Represents a singular dependency, such as ContentDependency etc.
    /// </summary>
    public abstract class BaseUmbracoDependency
    {
        private UmbracoDependency _dependency;
        
        /// <summary>
        /// called when the UmbracoDependency is initiated
        /// </summary>
        /// <param name="dependency"></param>
        internal void AddTieToDependency(UmbracoDependency dependency)
        {
            if (_dependency!=null)
            {
                throw new Exception("Cannot tie a specific dependency handler to a UmbracoDependency");
            }
            LogHelper.Info<BaseUmbracoDependency>("Adding AddTieToDependency");
            _dependency = dependency;
        }
        

        protected void ChangeDetected()
        {
            LogHelper.Info<BaseUmbracoDependency>("ChangeDetected");
            _dependency.ChangeDetected();
        }

        internal abstract void HandleEvent(DependencyTypesEnum typeEnum, List<IContentBase> contentItems);
        
        internal enum DependencyTypesEnum
        {
            Content,
            Media
        }

    }
}