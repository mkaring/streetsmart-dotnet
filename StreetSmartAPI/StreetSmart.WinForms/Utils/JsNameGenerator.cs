﻿/*
 * Integration in ArcMap for Cycloramas
 * Copyright (c) 2016, CycloMedia, All rights reserved.
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

using System;
using System.Collections.Generic;
using System.Linq;

namespace StreetSmart.WinForms.Utils
{
  internal class JsNameGenerator : List<string>
  {
    public JsNameGenerator(int numberItems)
    {
      for (int i = 0; i < numberItems; i++)
      {
        Add($"type{Guid.NewGuid():N}");
      }
    }

    public string JsGetTypeDef()
    {
      return this.Aggregate(string.Empty, (current, name) => $"{current}var {name};");
    }

    public string JsAssignToNames(string typeName)
    {
      string result = string.Empty;

      for (int i = 0; i < Count; i++)
      {
        result = $"{result}{this[i]}={typeName}[{i}];";
      }

      return result;
    }

    public string JsToResultTypes(string resultType)
    {
      return this.Aggregate($"let {resultType}={{}};",
        (current, type) => $"{current}{resultType}[{type}.getType()]={type.ToQuote()};");
    }
  }
}
