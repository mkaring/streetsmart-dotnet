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

using System.Collections.Generic;
using System.Linq;

using StreetSmart.Common.Interfaces.Data;

namespace StreetSmart.Common.Data
{
  internal class PanoramaViewerOptions : BaseViewerOptions, IPanoramaViewerOptions
  {
    private bool? _replace;
    private bool? _recordingsVisible;

    public PanoramaViewerOptions(bool? closable, bool? maximizable, bool? timeTravelVisible, bool? navBarVisible,
      bool? replace, bool? recordingsVisible) : base(closable, maximizable, timeTravelVisible, navBarVisible)
    {
      Replace = replace;
      RecordingsVisible = recordingsVisible;
    }

    public bool? Replace
    {
      get => _replace;
      set
      {
        _replace = value;
        RaisePropertyChanged();
      }
    }

    public bool? RecordingsVisible
    {
      get => _recordingsVisible;
      set
      {
        _recordingsVisible = value;
        RaisePropertyChanged();
      }
    }

    public override string ToString()
    {
      List<string> options = new List<string>();

      if (Replace != null)
      {
        options.Add($"replace:{Replace.ToString().ToLower()}");
      }

      if (RecordingsVisible != null)
      {
        options.Add($"recordingsVisible:{RecordingsVisible.ToString().ToLower()}");
      }

      string baseOptions = base.ToString();
      string result = options.Aggregate(baseOptions, (current, part) => $"{current},{part}");
      return options.Count == 0 && string.IsNullOrEmpty(baseOptions)
        ? string.Empty
        : $",panoramaViewer:{{{result}}}";
    }
  }
}
