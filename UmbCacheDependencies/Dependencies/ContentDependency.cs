using Umbraco.Core.Logging;

namespace UmbCacheDependencies.Dependencies
{
    public class ContentDependency : BaseUmbracoDependency
    {
        private int? _associatedId;
        public ContentDependency(int id)
        {
              _associatedId = id;
        }

        public ContentDependency(string docTypeName)
        {
            
        }

        internal override void HandleEvent(DependencyTypesEnum typeEnum, int[] ids)
        {
            if (typeEnum == DependencyTypesEnum.Content)
            {
                if (_associatedId.HasValue)
                {
                    foreach (int id in ids)
                    {
                        if (_associatedId == id)
                        {
                            base.ChangeDetected();
                            break;
                        }
                    }
                }

            }
        }
    
    }
}