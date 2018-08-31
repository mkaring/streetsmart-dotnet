﻿/*
 * Street Smart .NET integration
 * Copyright (c) 2016 - 2018, CycloMedia, All rights reserved.
 * 
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3.0 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library.
 */

using System.Drawing;

namespace StreetSmart.Common.Interfaces.GeoJson
{
  /// <summary>
  /// Observation lines
  /// </summary>
  public interface IObservationLines
  {
    /// <summary>
    /// Active observation
    /// </summary>
    int ActiveObservation { get; }

    /// <summary>
    /// RecordingId
    /// </summary>
    string RecordingId { get; }

    /// <summary>
    /// Color
    /// </summary>
    Color Color { get; }

    /// <summary>
    /// Selected measure method
    /// </summary>
    MeasureMethod SelectedMeasureMethod { get; }
  }
}
