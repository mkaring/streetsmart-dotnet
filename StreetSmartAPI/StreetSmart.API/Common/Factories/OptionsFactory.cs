﻿/*
 * Street Smart .NET integration
 * Copyright (c) 2016 - 2020, CycloMedia, All rights reserved.
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
using System.Globalization;
using System.Security;

using StreetSmart.Common.Data;
using StreetSmart.Common.Interfaces.Data;
using StreetSmart.Common.Interfaces.DomElement;

namespace StreetSmart.Common.Factories
{
  /// <summary>
  /// Factory for create a object which containing the options used for initializing the API
  /// </summary>
  public static class OptionsFactory
  {
    // ReSharper disable InconsistentNaming

    /// <summary>
    /// Create the options object which used for initializing the API
    /// </summary>
    /// <param name="userName">Username of the user.</param>
    /// <param name="password">Password of the user.</param>
    /// <param name="apiKey">ApiKey given to the user.</param>
    /// <param name="srs">Coordinate system used in the API. E.g. "EPSG:29882".</param>
    /// <param name="element">The Domelement where in the panoramic image is rendered.</param>
    /// <returns>Object containing the options used for initializing the API</returns>
    public static IOptions Create(string userName, string password, string apiKey, string srs, IDomElement element)
      => Create(userName, password, apiKey, srs, null, element);

    /// <summary>
    /// Create the options object which used for initializing the API
    /// </summary>
    /// <param name="userName">Username of the user.</param>
    /// <param name="password">Password of the user.</param>
    /// <param name="apiKey">ApiKey given to the user.</param>
    /// <param name="srs">Coordinate system used in the API. E.g. "EPSG:29882".</param>
    /// <param name="addressSettings">The address settings to use for address searches.</param>
    /// <param name="element">The Domelement where in the panoramic image is rendered.</param>
    /// <returns>Object containing the options used for initializing the API</returns>
    public static IOptions Create(string userName, string password, string apiKey, string srs,
      IAddressSettings addressSettings, IDomElement element)
      => Create(userName, password, apiKey, srs, string.Empty, string.Empty, addressSettings, element);

    /// <summary>
    /// Create the options object which used for initializing the API
    /// </summary>
    /// <param name="userName">Username of the user.</param>
    /// <param name="password">Password of the user.</param>
    /// <param name="apiKey">ApiKey given to the user.</param>
    /// <param name="srs">Coordinate system used in the API. E.g. "EPSG:29882".</param>
    /// <param name="locale">Language used as default in the API.</param>
    /// <param name="addressSettings">The address settings to use for address searches.</param>
    /// <param name="element">The Domelement where in the panoramic image is rendered.</param>
    /// <returns>Object containing the options used for initializing the API</returns>
    public static IOptions Create(string userName, string password, string apiKey, string srs, string locale,
      IAddressSettings addressSettings, IDomElement element)
      => Create(userName, password, apiKey, srs, locale, string.Empty, addressSettings, element);

    /// <summary>
    /// Create the options object which used for initializing the API
    /// </summary>
    /// <param name="userName">Username of the user.</param>
    /// <param name="password">Password of the user.</param>
    /// <param name="apiKey">ApiKey given to the user.</param>
    /// <param name="srs">Coordinate system used in the API. E.g. "EPSG:29882".</param>
    /// <param name="locale">Language used as default in the API.</param>
    /// <param name="configurationURL">Alternate configuration url to use for all configuration services.</param>
    /// <param name="addressSettings">The address settings to use for address searches.</param>
    /// <param name="element">The Domelement where in the panoramic image is rendered.</param>
    /// <returns>Object containing the options used for initializing the API</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IOptions Create(string userName, string password, string apiKey, string srs, string locale,
      string configurationURL, IAddressSettings addressSettings, IDomElement element)
    {
      if (string.IsNullOrEmpty(userName))
      {
        throw new ArgumentNullException(nameof(userName));
      }

      if (string.IsNullOrEmpty(password))
      {
        throw new ArgumentNullException(nameof(password));
      }

      if (string.IsNullOrEmpty(apiKey))
      {
        throw new ArgumentNullException(nameof(apiKey));
      }

      if (string.IsNullOrEmpty(srs))
      {
        throw new ArgumentNullException(nameof(srs));
      }

      SecureString Password = new SecureString();

      foreach (var character in password)
      {
        Password.AppendChar(character);
      }

      return new Options(userName, Password, apiKey, srs, locale,
        string.IsNullOrEmpty(configurationURL) ? null : new Uri(configurationURL), addressSettings, element);
    }

    // ReSharper restore InconsistentNaming
  }
}
