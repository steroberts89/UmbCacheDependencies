using Umbraco.Core.Logging;

namespace UmbCacheDependencies.Dependencies
{
    public abstract class BaseUmbracoDependency
    {
        private UmbracoDependency _dependency;
        
        internal void AddTieToDependency(UmbracoDependency dependency)
        {
            LogHelper.Info<BaseUmbracoDependency>("Adding AddTieToDependency");
            _dependency = dependency;
        }
        
        protected void ChangeDetected()
        {
            LogHelper.Info<BaseUmbracoDependency>("ChangeDetected");
            _dependency.ChangeDetected();
        }
        
        internal abstract void HandleEvent(DependencyTypesEnum typeEnum, int[] ids);
        
        internal enum DependencyTypesEnum
        {
            Content,
            Media
        }

    }
}