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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StreetSmart.Common.Interfaces.GeoJson;

namespace StreetSmart.Common.Data.GeoJson
{
  internal class MeasureDetails: DataConvert, IMeasureDetails//,IEquatable<MeasureDetails>
  {
    public MeasureDetails(Dictionary<string, object> measureDetails, MeasurementTools measurementTool)
    {
      var details = GetDictValue(measureDetails, "details");
      var pointProblems = GetListValue(measureDetails, "pointProblems");
      string pointReliability = ToString(measureDetails, "pointReliability");

      PointProblems = new List<PointProblems>();

      try
      {
        MeasureMethod = (MeasureMethod) ToEnum(typeof(MeasureMethod), measureDetails, "measureMethod");
      }
      catch (ArgumentException)
      {
        MeasureMethod = MeasureMethod.NotDefined;
      }

      switch (MeasureMethod)
      {
        case MeasureMethod.DepthMap:
          Details = new DetailsDepth(details);
          break;
        case MeasureMethod.SmartClick:
          Details = new DetailsSmartClick(details);
          break;
        case MeasureMethod.ForwardIntersection:
          Details = new DetailsForwardIntersection(details, measurementTool);
          break;
        case MeasureMethod.AutoFocus:
        case MeasureMethod.NotDefined:
          Details = null;
          break;
      }

      foreach (var pointProblem in pointProblems)
      {
        switch (pointProblem?.ToString() ?? string.Empty)
        {
          case "ONE_OBSERVATION":
            PointProblems.Add(Interfaces.GeoJson.PointProblems.OneObservation);
            break;
          case "TOO_FEW_RECORDINGS":
            PointProblems.Add(Interfaces.GeoJson.PointProblems.TooFewRecordings);
            break;
          case "INVALID_ANGLE":
            PointProblems.Add(Interfaces.GeoJson.PointProblems.InvalidAngle);
            break;
          case "POINT_TOO_FAR":
            PointProblems.Add(Interfaces.GeoJson.PointProblems.PointTooFar);
            break;
          case "STANDARD_DEVIATION":
            PointProblems.Add(Interfaces.GeoJson.PointProblems.StandardDeviation);
            break;
        }
      }

      switch (pointReliability)
      {
        case "RELIABLE":
          PointReliability = Reliability.Reliable;
          break;
        case "ACCEPTABLE":
          PointReliability = Reliability.Acceptable;
          break;
        case "UNRELIABLE":
          PointReliability = Reliability.Unreliable;
          break;
        default:
          PointReliability = Reliability.NotDefined;
          break;
      }
    }

    public MeasureDetails(IMeasureDetails measureDetails, MeasurementTools measurementTool)
    {
      if (measureDetails != null)
      {
        MeasureMethod = measureDetails.MeasureMethod;

        switch (measureDetails.MeasureMethod)
        {
          case MeasureMethod.DepthMap:
            Details = new DetailsDepth((IDetailsDepth) measureDetails.Details);
            break;
          case MeasureMethod.SmartClick:
            Details = new DetailsSmartClick((IDetailsSmartClick) measureDetails.Details);
            break;
          case MeasureMethod.ForwardIntersection:
            Details = new DetailsForwardIntersection((IDetailsForwardIntersection) measureDetails.Details, measurementTool);
            break;
          case MeasureMethod.AutoFocus:
          case MeasureMethod.NotDefined:
            Details = null;
            break;
        }

        if (measureDetails.PointProblems != null)
        {
          PointProblems = new List<PointProblems>();

          foreach (var pointProblem in measureDetails.PointProblems)
          {
            PointProblems.Add(pointProblem);
          }
        }

        PointReliability = measureDetails.PointReliability;
      }
    }

    public MeasureDetails()
    {
      MeasureMethod = MeasureMethod.unknown;
      PointProblems = new List<PointProblems>();
    }

    public MeasureMethod MeasureMethod { get; }

    public IDetails Details { get; }

    public IList<PointProblems> PointProblems { get; }

    public Reliability PointReliability { get; }

    public override string ToString()
    {
      var sb = new StringBuilder();

      sb.Append("[");
      if (PointProblems.Any())
      {
        sb.Append(string.Join(",", PointProblems.Select(problem => $"\"{problem.Description()}\"")));
      }
      sb.Append("]");

      string measureDetails = Details == null
          ? string.Empty
          : $",\"measureMethod\":\"{MeasureMethod.Description()}\",\"details\":{Details},\"pointProblems\":{sb},\"pointReliability\":\"{PointReliability.Description()}\"";

      sb.Clear();
      sb.Append("{");
      sb.Append(measureDetails.TrimStart(','));
      sb.Append("}");

      return $"{sb}";
    }

    /*
  public bool Equals(MeasureDetails other)
  {
    if (other == null) return false;
    return PointProblems.SequenceEqual(other.PointProblems) &&
           MeasureMethod.Equals(other.MeasureMethod) &&
           Details == other.Details &&
           PointReliability.Equals(other.PointReliability);
  }

  public override bool Equals(object obj)
  {
    return Equals(obj as MeasureDetails);
  }

  public override int GetHashCode() => (PointProblems, MeasureMethod, Details, PointReliability).GetHashCode();



     */
    //public override string ToString()
    //{
    //  string pointsWithProblems = PointProblems.Aggregate("[", (current, problem) => $"{current}\"{problem.Description()}\",");
    //  string pointsWithProblemsStr = $"{pointsWithProblems.Substring(0, Math.Max(pointsWithProblems.Length - 1, 1))}]";

    //  string measureDetails = Details == null
    //    ? string.Empty
    //    : $"\"measureMethod\":\"{MeasureMethod.Description()}\",\"details\":{Details},\"pointProblems\":{pointsWithProblemsStr}," +
    //      $"\"pointReliability\":\"{PointReliability.Description()}\"";

    //  return $"{{{measureDetails}}}";
    //}
  }
}
