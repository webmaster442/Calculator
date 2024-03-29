﻿//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Diagnostics;

namespace CalculatorShell.Engine.Algortihms;

internal static class Doubles
{
    public static double DegToRad(double angle)
        => angle * (Math.PI / 180.0);

    public static double GradToRad(double gradians)
        => gradians * (Math.PI / 200.0);

    public static double RadToDeg(double radians)
        => radians * (180.0 / Math.PI);

    public static double RadToGrad(double radians)
        => radians * (200.0 / Math.PI);

    public static double SinCorrected(double angle, AngleSystem angleSystem)
    {
        return angleSystem switch
        {
            AngleSystem.Rad => Math.Round(Math.Sin(angle), 14),
            AngleSystem.Deg => Math.Round(Math.Sin(DegToRad(angle)), 14),
            AngleSystem.Grad => Math.Round(Math.Sin(GradToRad(angle)), 14),
            _ => throw new UnreachableException(),
        };
    }

    public static double AsinCorrected(double value, AngleSystem angleSystem)
    {
        return angleSystem switch
        {
            AngleSystem.Rad => Math.Round(Math.Asin(value), 14),
            AngleSystem.Deg => Math.Round(RadToDeg(Math.Asin(value)), 14),
            AngleSystem.Grad => Math.Round(RadToGrad(Math.Asin(value)), 14),
            _ => throw new UnreachableException(),
        };
    }

    public static double CosCorrected(double angle, AngleSystem angleSystem)
    {
        return angleSystem switch
        {
            AngleSystem.Rad => Math.Round(Math.Cos(angle), 14),
            AngleSystem.Deg => Math.Round(Math.Cos(DegToRad(angle)), 14),
            AngleSystem.Grad => Math.Round(Math.Cos(GradToRad(angle)), 14),
            _ => throw new UnreachableException(),
        };
    }

    public static double AcosCorrected(double value, AngleSystem angleSystem)
    {
        return angleSystem switch
        {
            AngleSystem.Rad => Math.Round(Math.Acos(value), 14),
            AngleSystem.Deg => Math.Round(RadToDeg(Math.Acos(value)), 14),
            AngleSystem.Grad => Math.Round(RadToGrad(Math.Acos(value)), 14),
            _ => throw new UnreachableException(),
        };
    }

    public static double TanCorrected(double angle, AngleSystem angleSystem)
    {
        return SinCorrected(angle, angleSystem) / CosCorrected(angle, angleSystem);
    }

    public static double AtanCorrected(double value, AngleSystem angleSystem)
    {
        return angleSystem switch
        {
            AngleSystem.Rad => Math.Round(Math.Atan(value), 14),
            AngleSystem.Deg => Math.Round(RadToDeg(Math.Atan(value)), 14),
            AngleSystem.Grad => Math.Round(RadToGrad(Math.Atan(value)), 14),
            _ => throw new UnreachableException(),
        };
    }

    public static double Percent(double number, double percent)
    {
        return (number / 100.0) * percent;
    }
}
