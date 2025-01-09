using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Viperinius.Plugin.SpotifyImport.Configuration;

namespace Viperinius.Plugin.SpotifyImport
{
    internal abstract class GenericPlaylistProvider
    {
        private readonly ILogger<GenericPlaylistProvider> _logger;

        protected GenericPlaylistProvider(ILogger<GenericPlaylistProvider> logger)
        {
            _logger = logger;
            Playlists = new List<ProviderPlaylistInfo>();
        }

        /// <summary>
        /// Gets the provider name.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Gets the base URL to the provider API.
        /// </summary>
        public abstract Uri ApiUrl { get; }

        /// <summary>
        /// Gets or sets the auth token for the API (if needed).
        /// </summary>
        public abstract object? AuthToken { get; set; }

        /// <summary>
        /// Gets or sets the list of playlist sources.
        /// </summary>
        public List<ProviderPlaylistInfo> Playlists { get; protected set; }

        public abstract void SetUpProvider();

        public virtual async Task<List<string>?> GetUserPlaylistIds(
            TargetUserConfiguration target,
            CancellationToken? cancellationToken = null)
        {
            var playlistsInfo = await GetUserPlaylistsInfo(target, cancellationToken).ConfigureAwait(false);
            return playlistsInfo?.Select(p => p.Id).ToList();
        }

        public virtual async Task PopulatePlaylists(List<string> playlistIds, CancellationToken? cancellationToken = null)
        {
            _logger.LogInformation("Starting to query {Amount} playlists from {Name}", playlistIds.Count, Name);

            foreach (var playlistId in playlistIds)
            {
                var playlist = await GetPlaylist(playlistId, cancellationToken).ConfigureAwait(false);
                if (playlist != null)
                {
                    Playlists.Add(playlist);
                }
                else
                {
                    _logger.LogError("Failed to get playlist with id {Id}", playlistId);
                }
            }
        }

        protected abstract Task<List<ProviderPlaylistInfo>?> GetUserPlaylistsInfo(
            TargetUserConfiguration target,
            CancellationToken? cancellationToken = null);

        protected abstract Task<ProviderPlaylistInfo?> GetPlaylist(string playlistId, CancellationToken? cancellationToken = null);

        protected abstract Task<ProviderTrackInfo?> GetTrack(string trackId, CancellationToken? cancellationToken = null);

        protected abstract string CreatePlaylistState<T>(T data);
    }
}
