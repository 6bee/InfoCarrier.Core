﻿// Copyright (c) on/off it-solutions gmbh. All rights reserved.
// Licensed under the MIT license. See license.txt file in the project root for license information.

namespace InfoCarrier.Core.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using Microsoft.EntityFrameworkCore.Update;

    /// <summary>
    ///     A serializable object containing the <see cref="UpdateEntryDto" />'s for saving
    ///     updated entities into the actual database on the server-side.
    /// </summary>
    [DataContract]
    public class SaveChangesRequest : EntityDtoHolder
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SaveChangesRequest"/> class.
        /// </summary>
        [Obsolete("Called by the de-serializer; should only be called by deriving classes for de-serialization purposes")]
        public SaveChangesRequest()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SaveChangesRequest"/> class.
        /// </summary>
        /// <param name="entries">The <see cref="IUpdateEntry" />'s which need to be saved.</param>
        internal SaveChangesRequest(IReadOnlyList<IUpdateEntry> entries)
            : base(entries)
        {
            this.SharedIdentityDataTransferObjects.AddRange(
                this.ToUpdateEntryDtos(entries.Select(e => e.SharedIdentityEntry).Where(e => e != null)));
        }

        /// <summary>
        ///     Gets or sets state shared state entires mapped to <see cref="UpdateEntryDto" />'s.
        /// </summary>
        [DataMember]
        public List<UpdateEntryDto> SharedIdentityDataTransferObjects { get; set; } = new List<UpdateEntryDto>();
    }
}
