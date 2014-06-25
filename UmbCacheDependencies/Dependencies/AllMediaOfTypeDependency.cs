using System;
using System.Collections.Generic;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace UmbCacheDependencies.Dependencies
{
    public class AllMediaOfTypeDependency : BaseUmbracoDependency
    {
        private int? _associatedId;
        /// <summary>
        /// if any media items of the specified type are updated, the cache will be flushed
        /// </summary>
        /// <param name="id"></param>
        public AllMediaOfTypeDependency(int id)
        {
              _associatedId = id;
        }

        internal override void HandleEvent(DependencyTypesEnum typeEnum, List<IContentBase> contentItems)
        {
            if (typeEnum == DependencyTypesEnum.Media)
            {
                if (_associatedId.HasValue)
                {
                    foreach (var item in contentItems)
                    {
                        if (_associatedId == item.ContentTypeId)
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