﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Microsoft;

/// <summary>
/// Common runtime checks that throw exceptions upon failure.
/// </summary>
public static partial class Verify
{
    /// <summary>
    /// Throws an <see cref="InvalidOperationException"/> if a condition is false.
    /// </summary>
    [DebuggerStepThrough]
    public static void Operation([DoesNotReturnIf(false)] bool condition, string? message)
    {
        if (!condition)
        {
            throw new InvalidOperationException(message);
        }
    }

    /// <summary>
    /// Throws an <see cref="InvalidOperationException"/> if a condition is false.
    /// </summary>
    [DebuggerStepThrough]
    public static void Operation([DoesNotReturnIf(false)]bool condition, string unformattedMessage, object? arg1)
    {
        if (!condition)
        {
            throw new InvalidOperationException(PrivateErrorHelpers.Format(unformattedMessage, arg1));
        }
    }

    /// <summary>
    /// Throws an <see cref="InvalidOperationException"/> if a condition is false.
    /// </summary>
    [DebuggerStepThrough]
    public static void Operation([DoesNotReturnIf(false)]bool condition, string unformattedMessage, object? arg1, object? arg2)
    {
        if (!condition)
        {
            throw new InvalidOperationException(PrivateErrorHelpers.Format(unformattedMessage, arg1, arg2));
        }
    }

    /// <summary>
    /// Throws an <see cref="InvalidOperationException"/> if a condition is false.
    /// </summary>
    [DebuggerStepThrough]
    public static void Operation([DoesNotReturnIf(false)]bool condition, string unformattedMessage, params object?[] args)
    {
        if (!condition)
        {
            throw new InvalidOperationException(PrivateErrorHelpers.Format(unformattedMessage, args));
        }
    }

    /// <summary>
    /// Throws an <see cref="InvalidOperationException"/> if a condition is false.
    /// </summary>
    [DebuggerStepThrough]
    public static void OperationWithHelp([DoesNotReturnIf(false)]bool condition, string? message, string? helpLink)
    {
        if (!condition)
        {
            var ex = new InvalidOperationException(message)
            {
                HelpLink = helpLink,
            };
            throw ex;
        }
    }

    /// <summary>
    /// Throws an <see cref="InvalidOperationException"/>.
    /// </summary>
    /// <returns>
    /// Nothing.  This method always throws.
    /// The signature claims to return an exception to allow callers to throw this method
    /// to satisfy C# execution path constraints.
    /// </returns>
    [DebuggerStepThrough]
    [DoesNotReturn]
    public static Exception FailOperation(string message, params object?[] args)
    {
        throw new InvalidOperationException(PrivateErrorHelpers.Format(message, args));
    }

    /// <summary>
    /// Throws an <see cref="ObjectDisposedException"/> if an object is disposed.
    /// </summary>
    [DebuggerStepThrough]
    public static void NotDisposed(IDisposableObservable disposedValue, string? message = null)
    {
        Requires.NotNull(disposedValue, nameof(disposedValue));

        if (disposedValue.IsDisposed)
        {
            string objectName = disposedValue.GetType().FullName;
            if (message is object)
            {
                throw new ObjectDisposedException(objectName, message);
            }
            else
            {
                throw new ObjectDisposedException(objectName);
            }
        }
    }

    /// <summary>
    /// Throws an <see cref="ObjectDisposedException"/> if a condition is false.
    /// </summary>
    [DebuggerStepThrough]
    public static void NotDisposed([DoesNotReturnIf(false)] bool condition, object? disposedValue, string? message = null)
    {
        if (!condition)
        {
            string objectName = disposedValue is object ? disposedValue.GetType().FullName : string.Empty;
            if (message is object)
            {
                throw new ObjectDisposedException(objectName, message);
            }
            else
            {
                throw new ObjectDisposedException(objectName);
            }
        }
    }

    /// <summary>
    /// Throws an <see cref="ObjectDisposedException"/> if a condition is false.
    /// </summary>
    [DebuggerStepThrough]
    public static void NotDisposed([DoesNotReturnIf(false)]bool condition, string? message)
    {
        if (!condition)
        {
            throw new ObjectDisposedException(null, message);
        }
    }

    /// <summary>
    /// Throws an exception if the given value is negative.
    /// </summary>
    /// <param name="hresult">The HRESULT corresponding to the desired exception.</param>
    /// <param name="ignorePreviousComCalls">If true, prevents <c>ThrowExceptionForHR</c> from returning an exception from a previous COM call and instead always use the HRESULT specified.</param>
    /// <remarks>
    /// No exception is thrown for S_FALSE.
    /// </remarks>
    [DebuggerStepThrough]
    [System.Security.SecurityCritical]
    public static void HResult(int hresult, bool ignorePreviousComCalls = false)
    {
        if (hresult < 0)
        {
            if (ignorePreviousComCalls)
            {
                Marshal.ThrowExceptionForHR(hresult, new IntPtr(-1));
            }
            else
            {
                Marshal.ThrowExceptionForHR(hresult);
            }
        }
    }
}
