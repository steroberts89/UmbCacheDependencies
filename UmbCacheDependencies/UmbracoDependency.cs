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
    /// <summary>
    /// This is a .Net Cache dependency, It can be linked to multiple umbraco dependencies
    /// </summary>
    public class UmbracoDependency : CacheDependency
    {
        private BaseUmbracoDependency[] _myDependencies;

        /// <summary>
        /// This is a .Net Cache dependency, It can be linked to multiple Umbraco Dependencies
        /// </summary>
        /// <param name="dependencies">List of Umbraco Dependencies for the Cache to be flushed on</param>
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

        /// <summary>
        /// Called from the BaseUmbracoDependency when a change is detected
        /// </summary>
        internal void ChangeDetected()
        {
            LogHelper.Info<UmbracoDependency>("Telling base of a change detected");
            base.NotifyDependencyChanged(null, null);
        }

        /// <summary>
        /// Class specific to .net cache providers, Cleans up our BaseUmbracoDependencys
        /// </summary>
        protected override void DependencyDispose()
        {
            LogHelper.Info<UmbracoDependency>("Cleaning up as dependency disposed called");

            DependencyProxy.AddQueueItemsToBeRemoved(_myDependencies);
            
            base.DependencyDispose();
        }
    }
}