﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tripous;

namespace DevApp.WinForms
{
    /// <summary>
    /// The Project resource provider
    /// </summary>
    [ResourceProviderAttribute()]
    internal class ResourceProvider : Tripous.ResourceProvider
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ResourceProvider()
            : base(Properties.Resources.ResourceManager, typeof(ResourceProvider).Namespace)
        {
        }
    }
}
