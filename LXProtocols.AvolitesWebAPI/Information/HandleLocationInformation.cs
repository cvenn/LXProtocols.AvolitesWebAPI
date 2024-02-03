using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXProtocols.AvolitesWebAPI
{
    /// <summary>
    /// Information about a handle location containing a subset of handles within the Titan system.
    /// </summary>
    public class HandleLocationInformation
    {
        /// <summary>
        /// The group name.
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// The index of the handle in the group.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// The page number in the group.
        /// </summary>
        public int Page { get; set; }
    }
}
