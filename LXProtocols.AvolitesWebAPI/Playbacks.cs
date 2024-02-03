using LXProtocols.AvolitesWebAPI.Information;
using LXProtocols.AvolitesWebAPI.JsonConverters;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace LXProtocols.AvolitesWebAPI
{
    /// <summary>
    /// Implements WebAPI methods related to controlling playbacks.
    /// </summary>
    public class Playbacks
    {
        private HttpClient http = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Playbacks"/> class.
        /// </summary>
        /// <param name="http">The HTTP.</param>
        internal Playbacks(HttpClient http)
        {
            this.http = http;
        }

        /// <summary>
        /// Gets the playback information given the titan ID for the playback.
        /// </summary>
        /// <param name="playbackId">The playback ID of the cue list containing the cue.</param>
        /// <returns>The playback information for the specified playback.</returns>
        public async Task<PlaybackInformation> GetPlayback(int playbackId) {
            return (await http.GetFromJsonAsync<JsonInformation<PlaybackInformation>>($"titan/playback/{playbackId}")).Information;
        }

        /// <summary>
        /// Gets information about the playbacks in a given group page.
        /// </summary>
        /// <param name="group">The playback group name.</param>
        /// <param name="page">The group page offset number.</param>
        /// <returns>Information for all playbacks in the specified group / page.</returns>
        public async Task<IEnumerable<HandleInformation>> GetPlaybacks(string group, int page) {
            return await http.GetFromJsonAsync<IEnumerable<HandleInformation>>($"titan/handles/{group}/{page}");
        }

        /// <summary>
        /// Fires the specified playback at full.
        /// </summary>
        /// <param name="userNumber">The user number of the playback to fire.</param>
        public async Task Fire(HandleReference handle)
        {
            await http.GetAsync($"titan/script/2/Playbacks/FirePlaybackAtLevel?{handle.ToQueryArgument("handle")}&level=1&alwaysRefire=false");           
        }

        /// <summary>
        /// Fires the specified playback at the specified level..
        /// </summary>
        /// <param name="userNumber">The user number of the playback to fire.</param>
        /// <param name="level">The level to fire the playback at where 1 is full and 0 is off.</param>
        public async Task Level(HandleReference handle, float level)
        {            
            await http.GetAsync($"titan/script/2/Playbacks/FirePlaybackAtLevel?{handle.ToQueryArgument("handle")}&level={level}&alwaysRefire=false");
        }

        /// <summary>
        /// Kills the specified playback aithout releasing.
        /// </summary>
        /// <param name="userNumber">The user number of the playback to kill.</param>
        public async Task Kill(HandleReference handle)
        {
            await http.GetAsync($"titan/script/2/Playbacks/KillPlayback?{handle.ToQueryArgument("handle")}");
        }

        /// <summary>
        /// Kills all running playbacks without releasing.
        /// </summary>
        public async Task KillAll()
        {
            await http.GetAsync($"titan/script/2/Playbacks/KillAllPlaybacks");
        }

        /// <summary>
        /// Records a single cue on a playback. The new cue is created using the information in the programmer and the current record mode.
        /// </summary>
        /// <param name="group">The handle group the new cue is to be recorded on.</param>
        /// <param name="index">The handle ID in the group the cue is to be recorded on.</param>
        /// <param name="updateOnly">if set to true [update only].</param>
        /// <returns>The TitanID of the created cue.</returns>
        public async Task<int> StoreCue(string group, int index, bool updateOnly = false) {
            return await http.GetFromJsonAsync<int>($"titan/script/2/Playbacks/StoreCue?group={group}&index={index}&updateOnly={updateOnly}");
        }

        /// <summary>
        /// Replaces an existing cue on a playback. The replacement cue is created using the information in the programmer and the current record mode.
        /// </summary>
        /// <param name="handle">The handle ID of the cue that is to be replaced.</param>
        /// <param name="updateOnly">if set to true [update only].</param>
        public async Task ReplaceCue(int handle, bool updateOnly = false) {
            await http.GetAsync($"titan/script/2/Playbacks/ReplacePlaybackCue?handle_titanId={handle}&updateOnly={updateOnly}");
        }
    }
}
