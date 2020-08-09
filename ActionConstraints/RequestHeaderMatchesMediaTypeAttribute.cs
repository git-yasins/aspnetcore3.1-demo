using System;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace aspnetcore3_demo.ActionConstraints {
    /// <summary>
    /// 请求头媒体类型匹配特性,作用于Action
    /// </summary>
    [AttributeUsage (AttributeTargets.All, Inherited = true, AllowMultiple = true)]
    public class RequestHeaderMatchesMediaTypeAttribute : Attribute, IActionConstraint {
        private readonly MediaTypeCollection _mediaTypes = new MediaTypeCollection ();
        private readonly string requestHeaderToMatch;
        /// <summary>
        /// 匹配Heades Content-Type媒体类型
        /// </summary>
        /// <param name="requestHeaderToMatch">Content-Type</param>
        /// <param name="mediaType">application/json</param>
        /// <param name="otherMediaTypes">application/vnd.company.companyforcreation+json</param>
        public RequestHeaderMatchesMediaTypeAttribute (string requestHeaderToMatch, string mediaType, params string[] otherMediaTypes) {
            this.requestHeaderToMatch = requestHeaderToMatch??throw new ArgumentNullException (nameof (requestHeaderToMatch));
            if (MediaTypeHeaderValue.TryParse (mediaType, out MediaTypeHeaderValue parseMediaType)) {
                _mediaTypes.Add (parseMediaType);
            } else {
                throw new ArgumentNullException (nameof (mediaType));
            }

            foreach (var otherMeidaType in otherMediaTypes) {
                if (MediaTypeHeaderValue.TryParse (otherMeidaType, out MediaTypeHeaderValue parseOtherMediaType)) {
                    _mediaTypes.Add (parseOtherMediaType);
                } else {
                    throw new ArgumentNullException (nameof (otherMeidaType));
                }
            }
        }

        public int Order => 0;

        public bool Accept (ActionConstraintContext context) {
            var requestHeaders = context.RouteContext.HttpContext.Request.Headers;
            if (!requestHeaders.ContainsKey (requestHeaderToMatch))
                return false;
            var parsedRequestMediaType = new MediaType (requestHeaders[requestHeaderToMatch]);
            //比较系统媒体类型和传入的媒体类型是否匹配
            foreach (var mediaType in _mediaTypes) {
                var parseMediaType = new MediaType (mediaType);
                if (parsedRequestMediaType.Equals (parseMediaType)) {
                    return true;
                }
            }
            return false;
        }
    }
}
