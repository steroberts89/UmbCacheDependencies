UmbCacheDependencies
====================

.Net Cache Dependency Handlers for content and media etc.

This project provides the ability to expire items within the .Net Cache when a content/media type is changed within umbraco.

Usage is as follows:

            HttpContext.Current.Cache.Add(
                "key",
                "value", 
                new UmbracoDependency(
                    new ContentDependency(1234),
                    new MediaDependency(124)
                    ), 
                Cache.NoAbsoluteExpiration,
                Cache.NoSlidingExpiration, 
                CacheItemPriority.Normal, 
                null
            );
            
          
You can have as many ContentDependency/MediaDependencies as you wish.

THIS IS CURRENTLY BETA!!!


Currently you can provide cache dependency on a single content item id and a media item id.
Future developments may allow you to tie into a Document type name/Media type name so the cache is expired when any of those document types are edited.
