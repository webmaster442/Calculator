//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Net;

namespace Calculator.Web.Server.Api;

public delegate ApiResponse ApiMethod(HttpListenerRequest request);

public delegate void LowLevelApiMethod(HttpListenerContext context);
