UmbCacheDependencies
====================
THIS IS CURRENTLY BETA!!!
====================
.Net Cache Dependency Handlers for content and media.

This project provides the ability to expire items within the .Net Cache when a content item is changed within umbraco.

Usage is as follows:

            HttpContext.Current.Cache.Add(
                "key",
                "value", 
                new UmbracoDependency(
                    new ContentDependency(1234), // tickles cache only when document 1234 is updated.
                    new ContentDependency(1234,true), //tickles cache when document 1234 and its children are updated.
                    new AllContentOfTypeDependency(1235), //tickles cache when any document of document type 1235 is updated.
                    new AllContentOfTypeDependency("docTypeAlias"), //tickles cache when any document of document type docTypeAlias is updated.
                    new MediaDependency(124), //tickles cache when any media item of media type 124 is updated.
                    new AllMediaOfTypeDependency(1032)//tickles cache when any media of media type 1032 is updated.
                    ), 
                Cache.NoAbsoluteExpiration,
                Cache.NoSlidingExpiration, 
                CacheItemPriority.Normal, 
                null
            );
            
          
You can have as many MediaDependencies as you wish.

Currrent dependencies are as follows:


* ContentDependency - Requires a node Id which to tie to, if that content item get updated, then the cache is flushed. Has an optional parameter of catchChildren, this will make the cache expire if any children of the specified node are edited.
* MediaDependency - Accepts a media Id which to tie to, if that content item get updated, then the cache is flushed for that item.
* AllMediaOfTypeDependency - Requires a node ID of a media type. If any media items of that type are changed, then the cache is flushed.
* AllContentOfTypeDependency - Requires a node ID of a document type or an alias. If any documents of that type are changed, then the cache is flushed.
