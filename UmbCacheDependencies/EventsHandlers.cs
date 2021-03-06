﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UmbCacheDependencies.Dependencies;
using Umbraco.Core;
using Umbraco.Core.Events;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.Publishing;
using Umbraco.Core.Services;


namespace UmbCacheDependencies
{
    /// <summary>
    /// This class ties into the umbraco evnts
    /// </summary>
   internal class EventsHandlers : IApplicationEventHandler
    {
       public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
       {
          
       }

       public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
       {
          
       }

       public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
       {
           LogHelper.Info<EventsHandlers>("Starting up... tying into events");
           ContentService.Published += DocumentPublished;
           ContentService.Moved += DocumentMoved;
           ContentService.Deleted += ContentService_Deleted;

           MediaService.Moved += MediaService_Moved;
           MediaService.Saved += MediaService_Saved;
           MediaService.Deleted += MediaService_Deleted;
       }

       void MediaService_Deleted(IMediaService sender, DeleteEventArgs<IMedia> e)
       {
           DependencyProxy.CallDependencies(BaseUmbracoDependency.DependencyTypesEnum.Media, e.DeletedEntities.Cast<IContentBase>().ToList());
       }

       void MediaService_Saved(IMediaService sender, SaveEventArgs<IMedia> e)
       {
           DependencyProxy.CallDependencies(BaseUmbracoDependency.DependencyTypesEnum.Media, e.SavedEntities.Cast<IContentBase>().ToList());
       }

       void MediaService_Moved(IMediaService sender, MoveEventArgs<IMedia> e)
       {
           DependencyProxy.CallDependencies(BaseUmbracoDependency.DependencyTypesEnum.Media, e.MoveInfoCollection.Select(x=>x.Entity).Cast<IContentBase>().ToList());
       }
       
       void ContentService_Deleted(IContentService sender, DeleteEventArgs<IContent> e)
       {
           DependencyProxy.CallDependencies(BaseUmbracoDependency.DependencyTypesEnum.Content, e.DeletedEntities.Cast<IContentBase>().ToList());
       }

       private void DocumentMoved(IContentService sender, MoveEventArgs<IContent> e)
       {
           DependencyProxy.CallDependencies(BaseUmbracoDependency.DependencyTypesEnum.Content, e.MoveInfoCollection.Select(x=>x.Entity).Cast<IContentBase>().ToList());
       }

       private void DocumentPublished(IPublishingStrategy sender, PublishEventArgs<IContent> e)
       {
           DependencyProxy.CallDependencies(BaseUmbracoDependency.DependencyTypesEnum.Content, e.PublishedEntities.Cast<IContentBase>().ToList());
       }
    }
}