﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Services
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private static log4net.ILog Log = log4net.LogManager.GetLogger(typeof(ErrorHandlingMiddleware));
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context) 
        {
            try
            {
                _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
                throw ex;
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            Log.Error(exception.Message, exception);
            return Task.CompletedTask;
        }
    }
}
