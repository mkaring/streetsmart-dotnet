﻿/*
 * Street Smart .NET integration
 * Copyright (c) 2016 - 2021, CycloMedia, All rights reserved.
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

using System.Windows;

using StreetSmart.Common.API;
using StreetSmart.Common.Factories;
using StreetSmart.Common.Interfaces.API;
using StreetSmart.WPF;

namespace StreetSmart.Wpf
{
  /// <summary>
  /// Interaction logic for StreetSmartGUI.xaml
  /// </summary>
  public partial class StreetSmartGUI
  {
    #region dependency property API

    private readonly StreetSmartAPI _api;

    /// <summary>
    /// Dependency property of the api
    /// </summary>
    public static readonly DependencyProperty ApiProperty =
      DependencyProperty.Register("WpfApi", typeof(WpfApi), typeof(StreetSmartGUI),
        new PropertyMetadata(null, OnApiChanged));

    /// <summary>
    /// Changed the api
    /// </summary>
    private static void OnApiChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
      if (e.NewValue != null)
      {
        ((StreetSmartGUI) sender).InitApi((WpfApi) e.NewValue);
      }
    }

    #endregion

    #region Properties

    /// <summary>
    /// The StreetSmart API
    /// </summary>
    public WpfApi WpfApi
    {
      get => (WpfApi) GetValue(ApiProperty);
      set => SetValue(ApiProperty, value);
    }

        /// <summary>
        /// The StreetSmart API
        /// </summary>

        public IStreetSmartAPI Api => _api;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of the StreetSmartGUI
        /// </summary>
        public StreetSmartGUI()
    {
      InitializeComponent();

      _api = StreetSmartAPIFactory.Create() as StreetSmartAPI;
      _api?.InitBrowser(Browser);
    }

    #endregion

    #region private function - init the API

    private void InitApi(WpfApi api)
    {
      api.Api = Api;
    }

    #endregion
  }
}
