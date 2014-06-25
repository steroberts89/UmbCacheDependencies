using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Caching;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Publishing;
using Umbraco.Core.Services;

namespace UmbCacheDependencies
{
    class UmbracoContentCacheDependency : CacheDependency
    {
        private int[] dependentContent;
        public UmbracoContentCacheDependency(int[] contentIds)
        {
            dependentContent = contentIds;
            ContentService.Published += DocumentPublished;
        }

        private void DocumentPublished(IPublishingStrategy sender, PublishEventArgs<IContent> e)
        {
            var ids = e.PublishedEntities.Select(x => x.Id);
            bool found = false;
            foreach (int id in ids)
            {
                if (dependentContent.Any(x => x == id))
                {
                    found = true;
                    break;
                }
            }

            if (found)
            {
                base.NotifyDependencyChanged(sender,e);
            }
            base.DependencyDispose();
              
        }

        protected override void DependencyDispose()
        {
            ContentService.Published -= DocumentPublished;
            base.DependencyDispose();
        }
    }
}