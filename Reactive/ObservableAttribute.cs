// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Reactive
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class ObservableAttribute : Attribute
    {
        /// <summary>
        /// If PropertyName is not specified, the field name will be converted from '_name' or 'm_name' to 'Name'.
        /// </summary>
        /// <param name="propertyName">Property name to bind.</param>
        public ObservableAttribute(string propertyName = "") => PropertyName = propertyName;

        public string PropertyName { get; init; }
    }
}