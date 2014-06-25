using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Caching;
using UmbCacheDependencies;
using Umbraco.Core;
using Umbraco.Core.Events;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.Publishing;
using Umbraco.Core.Services;
using UmbCacheDependencies.Dependencies;

namespace UmbCacheDependencies
{
    public class UmbracoDependency : CacheDependency
    {
        private BaseUmbracoDependency[] _myDependencies;
        public UmbracoDependency(params BaseUmbracoDependency[] dependencies)
        {
            LogHelper.Info<UmbracoDependency>("Initialized");
            _myDependencies = dependencies;

            foreach (var umbracoDependency in _myDependencies)
            {
                LogHelper.Info<UmbracoDependency>("adding tie");
                umbracoDependency.AddTieToDependency(this);
            }
            DependencyProxy.AddDependencies(dependencies);
        }

        internal void ChangeDetected()
        {
            LogHelper.Info<UmbracoDependency>("Telling base of a change detected");
            base.NotifyDependencyChanged(null, null);
        }
        protected override void DependencyDispose()
        {
            LogHelper.Info<UmbracoDependency>("Cleaning up as dependency disposed called");
            
            DependencyProxy.QueueItemsToBeRemoved(_myDependencies);
            
            base.DependencyDispose();
        }
    }
}