using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UmbCacheDependencies.Dependencies;
using umbraco.BusinessLogic;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;

namespace UmbCacheDependencies
{
    /// <summary>
    /// Manages our BaseUmbracoDependencies and receives a calls from the events in <see cref="EventsHandlers"/> 
    /// </summary>
    static class DependencyProxy
    {
        /// <summary>
        /// List of currently active UmrbacoDependencies
        /// </summary>

        static readonly List<BaseUmbracoDependency> CurrentDependencies = new List<BaseUmbracoDependency>();

        /// <summary>
        /// This is a temporary removal queue used to store items which should be removed from the queue. Items are added from UmbracoDependency.DependencyDispose and removed from DependencyProxy.CallDependencies
        /// </summary>
        static readonly List<BaseUmbracoDependency> CurrentRemovalQueue = new List<BaseUmbracoDependency>();
        
        internal static void AddDependencies(BaseUmbracoDependency[] dependencies)
        {
            LogHelper.Info(typeof(DependencyProxy), "AddDependencies");
            lock (CurrentDependencies)
            {
                CurrentDependencies.AddRange(dependencies);
            }
        }

        internal static void RemoveUmbracoDependencies(BaseUmbracoDependency[] myDependencies)
        {
            LogHelper.Info(typeof(DependencyProxy), "RemoveUmbracoDependencies");
            lock (CurrentDependencies)
            {
                foreach (var dependency in myDependencies)
                {
                    CurrentDependencies.Remove(dependency);
                }
            }
        }

        /// <summary>
        /// Debug method to help ensure dependencies are removed.
        /// </summary>
        /// <returns></returns>
        public static int GetCurrentDependencies()
        {
            lock (CurrentDependencies)
            {
                return CurrentDependencies.Count;
            }
        }

        /// <summary>
        /// Loops through all the current umbraco dependencies and 
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="contentItems"></param>
        internal static void CallDependencies(BaseUmbracoDependency.DependencyTypesEnum contentType, List<IContentBase> contentItems)
        {
            lock (CurrentDependencies)
            {
                LogHelper.Info(typeof (DependencyProxy),
                    "Event Detected:" + contentType + " for ids:" + string.Join(",", contentItems.Select(x=>x.Id)));
                foreach (BaseUmbracoDependency baseUmbracoDependency in CurrentDependencies)
                {
                    baseUmbracoDependency.HandleEvent(contentType, contentItems);
                }
                lock (CurrentRemovalQueue)
                {
                    DependencyProxy.RemoveUmbracoDependencies(CurrentRemovalQueue.ToArray());
                    CurrentRemovalQueue.RemoveAll(x => true);
                }
            }
        }
        
        public static void AddQueueItemsToBeRemoved(BaseUmbracoDependency[] myDependencies)
        {
            lock (CurrentRemovalQueue)
            {
                CurrentRemovalQueue.AddRange(myDependencies);
            }
        }
    }
}