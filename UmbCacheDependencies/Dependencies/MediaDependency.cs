﻿using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;

namespace UmbCacheDependencies.Dependencies
{
    public class MediaDependency : BaseUmbracoDependency
    {
        private int? _associatedId;

        /// <summary>
        /// The Cache will expire when the specified node is updated.
        /// </summary>
        /// <param name="id">id of the umbraco node to attach to.</param>
        public MediaDependency(int id)
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
                        if (_associatedId == item.Id)
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