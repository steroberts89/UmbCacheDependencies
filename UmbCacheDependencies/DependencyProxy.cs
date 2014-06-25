using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UmbCacheDependencies.Dependencies;
using umbraco.BusinessLogic;
using Umbraco.Core.Logging;

namespace UmbCacheDependencies
{
    static class DependencyProxy
    {
        static readonly List<BaseUmbracoDependency> CurrentDependencies = new List<BaseUmbracoDependency>();
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
        internal static void CallDependencies(BaseUmbracoDependency.DependencyTypesEnum contentType, int[] ids)
        {
            lock (CurrentDependencies)
            {
                LogHelper.Info(typeof (DependencyProxy),
                    "Event Detected:" + contentType + " for ids:" + string.Join(",", ids));
                foreach (BaseUmbracoDependency baseUmbracoDependency in CurrentDependencies)
                {
                    //Collection was modified; enumeration operation may not execute. thrown as something calls the edit list in the HandleEvent
                    baseUmbracoDependency.HandleEvent(contentType, ids.ToArray());
                }
                lock (CurrentRemovalQueue)
                {
                    DependencyProxy.RemoveUmbracoDependencies(CurrentRemovalQueue.ToArray());
                    CurrentRemovalQueue.RemoveAll(x => true);
                }
            }
        }
        
        public static void QueueItemsToBeRemoved(BaseUmbracoDependency[] myDependencies)
        {
            lock (CurrentRemovalQueue)
            {
                CurrentRemovalQueue.AddRange(myDependencies);
            }
        }
    }
}