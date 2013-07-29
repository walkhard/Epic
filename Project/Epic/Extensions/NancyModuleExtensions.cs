using System;
using Nancy;

namespace Epic
{
    public static class NancyModuleExtensions
    {
        /// <summary>
        /// Set alert.
        /// </summary>
        /// <param name="module">NancyModule.</param>
        /// <param name="message">Message.</param>
        public static void Alert(this NancyModule module, string message)
        {
            module.Session ["EpicAlerts"] = message;
        }
    }
}