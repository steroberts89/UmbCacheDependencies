using System;
using System.Collections.Generic;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace UmbCacheDependencies.Dependencies
{
    public class AllContentOfTypeDependency : BaseUmbracoDependency
    {
        private int? _associatedId;
        
        /// <summary>
        /// if any media items of the specified type are updated, the cache will be flushed
        /// </summary>
        /// <param name="id"></param>
        public AllContentOfTypeDependency(int id)
        {
              _associatedId = id;
        }
        /// <summary>
        /// if any media items of the specified type are updated, the cache will be flushed
        /// </summary>
        /// <param name="contentTypeAlias">alias of the content type to tie to.</param>
        public AllContentOfTypeDependency(string contentTypeAlias)
        {
            var contentType = ApplicationContext.Current.Services.ContentTypeService.GetContentType(contentTypeAlias);
            if (contentType == null)
            {
                throw new ArgumentException("Content Type: " + contentTypeAlias + " not found!");
            }
            _associatedId = contentType.Id;
        }

        internal override void HandleEvent(DependencyTypesEnum typeEnum, List<IContentBase> contentItems)
        {
            if (typeEnum == DependencyTypesEnum.Content)
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