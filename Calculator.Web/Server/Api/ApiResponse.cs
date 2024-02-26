//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Net;

namespace Calculator.Web.Server.Api;

public sealed record ApiResponse(HttpStatusCode StatusCode, string Content, string MimeType);
