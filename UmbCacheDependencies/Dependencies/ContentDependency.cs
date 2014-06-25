using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;

namespace UmbCacheDependencies.Dependencies
{
    public class ContentDependency : BaseUmbracoDependency
    {
        private bool _catchChildren;
        private int? _associatedId;
        
        /// <summary>
        /// The Cache will expire when the specified node is updated.
        /// </summary>
        /// <param name="id">Id to attach to</param>
        /// <param name="catchChildren">Will make this dependency also flush when any children are edited.</param>
        public ContentDependency(int id, bool catchChildren=false)
        {
            _catchChildren = catchChildren;
            _associatedId = id;
        }

        internal override void HandleEvent(DependencyTypesEnum typeEnum, List<IContentBase> contentItems)
        {
            if (typeEnum == DependencyTypesEnum.Content)
            {
                if (_associatedId.HasValue)
                {
                    foreach (var item in contentItems)
                    {
                        if (_catchChildren)
                        {
                            if (Helpers.ConvertCsvToIntArray(item.Path).Any(x=>x == _associatedId))
                            {
                                base.ChangeDetected();
                                break;
                            }
                        }
                        else
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
}