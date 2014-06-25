﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using ICSharpCode.SharpZipLib.Zip;
using UmbCacheDependencies.Dependencies;


namespace UmbCacheDependencies.Old
{
    class Playground
    {
        private static void Test()
        {
            HttpContext.Current.Cache.Add("", "", new UmbracoContentCacheDependency(new int[] {1234}),
                Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);


            HttpContext.Current.Cache.Add("", "", 
                new UmbracoDependency(new ContentDependency(1234), new ContentDependency("docTypeAlias"), new MediaDependency(124), new MediaDependency("image"))

,
                Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
        }
    }
}