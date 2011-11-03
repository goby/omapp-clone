﻿using System;
using System.Collections.Generic;
using System.Text;

using OperatingManagement.RemotingClient.Configuration;

namespace OperatingManagement.RemotingClient
{
    /// <summary>
    /// Contains methods to create types of objects locally or remotely, or obtain
    /// references to existing remote objects.
    /// </summary>
    public static class RemotingActivator
    {
        /// <summary>
        /// Creates a proxy for the well-known object indicated by the specified type and URL.
        /// </summary>
        /// <remarks>
        /// 
        /// Type and Url was configured within a configuration file.
        /// </remarks>
        /// <typeparam name="T">The specific Type.</typeparam>
        /// <returns></returns>
        public static T GetObject<T>()
        {
            Type type = typeof(T);
            RemotingObjectElement element = RemotingClientConfiguration.Section.Objects[type.FullName];
            return (T)Activator.GetObject(type, element.Url);
        }
        /// <summary>
        /// Creates a proxy for the well-known object indicated by the specified type and URL.
        /// </summary>
        /// <remarks>
        /// 
        /// Type and Url was configured within a configuration file.
        /// </remarks>
        /// <typeparam name="T">The specific Type.</typeparam>
        /// <param name="ip">This parameter will replace the '{IP}' regex in the configuration pattern.</param>
        /// <returns></returns>
        public static T GetObject<T>(string ip)
        {
            Type type = typeof(T);
            RemotingObjectElement element = RemotingClientConfiguration.Section.Objects[type.FullName];
            return (T)Activator.GetObject(type, element.Url.Replace("{IP}", ip));
        }
    }
}