using System;
using System.Linq;

namespace UmbCacheDependencies.Dependencies
{
    public class MediaDependency : BaseUmbracoDependency
    {
        private int? _associatedId;
        public MediaDependency(int id)
        {
            _associatedId = id;
        }

        public MediaDependency(string mediaTypeName)
        {
           throw new NotImplementedException();
        }

        internal override void HandleEvent(DependencyTypesEnum typeEnum, int[] ids)
        {
            if (typeEnum == DependencyTypesEnum.Media)
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