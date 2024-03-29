﻿

using Microsoft.AspNetCore.Http;
using System;
using System.Net.Mime;

namespace Cafe.Infrastructure
{
	public static class MimeTypes
	{
		public static class Application
		{
			public const string Json = MediaTypeNames.Application.Json;
			public const string Octet = MediaTypeNames.Application.Octet;
			public const string Pdf = MediaTypeNames.Application.Pdf;
			public const string Rtf = MediaTypeNames.Application.Rtf;
			public const string Soap = MediaTypeNames.Application.Soap;
			public const string Xml = MediaTypeNames.Application.Xml;
			public const string Zip = MediaTypeNames.Application.Zip;
			public const string XWWWFormUrlencoded = "application/x-www-form-urlencoded";
		}
		public static class Image
		{
			public const string Gif = MediaTypeNames.Image.Gif;
			public const string Jpeg = MediaTypeNames.Image.Jpeg;
			public const string Tiff = MediaTypeNames.Image.Tiff;
		}
		public static class Text
		{
			public const string Html = MediaTypeNames.Text.Html;
			public const string Plain = MediaTypeNames.Text.Plain;
			public const string RichText = MediaTypeNames.Text.RichText;
			public const string Xml = MediaTypeNames.Text.Xml;
		}
	}
}
