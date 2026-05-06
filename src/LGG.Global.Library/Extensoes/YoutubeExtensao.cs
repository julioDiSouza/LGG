namespace LGG.Global.Library.Extensoes
{
    public static class YoutubeExtensao
    {
        public static string ObterVideoId(this string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return string.Empty;

            url = url.Trim();

            // Handle YouTube Shorts URLs: https://www.youtube.com/shorts/NfaqL5RVqog
            if (url.Contains("/shorts/"))
            {
                var shortsIndex = url.IndexOf("/shorts/");
                var videoId = url.Substring(shortsIndex + 8); // 8 = "/shorts/".Length
                // Remove any additional parameters after the video ID (like ?...)
                var questionMarkIndex = videoId.IndexOf('?');
                if (questionMarkIndex > 0)
                    videoId = videoId.Substring(0, questionMarkIndex);
                return videoId.Trim();
            }

            // Handle youtu.be short URLs: https://youtu.be/VIDEO_ID or https://youtu.be/VIDEO_ID?t=123s
            if (url.Contains("youtu.be/"))
            {
                var baseDomain = "youtu.be/";
                var domainIndex = url.IndexOf(baseDomain);
                if (domainIndex >= 0)
                {
                    var videoId = url.Substring(domainIndex + baseDomain.Length);
                    // Remove any additional parameters after the video ID (like ?...)
                    var questionMarkIndex = videoId.IndexOf('?');
                    if (questionMarkIndex > 0)
                        videoId = videoId.Substring(0, questionMarkIndex);
                    return videoId.Trim();
                }
            }

            // Handle embed URLs: https://www.youtube.com/embed/VIDEO_ID or https://www.youtube-nocookie.com/embed/VIDEO_ID
            if (url.Contains("/embed/"))
            {
                var embedIndex = url.IndexOf("/embed/");
                var videoId = url.Substring(embedIndex + 7); // 7 = "/embed/".Length
                // Remove any additional parameters after the video ID (like ?...)
                var questionMarkIndex = videoId.IndexOf('?');
                if (questionMarkIndex > 0)
                    videoId = videoId.Substring(0, questionMarkIndex);
                // Remove trailing slash if present
                videoId = videoId.TrimEnd('/');
                return videoId.Trim();
            }

            // Handle regular YouTube watch URLs with v parameter as first parameter: https://www.youtube.com/watch?v=VIDEO_ID&t=39s
            var vIndex = url.IndexOf("?v=");
            if (vIndex >= 0)
            {
                var videoId = url.Substring(vIndex + 3); // 3 = "?v=".Length
                // Remove any additional parameters after the video ID (like &t=... or &list=...)
                var ampersandIndex = videoId.IndexOf('&');
                if (ampersandIndex > 0)
                    videoId = videoId.Substring(0, ampersandIndex);
                return videoId.Trim();
            }

            // Handle URLs where v is not the first parameter: https://www.youtube.com/watch?t=...&v=...
            vIndex = url.IndexOf("&v=");
            if (vIndex >= 0)
            {
                var videoId = url.Substring(vIndex + 3); // 3 = "&v=".Length
                var ampersandIndex = videoId.IndexOf('&');
                if (ampersandIndex > 0)
                    videoId = videoId.Substring(0, ampersandIndex);
                return videoId.Trim();
            }

            return url;
        }
    }
}
