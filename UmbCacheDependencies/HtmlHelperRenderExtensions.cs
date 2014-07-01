using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Lucene.Net.Util.Cache;
using Umbraco.Core;
using umbraco.cms.businesslogic.member;
using Umbraco.Web;
using Cache = System.Web.Caching.Cache;


namespace UmbCacheDependencies
{
    public static class HtmlHelperRenderExtensions
    {
        public static IHtmlString DependencyCachedPartial(this HtmlHelper htmlHelper, string key, string partialViewName, object model, UmbracoDependency dependencies, ViewDataDictionary viewData = null)
        {
            var cache = HttpContext.Current.Cache;
            var result = cache[key] as MvcHtmlString;
            if (result == null)
            {
                result = htmlHelper.Partial(partialViewName, model, viewData);
                cache.Add(key, result, dependencies, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration,
                    CacheItemPriority.Normal, null);
            }
            return result;
        }
    }
}